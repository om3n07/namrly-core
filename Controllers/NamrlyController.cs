using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace namrly.Controllers
{
    [Route("api/[controller]")]
    public class NamrlyController : Controller
    {
        private NamrlyService _namrlyService;

        public NamrlyService NamrlyService => this._namrlyService != null ? this._namrlyService : this._namrlyService = new NamrlyService();


        [HttpGet]
        public async Task<IEnumerable<string>> GetNames(
            [FromQuery] string nameBase = null,
            [FromQuery] bool includeAdditionalSuffixes = false,
             [FromQuery] int? numResults = null)
        {
            if (!string.IsNullOrEmpty(nameBase)) {
                return await this.NamrlyService.GetRandomNames(nameBase, includeAdditionalSuffixes, numResults ?? 1);
            } else {
                return await this.NamrlyService.GetRandomNames(includeAdditionalSuffixes, numResults ?? 1);
            }
        }
    }
}
