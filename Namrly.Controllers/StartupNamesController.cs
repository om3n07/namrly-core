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
        private INamrlyService _namrlyService;

        public INamrlyService NamrlyService => this._namrlyService;

        public StartupNamesController(INamrlyService namrlyService)
        {
            this._namrlyService = namrlyService;
        }


        [HttpGet]
        public async Task<ActionResult> GetNames([FromQuery] string baseWord = null, [FromQuery] int? numResults = null)
        {
            ICollection<string> results;
            if (!string.IsNullOrEmpty(baseWord))
            {
                results = await this.NamrlyService.GetRandomNames(baseWord, numResults ?? 1);
            }
            else
            {
                results = await this.NamrlyService.GetRandomNames(numResults ?? 1);
            }

            if (results == null) return NoContent();

            return this.Ok(results);
        }
    }
}
