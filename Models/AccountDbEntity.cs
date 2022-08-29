using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Models
{
    public class AccountDbEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
    }
}
