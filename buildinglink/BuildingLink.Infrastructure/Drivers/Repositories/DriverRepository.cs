using BuildingLink.Core.Database.Contexts;
using BuildingLink.Core.Database.Mappers;
using BuildingLink.Core.Drivers.Entities;
using BuildingLink.Core.Drivers.Repositories;
using Microsoft.Extensions.Logging;

namespace BuildingLink.Infrastructure.Drivers.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IDbContext _context;
        private readonly ILogger<IDriverRepository> _logger;
        private readonly IEntityMapper _mapper;

        public DriverRepository(
            ILogger<IDriverRepository> logger,
            IDbContext dbContext,
            IEntityMapper mapper
            )
        {
            _logger = logger;
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            _logger.LogInformation($"{nameof(IDriverRepository.DeleteAsync)} : request received with id = {id} ...");

            var currentDriver = await GetByIdAsync(id);

            if (currentDriver == null)
            {
                throw new InvalidOperationException($"{nameof(IDriverRepository.DeleteAsync)} : No driver found by this id {id}.");
            }

            var deleteCommand = "Delete FROM Drivers " +
                                $"WHERE {nameof(Driver.Id)} = '{id}'";

            _logger.LogInformation($"{nameof(IDriverRepository.DeleteAsync)} : delete command = {deleteCommand}");

            var rowsAffected = await _context.ExcuteNonQueryCommandAsync(deleteCommand);

            _logger.LogInformation($"{nameof(IDriverRepository.DeleteAsync)} : delete command excuted with result = {rowsAffected}");

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            _logger.LogInformation($"{nameof(IDriverRepository.GetAllAsync)} : request received...");

            var getCommand = "SELECT * " +
                             "FROM Drivers d";

            _logger.LogInformation($"{nameof(IDriverRepository.GetAllAsync)} : get command = {getCommand}");

            var reader = await _context.ExcuteCommandAsync(getCommand);

            _logger.LogInformation($"{nameof(IDriverRepository.GetAllAsync)} : get command excuted with result = {reader}");

            return _mapper.Map<Driver>(reader);
        }

        public async Task<Driver?> GetByIdAsync(string id)
        {
            _logger.LogInformation($"{nameof(IDriverRepository.GetByIdAsync)} : request received with id = {id}...");

            var getCommand = "SELECT * " +
                             "FROM Drivers d " +
                             $"WHERE d.Id = '{id}'";

            _logger.LogInformation($"{nameof(IDriverRepository.GetByIdAsync)} : get command = {getCommand}");

            var reader = await _context.ExcuteCommandAsync(getCommand);

            _logger.LogInformation($"{nameof(IDriverRepository.GetByIdAsync)} : get command excuted with result = {reader}");

            return _mapper
                .Map<Driver>(reader)
                .SingleOrDefault();
        }

        public async Task<Driver> InsertAsync(Driver driver)
        {
            _logger.LogInformation($"{nameof(IDriverRepository.InsertAsync)} : request received with driver " +
                $"{nameof(Driver.FirstName)} = {driver.FirstName}," +
                $" {nameof(Driver.LastName)} = {driver.LastName}," +
                $" {nameof(Driver.Email)} = {driver.Email}" +
                $" {nameof(Driver.PhoneNumber)} = {driver.PhoneNumber}.");

            var insertCommand = "INSERT INTO Drivers" +
                                $"({nameof(Driver.Id)}, " +
                                $"{nameof(Driver.FirstName)}, " +
                                $"{nameof(Driver.LastName)}," +
                                $"{nameof(Driver.Email)}," +
                                $"{nameof(Driver.PhoneNumber)}," +
                                $"{nameof(Driver.CreatedAt)}," +
                                $"{nameof(Driver.CreatedBy)})" +
                                $"SELECT '{Guid.NewGuid()}'," +
                                $"{driver.FirstName}," +
                                $"{driver.LastName}," +
                                $"{driver.Email}," +
                                $"{driver.PhoneNumber}," +
                                $"{DateTime.Now}, " +
                                $"{nameof(IDriverRepository)};";

            _logger.LogInformation($"{nameof(IDriverRepository.InsertAsync)} : insert command = {insertCommand}");

            var rowsAffected = await _context.ExcuteNonQueryCommandAsync(insertCommand);

            _logger.LogInformation($"{nameof(IDriverRepository.InsertAsync)} : insert command excuted with result = {rowsAffected}");

            return await GetByIdAsync(driver.Id) ?? driver;
        }

        public async Task<Driver> UpdateAsync(Driver driver)
        {
            _logger.LogInformation($"{nameof(IDriverRepository.UpdateAsync)} : request received with driver " +
                $"{nameof(Driver.Id)} = '{driver.Id}'," +
                $" {nameof(Driver.FirstName)} = {driver.FirstName}," +
                $" {nameof(Driver.LastName)} = {driver.LastName}," +
                $" {nameof(Driver.Email)} = {driver.Email}" +
                $" {nameof(Driver.PhoneNumber)} = {driver.PhoneNumber}.");

            var currentDriver = await GetByIdAsync(driver.Id);

            if (currentDriver == null)
            {
                throw new InvalidOperationException($"{nameof(IDriverRepository.UpdateAsync)} : No driver found by this id {driver.Id}.");
            }

            var updateCommand = "UPDATE Drivers " +
                                $"SET {nameof(Driver.FirstName)} = '{driver.FirstName}'," +
                                $"{nameof(Driver.LastName)} = '{driver.LastName}'," +
                                $"{nameof(Driver.Email)} = '{driver.Email}'," +
                                $"{nameof(Driver.PhoneNumber)} = '{driver.PhoneNumber}'," +
                                $"{nameof(Driver.ModifiedBy)} = '{nameof(IDriverRepository)}'," +
                                $"{nameof(Driver.LastModifiedAt)} = '{DateTime.Now}' " +
                                $"WHERE {nameof(Driver.Id)} = '{driver.Id}'";

            _logger.LogInformation($"{nameof(IDriverRepository.UpdateAsync)} : update command = {updateCommand}");


            var rowsAffected = await _context.ExcuteNonQueryCommandAsync(updateCommand);

            _logger.LogInformation($"{nameof(IDriverRepository.UpdateAsync)} : update command excuted with result = {rowsAffected}");

            return await GetByIdAsync(driver.Id) ?? driver;
        }
    }
}