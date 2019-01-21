﻿using Microsoft.AspNetCore.Mvc;

namespace MP.AspNetCore.Swagger.Demo.V1.Models
{
    [ApiVersion("1.0")]
    public class Error
    {
        #region Properties

        /// <summary>
        /// Get/Set Error Code
        /// </summary>
        public int ErrorCode { get; set; } = 0;

        /// <summary>
        /// Get/Set Error Msg
        /// </summary>
        public string ErrorMsg { get; set; } = string.Empty;

        #endregion Properties
    }
}