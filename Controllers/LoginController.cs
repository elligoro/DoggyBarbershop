using DoggyBarbershop.Contracts;
using DoggyBarbershop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("ajaxPolicy")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginLogic _loginLogic;

        public LoginController(ILogger<LoginController> logger, ILoginLogic ordersLogic)
        {
            _logger = logger;
            _loginLogic = ordersLogic;
        }

        [Route("/login")]
        [HttpPost]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            _logger.LogDebug($"trying to Login with user: {request.Username}");
            return await _loginLogic.Login(request);
        }

        [Route("/Signup")]
        [HttpPost]
        public async Task<LoginResponse> Signup(SignupRequest request)
        {
            _logger.LogDebug($"trying to Signup with new user: {request.Username}");
            return await _loginLogic.Signup(request);
        }
    }
}
