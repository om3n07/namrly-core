using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Namrly.Services;
using Microsoft.AspNetCore.Mvc;

namespace Namrly.Controllers
{
    [Route("api/[controller]")]
    public class RelatedStartupNamesController : Controller
    {
        private INamrlyService _namrlyService;
        public INamrlyService NamrlyService => this._namrlyService;

        public RelatedStartupNamesController(INamrlyService namrlyService)
        {
            this._namrlyService = namrlyService;
        }

        [HttpGet]
        [Route("{baseWord}")]
        public async Task<ActionResult> GetNames([FromRoute] string baseWord, [FromQuery] int numResults = 1)
        {
            if (numResults <= 0) return this.BadRequest("numResults must be greater than 0");
            numResults = Math.Min(numResults, 25);

            var results = await this.NamrlyService.GetRelatedStartupNames(baseWord, numResults);
            if (results == null || results.Count == 0) return NotFound();

            return this.Ok(results);
        }
    }
}
