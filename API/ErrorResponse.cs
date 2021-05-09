using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {

        }
        public ErrorResponse(int statusCode,string userMessage)
        {
            IsError = true;
            UserMessage = userMessage;
            StatusCode = statusCode;
        }
        public bool IsError { get; set; }
        public string DeveloperMessage { get; set; }
        public string UserMessage { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
