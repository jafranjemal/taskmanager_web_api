using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagerAPI.Models
{
    public class ErrorResponseModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

}