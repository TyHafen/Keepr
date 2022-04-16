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

        internal void Delete(int id1, string id2)
        {
            throw new NotImplementedException();
        }
    }
}