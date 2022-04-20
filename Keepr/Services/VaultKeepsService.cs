using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkRepo;
        private readonly VaultsRepository _vaultsRepo;
        private readonly KeepsRepository _keepsRepo;

        public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsRepository vaultsRepo, KeepsRepository keepsRepo)
        {
            _vkRepo = vkRepo;
            _vaultsRepo = vaultsRepo;
            _keepsRepo = keepsRepo;
        }

        internal VaultKeep Create(VaultKeep vkData, string userId)
        {
            Vault vaultToCheck = _vaultsRepo.GetById(vkData.VaultId);
            VaultKeep isVault = _vkRepo.Validate(vkData);

            if (vaultToCheck.CreatorId != userId || isVault != null)
            {
                throw new Exception("cant add a keep to a vault that is not yours");
            }
            Keep keepToIncrement = _keepsRepo.GetById(vkData.KeepId);
            _keepsRepo.IncrementKeep(keepToIncrement);
            return _vkRepo.Create(vkData);


        }

        internal void Delete(int id, string userId)
        {
            VaultKeep found = _vkRepo.GetById(id);
            if (found.CreatorId != userId)
            {
                throw new Exception("Not yourssss vaultKeep to delete");
            }
            Keep keep = _keepsRepo.GetById(found.KeepId);
            _keepsRepo.DecrementKeep(keep);
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