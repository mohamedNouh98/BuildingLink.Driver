using BuildingLink.Core.Drivers.Commands;
using BuildingLink.Core.Drivers.Mappers;
using BuildingLink.Core.Drivers.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BuildingLink.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private readonly IDriverMapper _mapper;
        private readonly IDriverRepository _repository;

        public DriversController(
            IDriverRepository repository,
            IDriverMapper mapper,
            ILogger<DriversController> logger
            )
        { 
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation($"{nameof(DriversController.GetAllAsync)} Request recieved ...");

            var result = await _repository.GetAllAsync();

            _logger.LogInformation($"{nameof(DriversController.GetAllAsync)} returned result {result}");

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            _logger.LogInformation($"{nameof(DriversController.GetByIdAsync)} Request recieved ...");

            var result = await _repository.GetByIdAsync(id);
            
            _logger.LogInformation($"{nameof(DriversController.GetByIdAsync)} returned result {result}");

            return result!= null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Route("")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertAsync([FromBody] InsertDriverCommand command)
        {
            _logger.LogInformation($"{nameof(DriversController.InsertAsync)} Request recieved {command} ...");

            var newDriver = _mapper.Map(command);

            var result = await _repository.InsertAsync(newDriver);

            _logger.LogInformation($"{nameof(DriversController.InsertAsync)} returned result {result}");

            return Created(nameof(DriversController.InsertAsync), result);
        }

        [HttpPut]
        [Route("")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDriverCommand command)
        {
            _logger.LogInformation($"{nameof(DriversController.UpdateAsync)} Request recieved {command} ...");

            var updatedDriver = _mapper.Map(command);

            var result = await _repository.UpdateAsync(updatedDriver);

            _logger.LogInformation($"{nameof(DriversController.UpdateAsync)} returned result {result}");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            _logger.LogInformation($"{nameof(DriversController.DeleteAsync)} Request recieved {id} ...");

            var result = await _repository.DeleteAsync(id);

            _logger.LogInformation($"{nameof(DriversController.DeleteAsync)} returned result {result}");

            return result ? Ok(result) : BadRequest("Cannot delete the driver");
        }
    }
}
