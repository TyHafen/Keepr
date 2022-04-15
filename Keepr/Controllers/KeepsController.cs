using System.Collections.Generic;
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
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;

        public KeepsController(KeepsService keepsService)
        {
            _keepsService = keepsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep data)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = userInfo.Id;
                data.Creator = userInfo;
                Keep keep = _keepsService.Create(data);
                return Ok(keep);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);

            }
        }

        [HttpGet("{id}")]
        public ActionResult<Keep> GetById(int id)
        {
            try
            {
                Keep keep = _keepsService.GetById(id);
                return Ok(keep);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        public ActionResult<List<KeepsController>> GetAll()
        {
            try
            {
                List<Keep> keeps = _keepsService.GetAll();
                return Ok(keeps);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Edit([FromBody] Keep keepData, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepData.Id = id;
                keepData.CreatorId = userInfo.Id;
                Keep keep = _keepsService.Edit(keepData);
                return Ok(keep);
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
                _keepsService.Delete(id, userInfo.Id);
                return Ok("Keep has been deleted");
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }




    }
}