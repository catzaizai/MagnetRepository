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
    }
}
