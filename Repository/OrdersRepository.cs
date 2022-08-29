using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DoggyBarbershop.Contracts;
using DoggyBarbershop.Models;

namespace DoggyBarbershop.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private IDbConnection _db;
        public OrdersRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<int> Delete(int order_id)
        {
            return (await _db.ExecuteAsync($"DELETE FROM [dbo].[Orders] WHERE Id = @Id", new { Id = order_id }));
        }

        public async Task<IEnumerable<OrdersGetModel>> Get()
        {
            return (await _db.QueryAsync<OrdersGetModel>($"[dbo].[GetOrders]", commandType: CommandType.StoredProcedure));
        }

        public async Task<int> Insert(int accountId, OrderUpdateRequest request)
        {
            return (await _db.ExecuteAsync($"[dbo].[InsertOrder]", new { AccountId = accountId, BookingDate = request.BookingDate }
                                                                                    ,commandType: CommandType.StoredProcedure));
        }

        public async Task<int> Update(int order_id, OrderUpdateRequest request)
        {
            return (await _db.ExecuteAsync($"[dbo].[UpdateOrder]", new { Id = order_id, BookingDate = request.BookingDate }
                                                                        ,commandType: CommandType.StoredProcedure));
        }
    }
}
