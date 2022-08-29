using DoggyBarbershop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Contracts
{
    public interface ILoginRepository
    {
        Task<AccountDbEntity> GetAccountByUsername(string username);
        Task<int> InsertAccount(SignupRequest account, string hashedPassword);
        Task UpdateAccessToken(int accountId,string accessToken);
        Task<bool> IsAccountExists(int accountId);
    }
}
