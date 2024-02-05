using BuildingLink.Core.Common.Entities;
using Microsoft.Data.Sqlite;

namespace BuildingLink.Core.Database.Mappers
{
    public interface IEntityMapper
    {
        /// <summary>
        /// Map reader model to base entity object
        /// </summary>
        /// <param name="reader">Data reader object to read from</param>
        /// <returns>Mapped object</returns>
        IEnumerable<T> Map<T>(SqliteDataReader reader) where T : BaseEntity, new();
    }
}
