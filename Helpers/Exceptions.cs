using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Helpers
{
    public class AccountNotAuthorizedException : Exception
    {
        public AccountNotAuthorizedException(string message) : base(message)
        {}
    }
}
