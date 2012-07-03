using System.Data;

namespace Practice.Types.Interface
{
    /// <summary>
    /// The data table result filter interface for (AOP)
    /// </summary>
    public interface IDataTableResultFilter
    {
        #region Properties

        /// <summary>
        /// Gets Rank.
        /// </summary>
        int Rank { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The process.
        /// </summary>
        /// <param name="dataTable">
        /// The data table.
        /// </param>
        /// <param name="sqlCommand">
        /// The sql command.
        /// </param>
        void Process(ref DataTable dataTable, string sqlCommand);

        #endregion
    }
}