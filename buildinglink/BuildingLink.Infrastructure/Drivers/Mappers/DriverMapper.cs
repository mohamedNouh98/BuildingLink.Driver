using BuildingLink.Core.Drivers.Commands;
using BuildingLink.Core.Drivers.DataTransfer;
using BuildingLink.Core.Drivers.Entities;
using BuildingLink.Core.Drivers.Mappers;
using Microsoft.Data.Sqlite;

namespace BuildingLink.Infrastructure.Drivers.Mappers
{
    public class DriverMapper : IDriverMapper
    {
        public IEnumerable<DriverDto> Map(IEnumerable<Driver> drivers)
        {
            return drivers.Select(driver => new DriverDto()
            {
                Email = driver.Email,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                PhoneNumber = driver.PhoneNumber,
            }).AsEnumerable();
        }

        public DriverDto Map(Driver driver)
        {
            return new DriverDto()
            {
                Email = driver.Email,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                PhoneNumber = driver.PhoneNumber,
            };
        }

        public Driver Map(InsertDriverCommand cmd)
        {
            return new Driver()
            {
                Email = cmd.Email,
                FirstName = cmd.FirstName,
                LastName = cmd.LastName,
                PhoneNumber = cmd.PhoneNumber,
            };
        }

        public Driver Map(UpdateDriverCommand cmd)
        {
            return new Driver()
            {
                Id = cmd.Id,
                Email = cmd.Email,
                FirstName = cmd.FirstName,
                LastName = cmd.LastName,
                PhoneNumber = cmd.PhoneNumber,
            };
        }
    }
}