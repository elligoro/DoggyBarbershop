using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Models
{
    public class SignupRequest : LoginRequest
    {
        public string FirstName { get; set; }
    }
}
