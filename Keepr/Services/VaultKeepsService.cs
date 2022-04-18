using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkRepo;
        private readonly VaultsRepository _vaultsRepo;

        public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsRepository vaultsRepo)
        {
            _vkRepo = vkRepo;
            _vaultsRepo = vaultsRepo;
        }

        internal VaultKeep Create(VaultKeep vkData, string userId)
        {
            Vault vaultToCheck = _vaultsRepo.GetById(vkData.VaultId);
            if (vaultToCheck.CreatorId != userId)
            {
                throw new Exception("cant add a keep to a vault that is not yours");
            }
            return _vkRepo.Create(vkData);


        }

        internal void Delete(int id, string userId)
        {
            VaultKeep found = _vkRepo.GetById(id);
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
                throw new Exception("invalid Id ya");
            }
            return found;
        }
    }
}