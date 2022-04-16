using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vkService;

        public VaultKeepsController(VaultKeepsService vkService)
        {
            _vkService = vkService;
        }

        [HttpPost]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vkData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vkData.CreatorId = userInfo.Id;
                VaultKeep created = _vkService.Create(vkData);
                return Ok(created);

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                _vkService.Delete(id, user.Id);
                return Ok("You deleted the VaultKeep");

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}