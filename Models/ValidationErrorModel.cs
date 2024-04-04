using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagerAPI.Models
{
    

    public class ValidationErrorModel
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public ValidationErrorModel(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}