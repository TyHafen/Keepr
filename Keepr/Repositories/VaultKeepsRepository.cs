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
            string sql = @"INSERT INTO vaultKeeps (keepId, vaultId, creatorId) VALUES (@KeepId, @VaultId, @CreatorId); SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, vkData);
            vkData.Id = id;
            return vkData;
        }

        internal VaultKeep Get(int id)
        {
            string sql = @"SELECT * FROM vaultKeeps WHERE vaultKeeps.vaultId = @id";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal void Delete(int id)
        {
            string sql = @" DELETE FROM vaultKeeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}