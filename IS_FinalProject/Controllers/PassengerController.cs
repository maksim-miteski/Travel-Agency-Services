using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Passenger;
using TravelAgency.Services.Abstraction;

namespace IS_FinalProject.Controllers
{
    /// <summary>
    /// Passenger API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService _service;
        /// <summary>
        /// Passenger Constructor API Controller
        /// </summary>
        /// <param name="service">Passenger Service</param>

        public PassengerController(IPassengerService service)
        {
            _service = service;
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
        [HttpGet("Passengers/{id:int}", Name = nameof(GetPassenger))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassengerModelExtended))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPassenger([FromRoute] int id)
        {
            var passenger = await _service.Get(id);
            return passenger != null
                ? (IActionResult)Ok(passenger)
                : NoContent();
        }

        /// <summary>
        /// Get AllPassengers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   GET /api/Passengers
        /// 
        /// </remarks>
        /// <returns>An Base Model Item</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">The item was not Found</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        [HttpGet("Passengers", Name = nameof(GetAllPassengers))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PassengerModelBase>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPassengers()
        {
            var passengers = await _service.Get();
            return passengers != null && passengers.Any()
                ? Ok(passengers)
                : NoContent();
        }

        /// <summary>
        /// Create an Item
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   POST /api/Passenger
        ///   {
        ///    "Name":"string",
        ///    "LastName":"string",
        ///    "PassportNumber":1234567,
        ///    "DoB":"2021-01-01T12:03:32.3445Z"
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
        public async Task<IActionResult> Post([FromBody] PassengerModelCreate model)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Insert(model);
                if (item != null)
                {
                    return CreatedAtRoute(nameof(GetPassenger), item, item.Id);
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
        ///   PUT /api/Airplane/{id}
        ///   {
        ///    "Id":1,
        ///    "Name":"string",
        ///    "LastName":"string",
        ///    "PassportNumber":1234567,
        ///    "DoB":"2021-01-01T12:03:32.3445Z"
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
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassengerModelBase))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PassengerModelUpdate model)
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
        ///   DELETE /api/Passenger/{id}
        ///
        /// </remarks>
        /// <param name="id">identifier of the item</param>
        /// <returns>bool</returns>
        /// <response code="200">All went Well!</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        [HttpDelete("{id:int}")]
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
