using System;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Config
{
    /// <summary>
    /// 访问配置文件AppSettings节点的静态类。
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 获取数据库连接字符串配置节点的键名。
        /// </summary>
        [NotNull]
        public static string ConnectionStringName
        {
            get { return GetConfigValueAsString("Practice.ConnectionStringName") ?? "practice"; }
        }

        /// <summary>
        /// 获取数据库所有者名称。
        /// </summary>
        [NotNull]
        public static string DatabaseOwner
        {
            get { return GetConfigValueAsString("Practice.DatabaseOwner") ?? "dbo"; }
        }

        /// <summary>
        /// 获取数据库里对象的限定表示符。
        /// </summary>
        [NotNull]
        public static string DatabaseObjectQualifier
        {
            get { return GetConfigValueAsString("Practice.DatabaseObjectQualifier") ?? ""; }
        }

        /// <summary>
        /// 从配置文件获取数据库连接超时秒数。
        /// </summary>
        [NotNull]
        public static string SqlCommandTimeout
        {
            get { return GetConfigValueAsString("Practice.SqlCommandTimeout") ?? "99999"; }
        }

        /// <summary>
        /// 从配置文件获取数据库连接字符串。
        /// </summary>
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString; }
        }

        /// <summary>
        /// 从配置文件获取指定键名对应节点的值，并转换为布尔值。
        /// </summary>
        /// <param name="configKey"> 配置键名称。</param>
        /// <param name="defaultValue"> 若配置值为空时，设置为默认值。</param>
        /// <returns>返回布尔值。</returns>
        public static bool GetConfigValueAsBool([NotNull] string configKey, bool defaultValue)
        {
            string value = GetConfigValueAsString(configKey);

            return !string.IsNullOrEmpty(value) ? Convert.ToBoolean(value.ToLower()) : defaultValue;
        }

        /// <summary>
        /// 从配置文件获取指定键名对应节点的值，并转换为字符串。
        /// </summary>
        /// <param name="configKey"> 配置键名称。 </param>
        /// <returns> 返回字符串。 </returns>
        public static string GetConfigValueAsString([NotNull] string configKey)
        {
            CodeContract.ArgumentNotNull(configKey, "configKey");

            return (from key in WebConfigurationManager.AppSettings.AllKeys
                    where key.Equals(configKey, StringComparison.CurrentCultureIgnoreCase)
                    select WebConfigurationManager.AppSettings[key]).FirstOrDefault();
        }

        /// <summary>
        /// 从配置获取提供者类型的字符串。
        /// </summary>
        /// <param name="providerName"> 提供者的名称。 </param>
        /// <returns>
        /// 若存在指定提供者的名称的节点， 则返回其值；否则返回<see langword="null"/>.
        /// </returns>
        public static string GetProvider([NotNull] string providerName)
        {
            string key = string.Format("Practice.Provider.{0}", providerName);
            return GetConfigValueAsString(key);
        }
    }
}