using DoggyBarbershop.Contracts;
using DoggyBarbershop.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using DoggyBarbershop.Helpers;
using System.Net;

namespace DoggyBarbershop.Logic
{
    public class LoginLogic : ILoginLogic
    {
        private readonly ILogger<LoginLogic> _logger;
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _config;
        public LoginLogic(ILogger<LoginLogic> logger, ILoginRepository loginRepository, IConfiguration config)
        {
            _logger = logger;
            _loginRepository = loginRepository;
            _config = config;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                if (!ValidateRequiredFields(request))
                    return await Task.FromResult(new LoginResponse { Status = HttpStatusCode.BadRequest, Message = $"Missing account field data" });

                var dbAccount = await _loginRepository.GetAccountByUsername(request.Username);
                if (dbAccount is null)
                    return await Task.FromResult(new LoginResponse { Status = HttpStatusCode.BadRequest, Message = $"One of the fileds could not be found" });
                if (!ValidatePassword(HashPassword(request.Password), dbAccount.Password))
                    return await Task.FromResult(new LoginResponse { Status = HttpStatusCode.BadRequest, Message = $"One of the fileds could not be found" });

                var accessToken = GenerateAccessToken(dbAccount.Id);
                await _loginRepository.UpdateAccessToken(dbAccount.Id, accessToken);

                return await Task.FromResult(new LoginResponse
                {
                    Status = HttpStatusCode.OK,
                    Message = "Successfuly verified account",
                    Data = GenerateLoginResponse(dbAccount.Id, accessToken)
                });
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = $"{ex.Message}, trace: {ex.StackTrace}"
                };
            }
        }
        public async Task<LoginResponse> Signup(SignupRequest request)
        {
            try
            {
                if (!ValidateRequiredFields(request) || string.IsNullOrEmpty(request.FirstName))
                    return await Task.FromResult(new LoginResponse { Status = HttpStatusCode.BadRequest, Message = $"Missing account field data" });

                var userByUsername = await _loginRepository.GetAccountByUsername(request.Username);
                if (!(userByUsername is null))
                    return await Task.FromResult(new LoginResponse { Status = HttpStatusCode.BadRequest, Message = $"Username is already taken" });

                var hashedPassword = HashPassword(request.Password);
                int accountId = await _loginRepository.InsertAccount(request, hashedPassword);
                var accessToken = GenerateAccessToken(accountId);

                await _loginRepository.UpdateAccessToken(accountId, accessToken);

                return await Task.FromResult(new LoginResponse
                {
                    Status = HttpStatusCode.OK,
                    Message = "Successfuly created account",
                    Data = GenerateLoginResponse(accountId, accessToken)
                });
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = $"{ex.Message}, trace: {ex.StackTrace}"
                };
            }
        }

        private LoginData GenerateLoginResponse(int accountId, string accessToken)
        {
            return new LoginData
            {
                AccessToken = accessToken,
                AccountId = accountId,
                expires = (int)_config.GetValue(typeof(int), "AccessToken:CookieTimeMin"),
                RedirectTo = (string)_config.GetValue(typeof(string), "AccessToken:RedirectTo")
            };
        }

        private bool ValidateRequiredFields(LoginRequest request) => !(string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password));

        private bool ValidatePassword(string password, string dbPassword) 
        {
            var hashedPassword = HashPassword(password);
            return string.Equals(password, dbPassword);
        }

        private string GenerateAccessToken(int userId) => EncryptionHandler.Encript(userId.ToString());

        private string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
