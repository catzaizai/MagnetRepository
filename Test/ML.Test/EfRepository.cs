using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.Infrastructure.Repository;
using ML.Infrastructure.Repository.Attribute;
using ML.Infrastructure.Repository.Entity;
using ML.Infrastructure.Repository.Extensions;
using NUnit.Framework;

namespace ML.Test
{
    [TestFixture]
    public class EfRepository
    {
        public DhtInfoRepository Repository;
        [OneTimeSetUp]
        public void SetUp()
        {
            Repository = new DhtInfoRepository();
        }

        [Test]
        public void GetList()
        {
            var result = Repository.GetList("SELECT * FROM dht_info LIMIT 10");
            PrintList(result);
        }

        [Test]
        public void GetListByPage()
        {
            var result = Repository.GetList(10000, 10);
            PrintList(result);
        }

        [Test]
        public void GetAttributeProperty()
        {
            var name = typeof (DhtInfo).GetAttributeValue((TableAttribute x) => x.Name);
            Console.WriteLine(name);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            
        }

        private void PrintList(List<DhtInfo> result)
        {
            foreach (var dhtInfo in result)
            {
                Console.WriteLine(dhtInfo.Name);
            }
        }

        [Test]
        public void Test()
        {
            var i = 1;
            Console.WriteLine(i);
            Plus(i);
            Console.WriteLine(i);
        }

        private void Plus(int i)
        {
            i++;
        }
    }
}
