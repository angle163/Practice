using System.Collections.Generic;
using System.Data;

namespace Practice.Types.Interface
{
    /// <summary>
    /// 数据库访问接口。
    /// </summary>
    public interface IDbAccess
    {
        /// <summary>
        /// Filter list of result filters.
        /// </summary>
        IList<IDataTableResultFilter> ResultFilterList { get; }

        /// <summary>
        /// 获取当前数据库连接对象管理器实例。
        /// </summary>
        /// <returns></returns>
        IDbConnectionManager GetConnectionManager();

        /// <summary>
        /// 执行查询返回受影响的行数。
        /// </summary>
        /// <param name="cmd">命令实例。</param>
        /// <param name="transaction">启用事务。</param>
        /// <returns>返回受影响的行数。</returns>
        int ExecuteNonQuery(IDbCommand cmd, bool transaction);

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="cmd">命令实例。</param>
        /// <param name="transaction">启用事务。</param>
        /// <returns>结果集中第一行的第一列。</returns>
        object ExecuteScalar(IDbCommand cmd, bool transaction);

        /// <summary>
        /// 设置数据库连接器。
        /// </summary>
        /// <typeparam name="TManager"></typeparam>
        void SetConnectionManagerAdapter<TManager>() where TManager : IDbConnectionManager;

        /// <summary>
        /// 获取DataTable.
        /// </summary>
        /// <param name="cmd">命令实例。</param>
        /// <param name="transaction">启用事务。</param>
        /// <returns></returns>
        DataTable GetData(IDbCommand cmd, bool transaction);

        /// <summary>
        /// 获取DataTable.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        DataTable GetData(string commandText, bool transaction);

        /// <summary>
        /// 获取DataSet.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        DataSet GetDataSet(IDbCommand cmd, bool transaction);

        /// <summary>
        /// 从Data Reader中获取DataTable并返回。
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dt"></param>
        /// <param name="transaction"></param>
        /// <param name="acceptChanges"></param>
        /// <param name="firstColumnIndex"></param>
        /// <returns></returns>
        DataTable AddValuesToDataTableFormReader(IDbCommand cmd, DataTable dt,
            bool transaction, bool acceptChanges, int firstColumnIndex);

        /// <summary>
        /// 从Data Reader中获取DataTable并返回。
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dt"></param>
        /// <param name="transction"></param>
        /// <param name="acceptChanges"></param>
        /// <param name="firstColumnIndex"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        DataTable AddValuesToDataTableFormReader(IDbCommand cmd, DataTable dt,
            bool transction, bool acceptChanges, int firstColumnIndex, int currentIndex);

        /// <summary>
        /// 从Data Reader中获取DataTable并返回。
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="transaction"></param>
        /// <param name="acceptChanges"></param>
        /// <returns></returns>
        DataTable GetDataTableFromReader(IDbCommand cmd, bool transaction, bool acceptChanges);
    }
}