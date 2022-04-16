using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetAll()
        {
            string sql = @"SELECT k.*, a.* FROM keeps k JOIN accounts a ON k.creatorId = a.id; ";
            return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
            {
                keep.Creator = account;
                return keep;
            }).ToList();

        }

        internal Keep GetById(int id)
        {
            string sql = @"SELECT k.*, a.*
             FROM keeps k 
             JOIN accounts a 
             ON k.creatorId = a.id
             WHERE k.id = @id; ";
            return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
            {
                keep.Creator = account;
                return keep;
            }, new { id }).FirstOrDefault();
        }

        internal Keep Create(Keep data)
        {
            string sql = @"
            INSERT INTO keeps (name, description, img, views, kept, creatorId)
             VALUES (@Name, @Description, @Img, @Views, @Kept ,@CreatorId); SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, data);
            data.Id = id;
            return data;
        }

        internal List<Keep> GetProfileKeeps(string id)
        {
            string sql = @"
            SELECT k.* FROM keeps k WHERE k.creatorId = @id;";
            return _db.Query<Keep>(sql, new { id }).ToList();
        }

        internal Keep Edit(Keep original)
        {
            string sql = @"UPDATE keeps SET 
            name = @Name, 
            description = @Description, 
            img = @Img, 
            kept = @Kept,
             views = @Views
             WHERE id = @Id";
            int rows = _db.Execute(sql, original);
            if (rows > 0)
            {
                return original;
            }
            throw new Exception("SQL error on editting a keep. no row was affected");
        }

        internal List<VaultKeepViewModel> GetKeepsByVault(int id)
        {
            string sql = @"
            SELECT
            a.*,
             k.*,
            vk.id AS VaultKeepId 
            FROM vaultKeeps vk 
            JOIN keeps k on k.id = vk.keepId 
            JOIN accounts a ON a.id = k.creatorId
            WHERE vk.VaultId = @id;";
            return _db.Query<Account, VaultKeepViewModel, VaultKeepViewModel>(sql, (a, vkvm) =>
            {
                vkvm.Creator = a;
                return vkvm;
            }, new { id }).ToList();
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}