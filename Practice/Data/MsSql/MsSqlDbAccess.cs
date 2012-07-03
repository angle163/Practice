using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Practice.Config;
using Practice.Extension;
using Practice.Types;
using Practice.Types.Annotation;
using Practice.Types.Interface;

namespace Practice.Data.MsSql
{
    /// <summary>
    /// SQL Server数据库访问类。
    /// </summary>
    public class MsSqlDbAccess : IDbAccess
    {
        #region Constants and Fields

        /// <summary>
        /// 连接管理器类的类型。
        /// </summary>
        private Type _connectionManagerType = typeof(MsSqlDbConnectionManager);

        /// <summary>
        /// 连接的事务锁行为。
        /// </summary>
        private static IsolationLevel _isolationLevel = IsolationLevel.ReadUncommitted;

        /// <summary>
        ///   Result filter list
        /// </summary>
        private readonly IList<IDataTableResultFilter> _resultFilterList = new List<IDataTableResultFilter>();

        #endregion

        #region Properties

        /// <summary>
        ///   Gets Current IDbAccess -- needs to be switched to direct injection into all DB classes.
        /// </summary>
        public static IDbAccess Current
        {
            get { return ServiceLocatorAccess.CurrentServiceProvider.Get<IDbAccess>(); }
        }

        /// <summary>
        ///   获取连接的事务锁行为。
        /// </summary>
        public static IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
        }

        /// <summary>
        ///   Gets the Result Filter List.
        /// </summary>
        /// <exception cref = "NotImplementedException">
        /// </exception>
        public IList<IDataTableResultFilter> ResultFilterList
        {
            get { return _resultFilterList; }
        }

        public void SetConnectionManagerAdapter<TManager>() where TManager : IDbConnectionManager
        {
            Type newConnectionManager = typeof(TManager);

            if (typeof(IDbConnectionManager).IsAssignableFrom(newConnectionManager))
            {
                _connectionManagerType = newConnectionManager;
            }
        }

        /// <summary>
        /// 从参数创建一个连接字符串。
        /// </summary>
        /// <param name="applicationName"> </param>
        /// <param name="dataSource"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns>返回连接字符串。</returns>
        public static string GetConnectionStrng(
            [NotNull] string applicationName,
            [NotNull] string dataSource,
            [NotNull] string initialCatalog,
            bool integratedSecurity,
            [NotNull] string userID,
            [NotNull] string password)
        {
            var connBuilder = new SqlConnectionStringBuilder
                                  {
                                      ApplicationName = applicationName,
                                      DataSource = dataSource,
                                      InitialCatalog = initialCatalog
                                  };
            if (integratedSecurity)
            {
                connBuilder.IntegratedSecurity = true;
            }
            else
            {
                connBuilder.UserID = userID;
                connBuilder.Password = password;
            }

            return connBuilder.ConnectionString;
        }

        /// <summary>
        /// Creates new SqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">
        /// Command text to qualify.
        /// </param>
        /// <param name="isText">
        /// Determines whether command text is text or stored procedure.
        /// </param>
        /// <returns>
        /// New SqlCommand
        /// </returns>
        public static SqlCommand GetCommand([NotNull] string commandText, bool isText)
        {
            return GetCommand(commandText, isText, null);
        }

        /// <summary>
        /// Creates new SqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">
        /// Command text to qualify.
        /// </param>
        /// <param name="isText">
        /// Determines whether command text is text or stored procedure.
        /// </param>
        /// <param name="connection">
        /// Connection to use with command.
        /// </param>
        /// <returns>
        /// New SqlCommand
        /// </returns>
        public static SqlCommand GetCommand([NotNull] string commandText, bool isText,
                                            [NotNull] SqlConnection connection)
        {
            return isText
                       ? new SqlCommand
                       {
                           CommandType = CommandType.Text,
                           CommandText = GetCommandTextReplaced(commandText),
                           Connection = connection
                       }
                       : GetCommand(commandText);
        }

        /// <summary>
        /// Creates new SqlCommand calling stored procedure applying all qualifiers to the name.
        /// </summary>
        /// <param name="storedProcedure">
        /// Base of stored procedure name.
        /// </param>
        /// <returns>
        /// New SqlCommand
        /// </returns>
        [NotNull]
        public static SqlCommand GetCommand([NotNull] string storedProcedure)
        {
            return GetCommand(storedProcedure, null);
        }

        /// <summary>
        /// Creates new SqlCommand calling stored procedure applying all qualifiers to the name.
        /// </summary>
        /// <param name="storedProcedure">
        /// Base of stored procedure name.
        /// </param>
        /// <param name="connection">
        /// Connection to use with command.
        /// </param>
        /// <returns>
        /// New SqlCommand
        /// </returns>
        [NotNull]
        public static SqlCommand GetCommand([NotNull] string storedProcedure, [NotNull] SqlConnection connection)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = GetObjectName(storedProcedure),
                Connection = connection,
                CommandTimeout = int.Parse(Config.Config.SqlCommandTimeout)
            };

            return cmd;
        }

        /// <summary>
        /// Gets command text replaced with {databaseOwner} and {objectQualifier}.
        /// </summary>
        /// <param name="commandText">
        /// Test to transform.
        /// </param>
        /// <returns>
        /// The get command text replaced.
        /// </returns>
        [NotNull]
        public static string GetCommandTextReplaced([NotNull] string commandText)
        {
            commandText = commandText.Replace("{databaseOwner}", Config.Config.DatabaseOwner);
            commandText = commandText.Replace("{objectQualifier}", Config.Config.DatabaseObjectQualifier);

            return commandText;
        }

        /// <summary>
        /// 获取限定的对象名称。
        /// </summary>
        /// <param name="name">对象的名称。</param>
        /// <returns>返回限定的对象名称，格式为 {databaseOwner}.{objectQualifier}name.</returns>
        public static string GetObjectName([NotNull] string name)
        {
            return "[{0}].[{1}{2}]".FormatWith(Config.Config.DatabaseOwner, Config.Config.DatabaseObjectQualifier, name);
        }

        /// <summary>
        /// 测试数据库连接。
        /// </summary>
        /// <param name="exceptionMessage">异常消息。</param>
        /// <returns>若连接成功返回true.</returns>
        public static bool TestConnection([NotNull] out string exceptionMessage)
        {
            exceptionMessage = string.Empty;
            bool success = false;

            try
            {
                using (var connection = Current.GetConnectionManager())
                {
                    // 试图连接数据库
                    var conn = connection.OpenDBConnection;
                }

                // 连接成功
                success = true;
            }
            catch (Exception e)
            {
                // 无法连接
                exceptionMessage = e.Message;
            }

            return success;
        }

        #endregion

        #region Implemented Interfaces

        #region IDbAccess

        /// <summary>
        /// 获取连接管理器实例。
        /// </summary>
        /// <returns></returns>
        [CanBeNull]
        public IDbConnectionManager GetConnectionManager()
        {
            return Activator.CreateInstance(_connectionManagerType).ToClass<IDbConnectionManager>();
        }

        public int ExecuteNonQuery([NotNull] IDbCommand cmd, bool transaction)
        {
            return Execte(c => c.ExecuteNonQuery(), cmd, transaction); ;
        }

        public object ExecuteScalar([NotNull] IDbCommand cmd, bool transaction)
        {
            return Execte(c => c.ExecuteScalar(), cmd, transaction);
        }

        public DataTable GetData([NotNull] IDbCommand cmd, bool transaction)
        {
            using (var qc = new QueryCounter(cmd.CommandText))
            {
                return ProcessUsingResultFilters(GetDatasetBasic(cmd, transaction).Tables[0], cmd.CommandText);
            }
        }

        public DataTable GetData([NotNull] string commandText, bool transaction)
        {
            using (var qc = new QueryCounter(commandText))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = commandText;
                    return ProcessUsingResultFilters(GetDatasetBasic(cmd, transaction).Tables[0], commandText);
                }
            }
        }

        [NotNull]
        public DataSet GetDataSet([NotNull] IDbCommand cmd, bool transaction)
        {
            using (var qc = new QueryCounter(cmd.CommandText))
            {
                return GetDatasetBasic(cmd, transaction);
            }
        }

        public DataTable AddValuesToDataTableFormReader(IDbCommand cmd, DataTable dt,
            bool transaction, bool acceptChanges, int firstColumnIndex)
        {
            // 方法实现应该用于兼容数据层。
            throw new Exception("Not in use for the data layer.");
        }

        public DataTable AddValuesToDataTableFormReader(IDbCommand cmd, DataTable dt,
            bool transction, bool acceptChanges, int firstColumnIndex, int currentIndex)
        {
            // 方法实现应该用于兼容数据层。
            throw new Exception("Not in use for the data layer.");
        }

        public DataTable GetDataTableFromReader(IDbCommand cmd, bool transaction, bool acceptChanges)
        {
            // 方法实现应该用于兼容数据层。
            throw new Exception("Not in use for the data layer.");
        }

        #endregion

        #endregion

        #region Methods

        private DataTable ProcessUsingResultFilters([NotNull] DataTable dataTable, [NotNull] string sqlCommand)
        {
            string commandCleaned = sqlCommand.Replace(
                "[{0}].[{1}".FormatWith(Config.Config.DatabaseOwner, Config.Config.DatabaseObjectQualifier), string.Empty);

            if (commandCleaned.EndsWith("]"))
            {
                // 移除最后的字符
                commandCleaned = commandCleaned.Substring(0, commandCleaned.Length - 1);
            }

            // sort filters and process each one...
            ResultFilterList.OrderBy(x => x.Rank).ToList().ForEach(i => i.Process(ref dataTable, commandCleaned));

            // return possibility modified dataTable
            return dataTable;
        }

        /// <summary>
        /// 执行命令返回DataSet.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [NotNull]
        private DataSet GetDatasetBasic([NotNull] IDbCommand cmd, bool transaction)
        {
            using (var connectionManager = GetConnectionManager())
            {
                // 确定连接是否为空
                if (cmd.Connection == null)
                {
                    cmd.Connection = connectionManager.OpenDBConnection;
                }
                else if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                // 创建数据桥接器
                using (var ds = new DataSet())
                {
                    using (var da = new SqlDataAdapter())
                    {
                        da.SelectCommand = (SqlCommand)cmd;
                        da.SelectCommand.Connection = (SqlConnection)cmd.Connection;
                        Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

                        if (transaction)
                        {
                            // 启用事务并执行命令
                            using (var trans = connectionManager.OpenDBConnection.BeginTransaction(_isolationLevel))
                            {
                                try
                                {
                                    da.SelectCommand.Transaction = (SqlTransaction)trans;
                                    da.Fill(ds);
                                }
                                finally
                                {
                                    trans.Commit();
                                }
                            }
                        }
                        else
                        {
                            // 不启用事务并执行命令
                            da.Fill(ds);
                        }

                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL命令并返回数据。
        /// </summary>
        /// <typeparam name="TResult">返回数据类型。</typeparam>
        /// <param name="func">执行SQL的委托。</param>
        /// <param name="cmd">命令对象。</param>
        /// <param name="transaction">启用事务。</param>
        /// <returns>返回执行结果。</returns>
        private TResult Execte<TResult>([NotNull] Func<IDbCommand, TResult> func, IDbCommand cmd, bool transaction)
        {
            TResult result;
            using (var qc = new QueryCounter(cmd.CommandText))
            {
                using (var connectionManager = GetConnectionManager())
                {
                    // 获取一个打开的连接实例
                    cmd.Connection = connectionManager.OpenDBConnection;

                    Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

                    if (transaction)
                    {
                        // 启用事务并执行命令
                        using (var trans = connectionManager.OpenDBConnection.BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            result = func(cmd);
                            trans.Commit();
                            return result;
                        }
                    }
                    // 不启用事务并执行命令
                    result = func(cmd);
                }
                qc.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 执行SQL命令。
        /// </summary>
        /// <param name="func">执行SQL的委托。</param>
        /// <param name="cmd">命令对象。</param>
        /// <param name="transaction">启用事务。</param>
        private void Execte([NotNull] Action<IDbCommand> func, IDbCommand cmd, bool transaction)
        {
            using (var qc = new QueryCounter(cmd.CommandText))
            {
                using (var connectionManager = GetConnectionManager())
                {
                    // 获取一个打开的连接实例
                    cmd.Connection = connectionManager.OpenDBConnection;

                    Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

                    if (transaction)
                    {
                        // 启用事务并执行命令
                        using (var trans = connectionManager.OpenDBConnection.BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            func(cmd);
                            trans.Commit();
                        }
                    }
                    // 不启用事务并执行命令
                    func(cmd);
                }
                qc.Dispose();
            }
        }

        #endregion
    }
}