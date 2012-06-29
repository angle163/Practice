using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Practice.Extension;

namespace Practice.Web.Helper
{
    /// <summary>
    /// Page工具助手类。
    /// </summary>
    public static class PageHelper
    {
        /// <summary>
        /// 绑定数据GridView的数据源。
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="data"></param>
        public static void GridViewBindData(GridView gv, object data)
        {
            gv.DataSource = data;
            gv.DataBind();
        }

        /// <summary>
        /// 指定GridViewRow, 获得其指定索引的Cell中, 指定索引的Control。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="cellIndex"></param>
        /// <param name="controlIndex"></param>
        /// <returns></returns>
        public static T GetControlViaRow<T>(GridViewRow row, int cellIndex, int controlIndex) where T : class
        {
            return row.Cells[cellIndex].Controls[controlIndex] as T;
        }

        /// <summary>
        /// 指定GridViewRow, 查找其指定索引的Cell中, 指定ID的Control。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="cellIndex"></param>
        /// <param name="controlId"></param>
        /// <returns></returns>
        public static T FindControlViaRow<T>(GridViewRow row, int cellIndex, string controlId) where T : class
        {
            return row.Cells[cellIndex].FindControl(controlId) as T;
        }

        /// <summary>
        /// 指定元素, 添加指定的cssClass.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="addCssClass"></param>
        public static void AddCssClass(IAttributeAccessor tag, string addCssClass)
        {
            if (addCssClass.IsNotSet())
            {
                return;
            }
            if (tag.GetAttribute("class").IsNotSet())
            {
                tag.SetAttribute("class", addCssClass);
                return;
            }

            string[] classNames = addCssClass.Split(new char[] { ' ' });
            string setClass = " " + tag.GetAttribute("class") + " ";
            for (int i = 0; i < classNames.Length; i++)
            {
                if (setClass.IndexOf(string.Concat(" ", classNames[i], " "), StringComparison.Ordinal) < 0)
                {
                    setClass += classNames[i] + " ";
                }
            }
            tag.SetAttribute("class", setClass.Trim());
        }

        /// <summary>
        /// 指定元素, 移除指定的cssClass.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="removecssClass"></param>
        public static void RemoveCssClass(IAttributeAccessor tag, string removecssClass)
        {
            if (removecssClass.IsNotSet())
            {
                return;
            }

            if (tag.GetAttribute("class").IsNotSet())
            {
                return;
            }

            string[] classNames = removecssClass.Split(new char[] { ' ' });
            string className = " " + tag.GetAttribute("class") + " ";
            for (int i = 0; i < classNames.Length; i++)
            {
                className = className.Replace(string.Concat(" ", classNames[i], " "), " ");
            }
            tag.SetAttribute("class", className.Trim());
        }
    }
}