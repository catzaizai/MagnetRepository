using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.Infrastructure.SearchTool;
using NUnit.Framework;

namespace ML.Test
{
    [TestFixture]
    public class SearchTool
    {
        [Test]
        public void CreateIndex()
        {
            SearchHelper.CreateIndex(new SearchModel
            {
                Key = Guid.NewGuid().ToString(),
                Value = "测试jalksdjfx2",
                Content = "asdf测试，C#",
                Hot = 2
            });
            SearchHelper.CreateIndex(new SearchModel
            {
                Key = Guid.NewGuid().ToString(),
                Value = "测试jalksdjfx1",
                Content = "asdf测试，C#",
                Hot = 1
            });
            SearchHelper.CreateIndex(new SearchModel
            {
                Key = Guid.NewGuid().ToString(),
                Value = "测试jalksdjfx3",
                Content = "asdf测试，C#",
                Hot = 3
            });
        }

        [Test]
        public void Search()
        {
            var result = SearchHelper.Search("mp4");
            foreach (var searchModel in result)
            {
                Console.WriteLine(searchModel.Value + " : " + searchModel.Content);
            }
        }

        [OneTimeTearDown]
        public void DeleteAllIndex()
        {
            //SearchHelper.DeleteAllIndex();
        }
    }
}
