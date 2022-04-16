using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkRepo;

        public VaultKeepsService(VaultKeepsRepository vkRepo)
        {
            _vkRepo = vkRepo;
        }

        internal VaultKeep Create(VaultKeep vkData)
        {
            return _vkRepo.Create(vkData);
        }

        internal void Delete(int id, string userId)
        {
            VaultKeep found = Get(id);
            if (found.CreatorId != userId)
            {
                throw new Exception("Not yourssss vaultKeep to delete");
            }
            _vkRepo.Delete(id);
        }
        private VaultKeep Get(int id)
        {
            VaultKeep found = _vkRepo.Get(id);
            if (found == null)
            {
                throw new Exception("invalid Id");
            }
            return found;
        }
    }
}