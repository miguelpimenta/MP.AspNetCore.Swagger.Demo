using Microsoft.AspNetCore.Mvc;

namespace MP.AspNetCore.Swagger.Demo.V2.Models
{
    [ApiVersion("2.0")]
    public class TestReq
    {
        #region Properties

        /// <summary>
        /// Get/Set Operation
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Get/Set Result
        /// </summary>
        public string Msg { get; set; } = string.Empty;

        #endregion Properties
    }
}