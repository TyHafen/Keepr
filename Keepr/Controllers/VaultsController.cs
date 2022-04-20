using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public VaultsController(VaultsService vaultsService, KeepsService keepsService)
        {
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
        {
            try
            {

                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                vaultData.Creator = userInfo;
                Vault vault = _vaultsService.Create(vaultData);

                return Ok(vault);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vaultsService.GetById(id);
                if (vault.IsPrivate && userInfo.Id == vault.CreatorId)
                {
                    return Ok(vault);
                }
                if (vault.IsPrivate && userInfo.Id != vault.CreatorId)
                {
                    throw new Exception("you cant see this vault because it is private and is someone elses");
                }
                return Ok(vault);


            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Edit([FromBody] Vault vaultData, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.Id = id;
                vaultData.CreatorId = userInfo.Id;
                Vault vault = _vaultsService.Edit(vaultData);
                return Ok(vault);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteAsync(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _vaultsService.Delete(id, userInfo.Id);
                return Ok("This vault has been deleted");

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeeps(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<VaultKeepViewModel> keeps = _keepsService.GetKeepsByVault(id);
                Vault vault = _vaultsService.GetById(id);
                if (vault.IsPrivate && vault.CreatorId != userInfo?.Id)
                {
                    throw new System.Exception("Cant reach a private keep that isnt yours");
                }
                if (vault.IsPrivate && vault.CreatorId == userInfo?.Id)
                {
                    return Ok(keeps);
                }
                return Ok(keeps);


            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }


        }
    }

}