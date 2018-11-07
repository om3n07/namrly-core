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

        // // GET api/values
        // [HttpGet]
        // public async Task<IEnumerable<string>> GetName()
        // {
        //     var name = await this.NamrlyService.GetRandomName();
        //     return new string[] { string.Format("{0}ly", name) };
        // }

        [HttpGet]
        public async Task<IEnumerable<string>> GetNames([FromQuery] string nameBase, [FromQuery] int? numResults = 0)
        {
            return new string[] { await this.NamrlyService.GetRandomName() };
        }

        // GET api/values/5
        // [HttpGet("{id}")]
        // public string GetName(int id)
        // {
        //     return "value";
        // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
