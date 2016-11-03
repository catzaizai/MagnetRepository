using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Infrastructure.Repository.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : System.Attribute
    {
        public string Name { get; }

        public TableAttribute(string name)
        {
            Name = name;
        }  
    }
}
