using DoggyBarbershop.Contracts;
using DoggyBarbershop.Helpers;
using DoggyBarbershop.Logic;
using DoggyBarbershop.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersLogic _ordersLogic;
        private int _accountId;

        public OrdersController(ILogger<OrdersController> logger, IOrdersLogic ordersLogic)
        {
            _logger = logger;
            _ordersLogic = ordersLogic;
        }

        [HttpGet]
        [Auth]
        public async Task<OrdersResponse> Get()
        {
            _logger.LogDebug($"trying to get all orders");
            var accountId = GetAccountIdFromAuth();
            return await _ordersLogic.Get(accountId);
        }
        /*
        [HttpPut]
        [Auth]
        [Route("/order")]
        public async Task<OrdersResponse> Update(OrderUpdateRequest request)
        {
            var accountId = GetAccountIdFromAuth();
            _logger.LogDebug($"trying to update order of account {accountId}");
            return await _ordersLogic.Update(accountId, request);
        }
        */

        [HttpPost]
        [Auth]
        [Route("/order")]
        public async Task<OrdersResponse> Insert(OrderUpdateRequest request)
        {
            var accountId = GetAccountIdFromAuth();
            return await _ordersLogic.Upsert(accountId, request);
        }

        [HttpDelete]
        [Auth]
        [Route("/order/{orderId}")]
        public async Task<OrdersResponse> Delete(int orderId)
        {
            _logger.LogDebug($"trying to delete order: {orderId}");
            var accountId = GetAccountIdFromAuth();
            return await _ordersLogic.Delete(orderId, accountId);
        }

        private int GetAccountIdFromAuth()
        {
            var auth = HttpContext.Request.Headers["Authorization"].ToString();
            auth = auth.Substring("bearer ".Length);
            int.TryParse(EncryptionHandler.Decript(auth), out int accountId);
            return accountId;
        }
    }
}
