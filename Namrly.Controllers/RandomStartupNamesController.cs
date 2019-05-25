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
    public class RandomStartupNamesController : Controller
    {
        private INamrlyService _namrlyService;
        public INamrlyService NamrlyService => this._namrlyService;

        public RandomStartupNamesController(INamrlyService namrlyService)
        {
            this._namrlyService = namrlyService;
        }

        [HttpGet]
        public async Task<ActionResult> GetName([FromQuery] int numResults = 1)
        {
            if (numResults <= 0) return this.BadRequest("numResults must be greater than 0");
            numResults = Math.Min(numResults, 25);

            var results = await this.NamrlyService.GetRandomStartupNames(numResults);
            if (results == null || results.Count == 0) return this.NoContent();

            return this.Ok(results);
        }
    }
}
