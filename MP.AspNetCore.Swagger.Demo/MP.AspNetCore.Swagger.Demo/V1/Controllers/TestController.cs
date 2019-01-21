using Microsoft.AspNetCore.Mvc;
using MP.AspNetCore.Swagger.Demo.V1.Models;
using System.Net;
using System.Threading.Tasks;

namespace MP.AspNetCore.Swagger.Demo.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.5", Deprecated = true)]
    [Produces("Application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Test.
        /// </summary>
        /// <returns>Something...</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [HttpGet("test1")]
        public IActionResult Get1()
        {
            return Ok("Ok from controller version 1.0");
        }

        /// <summary>
        /// Test.
        /// </summary>
        /// <returns>Something...</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [MapToApiVersion("1.5")]
        [HttpGet("test2")]
        public IActionResult Get2()
        {
            return Ok("Ok from controller version 1.5");
        }

        /// <summary>
        /// Testing...
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Something...</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Error....</response>
        /// <response code="401">Authorization has been denied for this request</response>
        /// <response code="500">Sometimes things don't work as supposed...</response>
        [ProducesResponseType(typeof(TestResp), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotImplemented)]
        [HttpGet("test/{input}")]
        public async Task<IActionResult> Test([FromRoute] int input)
        {
            switch (input)
            {
                case 1:
                    var resp = new TestResp
                    {
                        Id = 1,
                        Info = "Ok from controller version 1.X"
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