using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Infrastructure.Repository.Entity
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

        public string Name { get; set; }
    }
}
