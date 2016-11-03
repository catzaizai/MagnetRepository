using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ML.Infrastructure.SearchTool;

namespace ML.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MegneticController : Controller
    {
        [HttpGet("{keyword}")]
        public IEnumerable<SearchModel> Get(string keyword)
        {
            return SearchHelper.Search(keyword);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
