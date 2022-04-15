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
    }
}