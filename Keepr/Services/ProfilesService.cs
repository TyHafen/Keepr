using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _pRepo;

        public ProfilesService(ProfilesRepository pRepo)
        {
            _pRepo = pRepo;
        }

        internal Account GetById(string id)
        {
            Account found = _pRepo.GetById(id);
            if (found == null)
            {
                throw new Exception("No profile with that id");
            }
            return found;
        }
    }
}