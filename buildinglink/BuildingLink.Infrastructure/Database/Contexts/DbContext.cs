using BuildingLink.Core.Common.Helpers;
using BuildingLink.Core.Database.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BuildingLink.Infrastructure.Database.Contexts
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        { 
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BuildingLinkConnectionString") ?? string.Empty;
            
            InitializeAndSeedDatabseAsync().Wait();
        }

        public async Task<int> ExcuteNonQueryCommandAsync(string command)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sqliteCommand = connection.CreateCommand();
                sqliteCommand.CommandText = command;

                return await sqliteCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<SqliteDataReader> ExcuteCommandAsync(string command)
        {
            var connection = new SqliteConnection(_connectionString);
            
            await connection.OpenAsync();

            var sqliteCommand = connection.CreateCommand();
            sqliteCommand.CommandText = command;

            return await sqliteCommand.ExecuteReaderAsync();
            
        }

        private async Task InitializeAndSeedDatabseAsync()
        {
            var command = @"DROP TABLE Drivers; CREATE TABLE IF NOT EXISTS Drivers (
                            ID TEXT PRIMARY KEY UNIQUE,
                            FirstName TEXT NOT NULL,
                            LastName TEXT NOT NULL,
                            Email TEXT NOT NULL,
                            PhoneNumber TEXT NOT NULL,
                            CreatedAt TEXT NOT NULL,
                            CreatedBy TEXT NOT NULL,
                            LastModifiedAt TEXT NULL,
                            ModifiedBy TEXT NULL);";

            await ExcuteNonQueryCommandAsync(command);

            var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            command = "SELECT * FROM Driver";

            var sqliteCommand = connection.CreateCommand();
            sqliteCommand.CommandText = command;

            var reader = await sqliteCommand.ExecuteReaderAsync();

            if (!reader.Cast<IDataRecord>().Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    sqliteCommand = connection.CreateCommand();
                    sqliteCommand.CommandText = "INSERT INTO Drivers (Id, FirstName, LastName, Email, PhoneNumber, CreatedAt, CreatedBy) " +
                        "                                       VALUES (@id, @firstName, @lastName, @email, @phoneNumber, @createdAt, @createdBy)";

                    sqliteCommand.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    sqliteCommand.Parameters.AddWithValue("@firstName", StringHelpers.RandomText(8));
                    sqliteCommand.Parameters.AddWithValue("@lastName", StringHelpers.RandomText(8));
                    sqliteCommand.Parameters.AddWithValue("@email", $"{StringHelpers.RandomText(8)}@test.com");
                    sqliteCommand.Parameters.AddWithValue("@phoneNumber", $"01{StringHelpers.RandomNumber(9)}");
                    sqliteCommand.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString());
                    sqliteCommand.Parameters.AddWithValue("@createdBy", nameof(IDbContext));

                    sqliteCommand.ExecuteNonQuery();
                }
                
            }
        }
    }
}