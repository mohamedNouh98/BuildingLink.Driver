using BuildingLink.Core.Drivers.Commands;
using BuildingLink.Core.Drivers.DataTransfer;
using BuildingLink.Core.Drivers.Entities;

namespace BuildingLink.Core.Drivers.Mappers
{
    public interface IDriverMapper
    {
        /// <summary>
        /// Map list of driver model to DTO models
        /// </summary>
        /// <param name="drivers">Drivers list to be mapped</param>
        /// <returns>List of mapped objects</returns>
        IEnumerable<DriverDto> Map(IEnumerable<Driver> drivers);

        /// <summary>
        /// Map driver model to DTO model
        /// </summary>
        /// <param name="driver">driver model to be mapped</param>
        /// <returns>Mapped DTO object</returns>
        DriverDto Map(Driver driver);

        /// <summary>
        /// Map driver command to DTO model
        /// </summary>
        /// <param name="cmd">driver command to be mapped</param>
        /// <returns>Mapped driver object</returns>
        Driver Map(InsertDriverCommand cmd);

        /// <summary>
        /// Map driver command to DTO model
        /// </summary>
        /// <param name="cmd">driver command to be mapped</param>
        /// <returns>Mapped driver object</returns>
        Driver Map(UpdateDriverCommand cmd);
    }
}