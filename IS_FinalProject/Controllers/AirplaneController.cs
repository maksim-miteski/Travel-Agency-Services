using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Airplane;
using TravelAgency.Services.Abstraction;

namespace IS_FinalProject.Controllers
{
    /// <summary>
    /// Airplane API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AirplaneController : ControllerBase
    {
        private readonly IAirplaneService _service;
        /// <summary>
        /// Airplane Constructor API Controller
        /// </summary>
        /// <param name="service">Airplane Service</param>
        public AirplaneController(IAirplaneService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get Airplane By Id
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
        [HttpGet("Airplanes/{id:int}", Name = nameof(GetAirplane))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AirplaneModelExtended))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAirplane([FromRoute] int id)
        {
            var airplane = await _service.Get(id);
            return airplane != null
                ? (IActionResult)Ok(airplane)
                : NoContent();
        }

        /// <summary>
        /// Get AllAirplanes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   GET /api/Airplanes
        /// 
        /// </remarks>
        /// <returns>An Base Model Item</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">The item was not Found</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        [HttpGet("Airplanes", Name = nameof(GetAllAirplanes))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AirplaneModelBase>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAirplanes()
        {
            var airplanes = await _service.Get();
            return airplanes != null && airplanes.Any()
                ? Ok(airplanes)
                : NoContent();
        }

        /// <summary>
        /// Create an Item
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   POST /api/Airplane
        ///   {
        ///    "FlightNumber":111,
        ///    "maxCapacity":120
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
        public async Task<IActionResult> Post([FromBody] AirplaneModelCreate model)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Insert(model);
                if (item != null)
                {
                    return CreatedAtRoute(nameof(GetAirplane), item, item.Id);
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
        ///    "FlightNumber":111,
        ///    "maxCapacity":120
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AirplaneModelBase))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AirplaneModelUpdate model)
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
        ///   DELETE /api/Airplane/{id}
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
