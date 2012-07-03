using System;
using System.Data;
using Practice.Types.Handler;

namespace Practice.Types.Interface
{
    public interface IDbConnectionManager : IDisposable
    {
        /// <summary>
        /// 获取连接字符串。
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 获取当前的数据库连接实例，不确定状态。
        /// </summary>
        IDbConnection DBConnection { get; }

        /// <summary>
        /// 获取打开状态的数据库连接，可以调用多次。
        /// </summary>
        IDbConnection OpenDBConnection { get; }

        /// <summary>
        /// 信息消息事件。
        /// </summary>
        event DBConnInfoMessageEventHandler InfoMessage;

        /// <summary>
        /// 初始化数据库连接新实例。
        /// </summary>
        void InitConnection();

        /// <summary>
        /// 关闭数据库连接实例。
        /// </summary>
        void CloseConnection();
    }
}