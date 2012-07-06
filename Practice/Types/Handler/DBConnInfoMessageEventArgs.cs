using System;

namespace Practice.Types.Handler
{
    /// <summary>
    /// 数据库连接信息消息事件数据类。
    /// </summary>
    public class DBConnInfoMessageEventArgs : EventArgs
    {
        /// <summary>
        /// 消息。
        /// </summary>
        private string _message;


        /// <summary>
        /// 初始化<see cref="DBConnInfoMessageEventArgs"/>类的新实例。
        /// </summary>
        /// <param name="message">消息。</param>
        public DBConnInfoMessageEventArgs(string message)
        {
            _message = message;
        }

        /// <summary>
        /// 获取或设置消息。
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}