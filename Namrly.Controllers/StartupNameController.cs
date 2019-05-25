using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Namrly.Services;
using Microsoft.AspNetCore.Mvc;

namespace Namrly.Controllers
{
    [Route("api/[controller]")]
    public class StartupNameController : Controller
    {
        private NamrlyService _namrlyService;
        public NamrlyService NamrlyService => this._namrlyService != null ? this._namrlyService : this._namrlyService = new NamrlyService();




        [HttpGet]
        public async Task<ActionResult> GetName(
            [FromQuery] string baseWord = null,
            [FromQuery] bool includeAdditionalSuffixes = false)
        {
            var results = string.Empty;
            if (!string.IsNullOrEmpty(baseWord)) 
            {
                results = await this.NamrlyService.GetRandomName(baseWord, includeAdditionalSuffixes);
            } 
            else
            {
                results = await this.NamrlyService.GetRandomName(includeAdditionalSuffixes);
            }

            if (results == null) return this.NoContent();
            return this.Ok(results);
        }
    }
}
