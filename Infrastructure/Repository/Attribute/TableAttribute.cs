using System;

namespace Inf.Repository.Attribute
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
