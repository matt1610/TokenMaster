﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenMaster.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic AdditionalData { get; set; }

        public ApiResponse(bool success, string message, dynamic additionalData = null)
        {
            Success = success;
            Message = message;
            AdditionalData = additionalData;
        }
    }
}