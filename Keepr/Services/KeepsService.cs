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
            Keep original = _keepsRepo.GetById(id);
            Edit(original);
            if (original == null)
            {
                throw new Exception("This keep doesnt exist");
            }
            return original;

        }

        internal Keep Edit(Keep keepData)
        {
            Keep original = GetById(keepData.Id);
            if (keepData.CreatorId != original.CreatorId)
            {
                throw new Exception("You cannot edit someone elses keep");
            }
            original.Views++;
            original.Name = keepData.Name ?? original.Name;
            original.Description = keepData.Description ?? original.Description;
            original.Img = keepData.Img ?? original.Img;
            return _keepsRepo.Edit(original);
        }

        internal void Delete(int id, string userId)
        {
            Keep original = GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You cannot delete a keep you did make");
            }

            _keepsRepo.Delete(id);


        }

        internal List<Keep> GetProfileKeeps(string id)
        {
            return _keepsRepo.GetProfileKeeps(id);
        }

        private static void ValidateOwner(string userId, Keep data)
        {
            if (userId != data.CreatorId)
            {
                throw new Exception("You cannot delete a keep you did make");
            }
        }

        internal List<VaultKeepViewModel> GetKeepsByVault(int id)
        {
            return _keepsRepo.GetKeepsByVault(id);
        }
    }
}