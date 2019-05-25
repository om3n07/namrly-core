using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Namrly.Services;
using Microsoft.AspNetCore.Mvc;

namespace Namrly.Controllers
{
    [Route("api/[controller]")]
    public class StartupNamesController : Controller
    {
        private NamrlyService _namrlyService;

        public NamrlyService NamrlyService => this._namrlyService != null ? this._namrlyService : this._namrlyService = new NamrlyService();


        [HttpGet]
        public async Task<ActionResult> GetNames(
            [FromQuery] string baseWord = null,
            [FromQuery] bool includeAdditionalSuffixes = false,
             [FromQuery] int? numResults = null)
        {
            IEnumerable<string> results;
            if (!string.IsNullOrEmpty(baseWord)) {
                results = await this.NamrlyService.GetRandomNames(baseWord, numResults ?? 1, includeAdditionalSuffixes);
            } else {
                results = await this.NamrlyService.GetRandomNames( numResults ?? 1, includeAdditionalSuffixes);
            }

            if (results == null) return NoContent();

            return this.Ok(results);
        }
    }
}
