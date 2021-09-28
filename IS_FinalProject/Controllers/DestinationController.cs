using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Agent;
using TravelAgency.Models.Models.Airplane;
using TravelAgency.Models.Models.Destination;
using TravelAgency.Models.Models.Passenger;
using TravelAgency.Services.Abstraction;

namespace IS_FinalProject.Controllers
{
    /// <summary>
    /// Destination API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationService _service;
        /// <summary>
        /// Destination Constructor API Controller
        /// </summary>
        /// <param name="service">Destination Service</param>
        public DestinationController(IDestinationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get Agent By Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   GET /api/Agent/{id}
        /// 
        /// </remarks>
        /// <param name="id">Identifier of the item</param>
        /// <returns>An Extended Model Item</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">The item was not Found</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        [HttpGet("Agents/{id:int}/Traveled", Name = nameof(GetAgents))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentModelExtended))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgents([FromRoute] int id)
        {
            var agent = await _service.GetAgentById(id);
            return agent != null
                ? (IActionResult)Ok(agent)
                : NoContent();
        }

        /// <summary>
        /// Get Airplanes By Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   GET /api/Airplanes/{id}
        /// 
        /// </remarks>
        /// <param name="id">Identifier of the item</param>
        /// <returns>An Extended Model Item</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">The item was not Found</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        [HttpGet("Airplanes/{id:int}/Traveled", Name = nameof(GetAirplanes))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AirplaneModelExtended))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAirplanes([FromRoute] int id)
        {
            var plane = await _service.GetAirplaneById(id);
            return plane != null
                ? (IActionResult)Ok(plane)
                : NoContent();
        }

        /// <summary>
        /// Get Passenger By Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   GET /api/Passengers/{id}
        /// 
        /// </remarks>
        /// <param name="id">Identifier of the item</param>
        /// <returns>An Extended Model Item</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">The item was not Found</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        [HttpGet("Passengers/{id:int}/Traveled", Name = nameof(GetPassengers))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassengerModelExtended))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPassengers([FromRoute] int id)
        {
            var passenger = await _service.GetPassengerById(id);
            return passenger != null
                ? (IActionResult)Ok(passenger)
                : NoContent();
        }

        /// <summary>
        /// Create an Item
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   POST /api/Destination
        ///   {
        ///    "PassportNumber":1234567,
        ///    "departureDate":"2021-01-01T12:03:32.3445Z",
        ///    "AgentId":1,
        ///    "PassengerId":1,
        ///    "AirplaneId":1
        ///   }
        /// 
        /// </remarks>
        /// <param name="model">Model to Create</param>
        /// <returns>Identifier of the created item</returns>
        /// <response code="201">Path of the created item</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="400">The Item is null</response>
        /// <response code="409">The Item was not created</response>
        /// <response code="500">Server side error</response>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] DestinationModelCreate model)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Insert(model);
                if (item != null)
                {
                    return Created($"/api/Destination/{item.Id}",item.Id);
                }
                return Conflict();
            }
            return BadRequest();
        }

        /// <summary>
        /// Update an Item
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   PUT /api/Destination/{id}
        ///   {
        ///    "Id":1,
        ///    "PassportNumber":1234567,
        ///    "departureDate":"2021-01-01T12:03:32.3445Z",
        ///    "AgentId":1,
        ///    "PassengerId":1,
        ///    "AirplaneId":1
        ///   }
        /// 
        /// </remarks>
        /// <param name="id">identifier of the item</param>
        /// <param name="model">model to update</param>
        /// <returns>Airplane base model</returns>
        /// <response code="200">All went Well!</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="400">The Item is null</response>
        /// <response code="409">The Item was not created</response>
        /// <response code="500">Server side error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassengerModelBase))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] DestinationModelUpdate model)
        {
            if (ModelState.IsValid)
            {
                model.Id = id;
                var item = await _service.Update(model);
                return item != null
                ? Ok(item)
                : NoContent();
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete an Item
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   DELETE /api/Destination/{id}
        ///
        /// </remarks>
        /// <param name="id">identifier of the item</param>
        /// <returns>bool</returns>
        /// <response code="200">All went Well!</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                Ok(await _service.Delete(id));
            }
            return BadRequest();
        }
    }
}
