using BuildingLink.Core.Drivers.Entities;

namespace BuildingLink.Core.Drivers.Repositories
{
    public interface IDriverRepository
    {
        /// <summary>
        /// Get all drivers stored from drivers table in Databse
        /// </summary>
        /// <returns>List of Driver models</returns>
        Task<IEnumerable<Driver>> GetAllAsync();

        /// <summary>
        /// Get driver by provided id
        /// </summary>
        /// <param name="id">Id of the driver to get it's data.</param>
        /// <returns>Object from Driver model</returns>
        Task<Driver?> GetByIdAsync(string id);

        /// <summary>
        /// Update driver model
        /// </summary>
        /// <param name="driver">The driver model to be updated</param>
        /// <returns>New driver model after updating</returns>
        Task<Driver> UpdateAsync(Driver driver);

        /// <summary>
        /// Insert driver model
        /// </summary>
        /// <param name="driver">The driver model to be inserted</param>
        /// <returns>New driver model</returns>
        Task<Driver> InsertAsync(Driver driver);

        /// <summary>
        /// Delete driver by id
        /// </summary>
        /// <param name="id">Id of the driver to be deleted</param>
        /// <returns>Boolean indicate if the process is done or not</returns>
        Task<bool> DeleteAsync(string id);
    }
}