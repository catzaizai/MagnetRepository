using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Domain.Model
{
    public class MegneticModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PieceLength { get; set; }

        public string Ip { get; set; }

        public  string Port { get; set; }

        public string Hash { get; set; }

        public string Hot { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
