using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.Infrastructure.Repository;
using ML.Infrastructure.SearchTool;

namespace ML.UI.ClientService
{
    class Program
    {
        private static DhtInfoRepository Repository = new DhtInfoRepository();

        private static readonly string TempFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\temp";

        private static bool _isStop = false;

        private const int PageSize = 100;

        static void Main(string[] args)
        {
            Start();

        }

        private static async void Start()
        {
            Console.WriteLine("Task is start!");
            do
            {
                _isStop = await CreateIndex();
            } while (_isStop);
        }

        private static async Task<bool> CreateIndex()
        {
            var pageIndex = ReadTempFile();
            var result = Repository.GetList(pageIndex, PageSize);
            if (result.Count < PageSize)
            {
                Console.WriteLine("all of done!!");
                return false;
            }
            foreach (var dhtInfo in result)
            {
                Console.WriteLine("now create the key of ---> " + dhtInfo.Id + "：" + dhtInfo.Name);
                var model = new SearchModel
                {
                    Key = dhtInfo.InfoHash,
                    Content = dhtInfo.Address + dhtInfo.Port + dhtInfo.UpdateTime,
                    Hot = dhtInfo.Hot,
                    Size = Convert.ToInt32(dhtInfo.PieceLength),
                    Value = dhtInfo.Name
                };
                SearchHelper.CreateIndex(model);
            }
            WriteTempFile(pageIndex + 1);
            return true;
        }

        private static int ReadTempFile()
        {
            if (!Directory.Exists(TempFilePath))
            {
                Directory.CreateDirectory(TempFilePath);
                return 1;
            }
            if (!File.Exists(TempFilePath + @"\tem.txt"))
            {
                return 1;
            }
            var line = File.ReadAllLines(TempFilePath + @"\tem.txt", Encoding.UTF8);
            int pageIndex;
            int.TryParse(line[0], out pageIndex);
            return pageIndex;
        }

        private static void WriteTempFile(int pageIndex)
        {
            if (!Directory.Exists(TempFilePath))
            {
                Directory.CreateDirectory(TempFilePath);
            }
            File.WriteAllText(TempFilePath + @"\tem.txt", pageIndex.ToString(), Encoding.UTF8);
        }
    }
}
