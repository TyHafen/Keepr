using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultRepo;

        public VaultsService(VaultsRepository vaultRepo)
        {
            _vaultRepo = vaultRepo;
        }

        internal Vault Create(Vault vaultData)
        {
            return _vaultRepo.Create(vaultData);
        }

        internal Vault GetById(int id)
        {
            return _vaultRepo.GetById(id);
        }

        internal Vault Edit(Vault vaultData)
        {
            Vault original = GetById(vaultData.Id);
            if (vaultData.CreatorId != original.CreatorId)
            {
                throw new Exception("You cannot edit some elses Vault");
            }
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            return _vaultRepo.Edit(original);
        }

        internal void Delete(int id, string userId)
        {
            Vault original = GetById(id);
            ValidateOwner(userId, original);
            _vaultRepo.Delete(id);

        }
        private static void ValidateOwner(string userId, Vault data)
        {
            if (userId != data.CreatorId)
            {
                throw new Exception("You cannot delete a keep you did make");
            }
        }
    }
}