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
    }
}