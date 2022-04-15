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

        public VaultsController(VaultsService vaultsService)
        {
            _vaultsService = vaultsService;
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
        public ActionResult<Vault> GetById(int id)
        {
            try
            {
                Vault vault = _vaultsService.GetById(id);
                return Ok(vault);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }




    }

}