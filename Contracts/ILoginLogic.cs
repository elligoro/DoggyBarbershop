using DoggyBarbershop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Contracts
{
    public interface ILoginLogic
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<LoginResponse> Signup(SignupRequest request);

    }
}
