using System.Data;
using System.Data.SqlClient;
using Practice.Config;
using Practice.Types.Annotation;
using Practice.Types.Handler;
using Practice.Types.Interface;

namespace Practice.Data.MsSql
{
    /// <summary>
    /// 为数据库连接实例提供打开/关闭操作的类。
    /// </summary>
    public class MsSqlDbConnectionManager : IDbConnectionManager
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库连接实例。
        /// </summary>
        protected SqlConnection _connection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 初始化<see cref="MsSqlDbConnectionManager" />类的新实例。
        /// </summary>
        public MsSqlDbConnectionManager()
        {
            // 仅初始化数据库连接实例，不打开。
            InitConnection();
        }

        /// <summary>
        /// 销毁资源。
        /// </summary>
        public virtual void Dispose()
        {
            // 关闭并删除连接。
            CloseConnection();
            _connection = null;
        }

        #endregion

        #region New region

        public event DBConnInfoMessageEventHandler InfoMessage;

        #endregion

        #region Properties

        /// <summary>
        /// 获取当前的数据库连接实例，不确定状态。
        /// </summary>
        public SqlConnection DBConnection
        {
            get
            {
                InitConnection();
                return _connection;
            }
        }

        /// <summary>
        /// 获取打开状态的数据库连接，可以调用多次。
        /// </summary>
        public SqlConnection OpenDBConnection
        {
            get
            {
                InitConnection();

                if (_connection.State != ConnectionState.Open)
                {
                    //打开连接
                    _connection.Open();
                }
                return _connection;
            }
        }

        /// <summary>
        /// 获取连接字符串。
        /// </summary>
        public string ConnectionString
        {
            get { return Config.Config.ConnectionString; }
        }

        /// <summary>
        /// 获取数据库连接实例。
        /// </summary>
        IDbConnection IDbConnectionManager.DBConnection
        {
            get { return DBConnection; }
        }

        IDbConnection IDbConnectionManager.OpenDBConnection
        {
            get { return OpenDBConnection; }
        }

        #endregion

        #region IDbConnectionManager

        /// <summary>
        /// 初始化数据库连接实例。
        /// </summary>
        public void InitConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection();
                _connection.InfoMessage += ConnectionInfoManager;
                _connection.ConnectionString = ConnectionString;
            }
            else if (_connection.State != ConnectionState.Open)
            {
                // 在这里检查连接字符串
                _connection.ConnectionString = ConnectionString;
            }
        }

        /// <summary>
        /// 关闭数据库连接实例。
        /// </summary>
        public void CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 触发数据库连接信息消息的事件方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ConnectionInfoManager([NotNull] object sender, [NotNull] SqlInfoMessageEventArgs e)
        {
            if (InfoMessage != null)
            {
                InfoMessage(this, new DBConnInfoMessageEventArgs(e.Message));
            }
        }

        #endregion
    }
}