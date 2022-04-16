using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

        public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
        {
            _profilesService = profilesService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }

        [HttpGet("{id}")]
        public ActionResult<AccountController> GetById(string id)
        {
            try
            {
                Account profile = _profilesService.GetById(id);
                return Ok(profile);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetProfileKeeps(string id)
        {
            try
            {
                List<Keep> keeps = _keepsService.GetProfileKeeps(id);
                return (keeps);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/vaults")]
        public ActionResult<List<Keep>> GetProfileVaults(string id)
        {
            try
            {
                List<Vault> vaults = _vaultsService.GetProfileVaults(id);
                return Ok(vaults);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}