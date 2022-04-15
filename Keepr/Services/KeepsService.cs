using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsRepo;

        public KeepsService(KeepsRepository keepsRepo)
        {
            _keepsRepo = keepsRepo;
        }

        internal List<Keep> GetAll()
        {
            return _keepsRepo.GetAll();
        }

        internal Keep Create(Keep data)
        {
            return _keepsRepo.Create(data);
        }

        internal Keep GetById(int id)
        {
            return _keepsRepo.GetById(id);
        }

        internal Keep Edit(Keep keepData)
        {
            Keep original = GetById(keepData.Id);
            if (keepData.CreatorId != original.CreatorId)
            {
                throw new Exception("You cannot edit someone elses keep");
            }
            original.Name = keepData.Name ?? original.Name;
            original.Description = keepData.Description ?? original.Description;
            original.Img = keepData.Img ?? original.Img;
            return _keepsRepo.Edit(original);
        }

        internal void Delete(int id, string userId)
        {
            Keep original = GetById(id);
            ValidateOwner(userId, original);
            _keepsRepo.Delete(id);
        }

        private static void ValidateOwner(string userId, Keep data)
        {
            if (userId != data.CreatorId)
            {
                throw new Exception("You cannot delete a keep you did make");
            }
        }
    }
}