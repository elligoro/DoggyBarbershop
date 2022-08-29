using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DoggyBarbershop.Models
{
    public class BaseResponse
    {
            public HttpStatusCode Status { get; set; }
            public string Message { get; set; }
    }
}
