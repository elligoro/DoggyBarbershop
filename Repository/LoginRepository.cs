using DoggyBarbershop.Contracts;
using DoggyBarbershop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DoggyBarbershop.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private IDbConnection _db;
        public LoginRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }
        public async Task<AccountDbEntity> GetAccountByUsername(string username)
        {
            return (await _db.QueryAsync<AccountDbEntity>("[dbo].[GetAccountByUsername]", new { username }, commandType: CommandType.StoredProcedure))
                    .FirstOrDefault();
        }

        public async Task<bool> IsAccountExists(int accountId)
        {
            return (await _db.ExecuteScalarAsync<bool>("SELECT TOP 1 1 FROM [dbo].[Accounts] WHERE Id = @Id", new { Id = accountId }));
        }

        public async Task<int> InsertAccount(SignupRequest account, string hashedPassword)
        {
            var accountId =  (await _db.QueryAsync<int>("[dbo].[InsertAccount]", 
                              new { FirstName = account.FirstName, UserName = account.Username, Password = hashedPassword }, 
                              commandType: CommandType.StoredProcedure))
                             .Single();
            return accountId;
        }

        public async Task UpdateAccessToken(int accountId, string accessToken)
        {
            await _db.ExecuteAsync("[dbo].[UpdateAccessToken]", new { accountId, accessToken }, commandType: CommandType.StoredProcedure);
        }
    }
}
