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
    }
}