using Microsoft.Data.Sqlite;

namespace BuildingLink.Core.Database.Contexts
{
    public interface IDbContext
    {
        /// <summary>
        /// Excute single command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<int> ExcuteNonQueryCommandAsync(string command);

        /// <summary>
        /// Excute single command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<SqliteDataReader> ExcuteCommandAsync(string command);
    }
}