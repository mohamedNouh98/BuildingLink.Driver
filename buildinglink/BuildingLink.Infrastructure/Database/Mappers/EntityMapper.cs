using BuildingLink.Core.Common.Entities;
using BuildingLink.Core.Database.Attributes;
using BuildingLink.Core.Database.Mappers;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BuildingLink.Infrastructure.Database.Mappers
{
    public class EntityMapper : IEntityMapper
    {
        private readonly ILogger<IEntityMapper> _logger;
        
        public EntityMapper(ILogger<IEntityMapper> logger)
        {
            _logger = logger;
        }
        
        public IEnumerable<T> Map<T>(SqliteDataReader reader) where T : BaseEntity, new()
        {
            _logger.LogInformation($"{nameof(IEntityMapper)} : {nameof(IEntityMapper.Map)} request recieved ...");

            IList<T> collection = new List<T>();
            while (reader.Read())
            {
                var obj = new T();
                foreach (PropertyInfo i in obj.GetType().GetProperties()
                .Where(p => p.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(DbColumnAttribute)) != null).ToList())
                {

                    try
                    {
                        var ca = i.GetCustomAttribute(typeof(DbColumnAttribute));

                        if (ca != null)
                        {
                            if (((DbColumnAttribute)ca).Convert == true)
                            {
                                if (reader[i.Name] != DBNull.Value)
                                    i.SetValue(obj, Convert.ChangeType(reader[i.Name], i.PropertyType));
                            }
                            else
                            {
                                if (reader[i.Name] != DBNull.Value)
                                    i.SetValue(obj, reader[i.Name]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{nameof(IEntityMapper)} : {nameof(IEntityMapper.Map)} exception thrown - {ex.Message}");

                        throw ex;
                    }
                }
                collection.Add(obj);
            }
            return collection;
        }
    }
}
