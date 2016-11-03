using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Infrastructure.SearchTool
{
    public class SearchModel
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string Content { get; set; }

        public int Hot { get; set; }

        public int Size { get; set; }
    }
}
