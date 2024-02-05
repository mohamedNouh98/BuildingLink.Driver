using BuildingLink.Core.Database.Contexts;
using BuildingLink.Core.Database.Mappers;
using BuildingLink.Core.Drivers.Entities;
using BuildingLink.Core.Drivers.Repositories;
using BuildingLink.Infrastructure.Database.Contexts;
using BuildingLink.Infrastructure.Drivers.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Data;
using System.Reflection.PortableExecutable;

namespace BuildingLink.Core.Tests
{
    public class IDriverRepositoryTests
    {
        private readonly IDbContext _context;
        private readonly ILogger<IDriverRepository> _logger;
        private readonly IEntityMapper _mapper;

        private readonly IDriverRepository _sut;

        public IDriverRepositoryTests()
        {
            _context = Substitute.For<IDbContext>();
            _mapper = Substitute.For<IEntityMapper>();
            _logger = Substitute.For<ILogger<IDriverRepository>>();

            _sut = new DriverRepository(_logger, _context, _mapper);
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            // Arrange
            var drivers = new List<Driver>()
            {
                new(),
                new()
            };
            var reader = Substitute.For<IDataReader>() as SqliteDataReader;
            var command = "Command";

            _context
                .ExcuteCommandAsync(command)
                .Returns(reader);

            _mapper
                .Map<Driver>(reader)
                .Returns(drivers);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(drivers.Count, result.Count()); 
        }

        [Fact]
        public async Task GetByIdAsyncTest()
        {
            // Arrange
            var drivers = new List<Driver>()
            {
                new()
            };
            var reader = Substitute.For<IDataReader>() as SqliteDataReader;
            var command = "Command";
            var id = "Id";

            _context
                .ExcuteCommandAsync(command)
                .Returns(reader);

            _mapper
                .Map<Driver>(reader)
                .Returns(drivers);

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task InsertAsyncTest()
        {
            // Arrange
            var id = "Id";

            var driver = new Driver()
            {
                Id = id,
            };
            var drivers = new List<Driver>()
            {
                driver
            };

            var command = "Command";
            var reader = Substitute.For<IDataReader>() as SqliteDataReader;

            _context
                .ExcuteNonQueryCommandAsync(command)
                .Returns(1);

            _context
                .ExcuteCommandAsync(command)
                .Returns(reader);

            _mapper
                .Map<Driver>(reader)
                .Returns(drivers);

            // Act
            var result = await _sut.InsertAsync(driver);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task UpdateAsyncTest()
        {
            // Arrange
            var id = "Id";

            var driver = new Driver()
            {
                Id = id,
            };
            var drivers = new List<Driver>()
            {
                driver
            };

            var command = "Command";
            var reader = Substitute.For<IDataReader>() as SqliteDataReader;

            _context
                .ExcuteNonQueryCommandAsync(command)
                .Returns(1);

            _context
                .ExcuteCommandAsync(command)
                .Returns(reader);

            _mapper
                .Map<Driver>(reader)
                .Returns(drivers);

            // Act
            var result = await _sut.UpdateAsync(driver);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            // Arrange
            var id = "Id";

            var driver = new Driver()
            {
                Id = id,
            };
            var drivers = new List<Driver>()
            {
                driver
            };

            var command = "Command";
            var reader = Substitute.For<IDataReader>() as SqliteDataReader;

            _context
                .ExcuteNonQueryCommandAsync(Arg.Any<string>())
                .Returns(1);

            _context
                .ExcuteCommandAsync(command)
                .Returns(reader);

            _mapper
                .Map<Driver>(reader)
                .Returns(drivers);

            // Act
            var result = await _sut.DeleteAsync(id);

            // Assert
            Assert.True(result);
        }
    }
}