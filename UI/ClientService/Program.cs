using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Inf.Repository;
using Inf.Repository.Entity;
using Inf.SearchTool;
using Newtonsoft.Json;

namespace UI.ClientService
{
    class Program
    {
        private static readonly DhtInfoRepository Repository = new DhtInfoRepository();

        private static readonly string TempFilePath = AppDomain.CurrentDomain.BaseDirectory + @"temp/";

        private static readonly string FilePath = TempFilePath + "pageIndex.txt";

        private static bool _isStop = false;

        private static int _pageSize;

        private static int PageSize
        {
            get
            {
                if (_pageSize == 0)
                {
                    var pageSize = ConfigurationManager.AppSettings["PageSize"];
                    _pageSize = !string.IsNullOrEmpty(pageSize) ? Convert.ToInt32(pageSize) : 1000;
                }
                return _pageSize;
            }
        }

        static void Main(string[] args)
        {
            Start();
        }

        private static async void Start()
        {
            Console.WriteLine("Service is started!");           
            do
            {
                _isStop = await CreateIndex();
            } while (_isStop);
        }

        private static async Task<bool> CreateIndex()
        {
            var pageIndex = ReadTempFile();
            List<DhtInfo> result;
            try
            {
                result = Repository.GetList(pageIndex, _pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
            if (result.Count < PageSize)
            {
                Console.WriteLine("all of done!!");
                return true;
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
            if (!File.Exists(FilePath))
            {
                return 1;
            }
            var line = File.ReadAllLines(FilePath, Encoding.UTF8);
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
            File.WriteAllText(FilePath, pageIndex.ToString(), Encoding.UTF8);
        }


    }
}
