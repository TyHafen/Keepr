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
    }
}