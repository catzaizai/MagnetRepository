using System;
using ML.Infrastructure.Repository.Attribute;

namespace ML.Infrastructure.Repository.Entity
{
    [Table("dht_info")]
    public class DhtInfo : BaseEntity<int>
    {
        public string PieceLength { get; set; }

        public string Pieces { get; set; }

        public string Address { get; set; }

        public string Port { get; set; }
        
        public string InfoHash { get; set; }

        public int Hot { get; set; }

        public string Magnet { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
