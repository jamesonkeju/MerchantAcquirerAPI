using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : BaseController
    {
        private MerchantAcquirerAPIAppContext _context;

        public HomeController(MerchantAcquirerAPI.Data.MerchantAcquirerAPIAppContext context)
        {
            _context = context;
        }
        [HttpGet]

        [ProducesResponseType(typeof(List<TerminalOwner>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTask()
        {

            var s = _context.TerminalOwner.ToList();

            try
            {

                return Ok(s);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

        }

    }
}
