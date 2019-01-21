using Microsoft.AspNetCore.Mvc;

namespace MP.AspNetCore.Swagger.Demo.V1.Models
{
    [ApiVersion("1.0")]
    public class TestResp
    {
        #region Properties

        /// <summary>
        /// Get/Set Operation
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Get/Set Result
        /// </summary>
        public string Info { get; set; } = string.Empty;

        #endregion Properties
    }
}