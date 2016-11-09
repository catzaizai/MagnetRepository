using System.Collections.Generic;
using System.Linq;
using Dapper;
using Inf.Repository.Attribute;
using Inf.Repository.Entity;
using Inf.Repository.Extensions;
using MySql.Data.MySqlClient;

namespace Inf.Repository
{
    public class BaseRepository<TEntity>
    {
        private const string DefaultConnectionStr = "Database=p2pspider;Data Source=localhost;User Id=root;Password=root;pooling=false;CharSet=utf8;port=3306;";
        private string _connectionStr;

        private string ConnectionStr
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionStr))
                {
                    _connectionStr = System.Configuration.ConfigurationManager.AppSettings["DbConnectionStr"] ??
                                 DefaultConnectionStr;
                }               
                return _connectionStr;
            }
        }

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
