using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep vkData)
        {
            string sql = @"INSERT INTO vaultKeeps (keepId, vaultId) VALUES (@KeepId, @VaultId); SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, vkData);
            vkData.Id = id;
            return vkData;
        }
    }
}