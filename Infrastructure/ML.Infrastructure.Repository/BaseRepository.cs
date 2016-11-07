using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ML.Infrastructure.Repository.Attribute;
using ML.Infrastructure.Repository.Entity;
using ML.Infrastructure.Repository.Extensions;
using MySql.Data.MySqlClient;

namespace ML.Infrastructure.Repository
{
    public class BaseRepository<TEntity>
    {
        protected const string ConnectionStr = "Database=p2pspider;Data Source=108.171.211.103;User Id=root;Password=123456@sw;pooling=false;CharSet=utf8;port=3306;";

        public List<TEntity> GetList(string sql)
        {
            using (var conn = new MySqlConnection(ConnectionStr))
            {
                conn.Open();
                return conn.Query<TEntity>(sql).ToList();
            }
        }

        public List<TEntity> GetList(int index, int pageSize)
        {
            var tblName = typeof(DhtInfo).GetAttributeValue((TableAttribute x) => x.Name);
            if (--index < 0) index = 0;
            using (var conn = new MySqlConnection(ConnectionStr))
            {
                conn.Open();
                return conn.Query<TEntity>("SELECT * FROM "+ tblName +" WHERE id > "+ index * pageSize + " ORDER BY id LIMIT " + pageSize).ToList();
            }
        } 
    }
}
