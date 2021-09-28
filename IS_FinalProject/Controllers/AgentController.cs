using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Agent;
using TravelAgency.Services.Abstraction;

namespace IS_FinalProject.Controllers
{
    /// <summary>
    /// Agent API Controller
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _service;
        /// <summary>
        /// Agent Constructor API Controller
        /// </summary>
        /// <param name="service">Agent Service</param>
        public AgentController(IAgentService service)
        {
            _service = service;
        }
      
        /// <summary>
        /// Get Agent By Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   GET /api/Agents/{id}
        /// 
        /// </remarks>
        /// <param name="id">Identifier of the item</param>
        /// <returns>An Extended Model Item</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">The item was not Found</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        //api/Agents/id
        [HttpGet("Agents/{id:int}",Name =nameof(GetAgent))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentModelExtended))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgent([FromRoute] int id)
        {
            var agent =await _service.Get(id);
            return agent != null
                ?(IActionResult)Ok(agent)
                : NoContent();
        }
       
      
        /// <summary>
        /// Get AllAgent
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   GET /api/Agents
        /// 
        /// </remarks>
        /// <returns>An Base Model Item</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">The item was not Found</response>
        /// <response code="400">The Item is null</response>
        /// <response code="500">Server side error</response>
        //api/Agents/id
        [HttpGet("Agents", Name = nameof(GetAllAgents))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AgentModelBase>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAgents()
        {
            var agents =await _service.Get();
            return agents != null  &&  agents.Any()
                ? Ok(agents)
                : NoContent();
        }

        /// <summary>
        /// Create an Item
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///   POST /api/Agent
        ///   {
        ///    "Name":"string",
        ///    "LastName":"string",
        ///    "Title":"string",
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
        public async Task<IActionResult> Post([FromBody] AgentCreateModel model)
        {
            if(ModelState.IsValid)
            {
                var item = await _service.Insert(model);
                if(item != null)
                {
                    // return CreatedAtRoute(nameof(GetAgent), item, item.Id);
                    return Created($"/api/Agents/{item.Id}", item.Id);
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
        ///   PUT /api/Agent/{id}
        ///   {
        ///    "Id":1,
        ///    "Name":"string",
        ///    "LastName":"string",
        ///    "Title":"string",
        ///    "DoB":"2021-01-01T12:03:32.3445Z"
        ///   }
        /// 
        /// </remarks>
        /// <param name="id">identifier of the item</param>
        /// <param name="model">model to update</param>
        /// <returns>player base model</returns>
        /// <response code="200">All went Well!</response>
        /// <response code="405">Method not allowed</response>
        /// <response code="400">The Item is null</response>
        /// <response code="409">The Item was not created</response>
        /// <response code="500">Server side error</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentModelBase))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute]int id,[FromBody] AgentUpdateModel model)
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
        ///   DELETE /api/Agent/{id}
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
