using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Models
{
    public class LoginResponse : BaseResponse
    {
        public LoginData Data { get; set; }
    }

    public class LoginData
    {
        public int AccountId { get; set; }
        public string AccessToken { get; set; }
        public int expires { get; set; }
        public string RedirectTo { get; set; }
    }
}
