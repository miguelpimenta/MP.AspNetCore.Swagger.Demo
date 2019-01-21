using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.AspNetCore.Swagger.Demo.V2.Models;
using System.Net;
using System.Threading.Tasks;

namespace MP.AspNetCore.Swagger.Demo.V2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Produces("Application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Echo test.
        /// </summary>
        /// <returns>Something...</returns>
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet("test")]
        public IActionResult Get()
        {
            return Ok("Ok from controller version 2.0");
        }

        /// <summary>
        /// Testing...
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// <para>Example request:</para>
        /// <para>
        /// {
        ///     "id": 1,
        ///     "msg": "Test Msg...",
        /// }
        /// </para>
        /// </remarks>
        /// <returns>Something...</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Error....</response>
        /// <response code="401">Authorization has been denied for this request</response>
        /// <response code="500">Sometimes things don't work as supposed...</response>
        /// <response code="501">Not implemented</response>
        [AllowAnonymous]
        [Consumes("Application/json")]
        [ProducesResponseType(typeof(TestResp), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        [HttpPost, ActionName("Test")]
        public async Task<IActionResult> Test([FromBody] TestReq input)
        {
            switch (input.Id)
            {
                case 1:
                    var resp = new TestResp
                    {
                        Id = 1,
                        Info = $"The Msg from request is: {input.Msg.ToUpper()}"
                    };

                    return Ok(resp);

                case 2:
                    return BadRequest(new Error { ErrorCode = 400, ErrorMsg = "Error...." });

                case 3:
                    return Unauthorized();

                case 4:
                    return NotFound(new string[] { "404", "Not", "Found" });

                case 5:
                    return StatusCode((int)HttpStatusCode.InternalServerError);

                default:
                    return StatusCode((int)HttpStatusCode.NotImplemented);
            }
        }
    }
}