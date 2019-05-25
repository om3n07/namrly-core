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
        private INamrlyService _namrlyService;
        public INamrlyService NamrlyService => this._namrlyService;

        public StartupNameController(INamrlyService namrlyService)
        {
            this._namrlyService = namrlyService;
        }

        [HttpGet]
        public async Task<ActionResult> GetName(
            [FromQuery] string baseWord = null)
        {
            var results = string.Empty;
            if (!string.IsNullOrEmpty(baseWord))
            {
                results = await this.NamrlyService.GetRandomName(baseWord);
            }
            else
            {
                results = await this.NamrlyService.GetRandomName();
            }

            if (results == null) return this.NoContent();
            return this.Ok(results);
        }
    }
}
