using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultservice;

        public VaultsController(VaultsService vaultservice)
        {
            _vaultservice = vaultservice;
        }
    }
}