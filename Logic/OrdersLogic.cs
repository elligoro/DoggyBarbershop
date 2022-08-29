using DoggyBarbershop.Contracts;
using DoggyBarbershop.Helpers;
using DoggyBarbershop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DoggyBarbershop.Logic
{
    public class OrdersLogic: IOrdersLogic
    {
        private IOrdersRepository _ordersRepository;
        public OrdersLogic(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<OrdersResponse> Delete(int orderId, int accountId)
        {
            try
            {
                if (!(await IsOrderBelongsToAccount(accountId, orderId)))
                    throw new AccountNotAuthorizedException($"Order: {orderId} does not belong to accout of Id: {accountId}");
                await _ordersRepository.Delete(orderId);
                return new OrdersResponse
                {
                    Status = HttpStatusCode.OK,
                    Orders = (await _ordersRepository.Get()).ToList(),
                    Message = $"Successfully deleted an order {orderId}"
                };
            }
            catch(AccountNotAuthorizedException ex)
            {
                return new OrdersResponse
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = $"{ex.Message}, trace: {ex.StackTrace}"
                };
            }
            catch (Exception ex)
            {
                return new OrdersResponse
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = $"{ex.Message}, trace: {ex.StackTrace}"
                };
            }
        }

        public async Task<OrdersResponse> Get(int accountId)
        {
            try
            {
                var ordersAll = await _ordersRepository.Get();
                return new OrdersResponse { Status = HttpStatusCode.OK, Orders = ordersAll?.ToList(), AccountId = accountId };
            }catch(Exception ex)
            {
                return new OrdersResponse
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = $"{ex.Message}, trace: {ex.StackTrace}"
                };
            }
        }

        public async Task<OrdersResponse> Upsert(int accountId, OrderUpdateRequest request) => (request.OrderId.HasValue)
                                                                                                ? await Update(accountId, request)
                                                                                                : await Insert(accountId, request);

        private async Task<OrdersResponse> Insert(int accountId, OrderUpdateRequest request)
        {
            try
            {
                if (await _ordersRepository.Insert(accountId, request) > 0)
                {

                    return new OrdersResponse
                    {
                        Status = HttpStatusCode.OK,
                        Orders = (await _ordersRepository.Get()).ToList(),
                        Message = "Successfully created an order",
                        AccountId = accountId
                    };
                }
                throw new Exception($"could not insert order");

            }catch (Exception ex) {
                return new OrdersResponse
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = $"{ex.Message}, trace: {ex.StackTrace}"
                };
            }
        }

        private async Task<OrdersResponse> Update(int accountId, OrderUpdateRequest request)
        {
            var orderId = request.OrderId.Value;
            try
            {
                if (!(await IsOrderBelongsToAccount(accountId, orderId)))
                throw new AccountNotAuthorizedException($"Order: {orderId} does not belong to accout of Id: {accountId}");

                if (await _ordersRepository.Update(orderId, request) > 0)
                {
                    return new OrdersResponse
                    {
                        Status = HttpStatusCode.OK,
                        Orders = (await _ordersRepository.Get()).ToList(),
                        Message = $"Successfully updated the order {orderId}",
                        AccountId = accountId
                    };
                }
                throw new Exception($"could not update order {orderId}");
            }
            catch (AccountNotAuthorizedException ex)
            {
                return new OrdersResponse
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = $"{ex.Message}, trace: {ex.StackTrace}"
                };
            }
            catch (Exception ex)
            {
                return new OrdersResponse
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = $"{ex.Message}, trace: {ex.StackTrace}"
                };
            }
        }

        private async Task<bool> IsOrderBelongsToAccount(int accountId, int orderId)
        {
            return (await _ordersRepository.Get())?.FirstOrDefault(o => o.AccountId == accountId && o.OrderId == orderId) != null;
        }
    }
}
