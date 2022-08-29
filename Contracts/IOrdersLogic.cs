using DoggyBarbershop.Logic;
using DoggyBarbershop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Contracts
{
    public interface IOrdersLogic
    {
        Task<OrdersResponse> Get(int accountId);

        /*
        Task<OrdersResponse> Update(int accountId, OrderUpdateRequest request);
        Task<OrdersResponse> Insert(int accountId, OrderUpdateRequest request);
        */

        Task<OrdersResponse> Upsert(int accountId, OrderUpdateRequest request);
        Task<OrdersResponse> Delete(int orderId, int accountId);
    }
}
