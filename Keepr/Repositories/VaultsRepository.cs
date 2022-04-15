using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault Create(Vault vaultData)
        {
            string sql = @"INSERT INTO vaults (name, description, creatorId, isPrivate) VALUES (@Name, @Description, @CreatorId, @IsPrivate); SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }

        internal Vault GetById(int id)
        {
            string sql = @"SELECT v.*, a.* 
            FROM vaults v
             JOIN accounts a 
             ON v.creatorId = a.id 
             WHERE v.id = @id;";
            return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
            {
                vault.Creator = account;
                return vault;
            }, new { id }).FirstOrDefault();
        }
    }
}