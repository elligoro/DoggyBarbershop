using DoggyBarbershop.Contracts;
using DoggyBarbershop.Controllers;
using DoggyBarbershop.Helpers;
using DoggyBarbershop.Models;
using DoggyBarbershop.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DoggyBarbershop.Logic
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var repo = filterContext.HttpContext.RequestServices.GetService(typeof(ILoginRepository)) as LoginRepository;
            var auth = filterContext.HttpContext.Request.Headers["Authorization"].ToString();
            int accountId;
            try
            {
                accountId = EncryptionHandler.DecryptAccessToken(auth);
            }catch(AccountNotAuthorizedException ex)
            {
                    filterContext.Result = new JsonResult(new BaseResponse
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = $"{ex.Message}"
                    });

                return;
             }
            
            var isAccountExists = repo.IsAccountExists(accountId).Result;
            if(isAccountExists)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new JsonResult(new BaseResponse
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = $"could not authenticate account"
                });
            }
        }
    }
}