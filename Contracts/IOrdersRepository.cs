using DoggyBarbershop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Contracts
{
    public interface IOrdersRepository
    {
        Task<int> Update(int order_id, OrderUpdateRequest request);
        Task<int> Insert(int accountId, OrderUpdateRequest request);
        Task<IEnumerable<OrdersGetModel>> Get();
        Task<int> Delete(int order_id);
    }
}
