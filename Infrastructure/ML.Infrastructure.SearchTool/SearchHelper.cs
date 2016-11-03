using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace ML.Infrastructure.SearchTool
{
    public class SearchHelper
    {
        private static string _indexPath;
        private const int MaxLength = 100;

        private static FSDirectory _writerDirectory;
        private static IndexWriter _writer;

        private static FSDirectory _readerDirectory;
        private static IndexReader _reader;
        private static IndexSearcher _searcher;

        private static readonly bool IsExists = IndexReader.IndexExists(WriterDirectory);

        public static string IndexPath => _indexPath ?? (_indexPath = AppDomain.CurrentDomain.BaseDirectory + @"\Index");

        private static FSDirectory WriterDirectory => _writerDirectory ?? (_writerDirectory = FSDirectory.Open(new DirectoryInfo(IndexPath)));

        private static IndexWriter Writer
            =>
                _writer ??
                (_writer =
                    new IndexWriter(WriterDirectory, new PanGuAnalyzer(), !IsExists, IndexWriter.MaxFieldLength.UNLIMITED));

        private static FSDirectory ReaderDirectory
            =>
                _readerDirectory ??
                (_readerDirectory = FSDirectory.Open(new DirectoryInfo(IndexPath), new NoLockFactory()));

        private static IndexReader Reader => _reader ?? (_reader = IndexReader.Open(ReaderDirectory, true));

        private static IndexSearcher Searcher => _searcher ?? (_searcher = new IndexSearcher(Reader));

        public static bool CreateIndex(SearchModel model)
        {

            var document = new Document();
            document.Add(new Field("key", model.Key, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("value", model.Value, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("content", model.Content, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("hot", model.Hot.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("size", model.Size.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            Writer.DeleteDocuments(new Term("key", model.Key));
            Writer.AddDocument(document);
            Writer.Commit();
            return true;
        }

        public static IEnumerable<SearchModel> Search(string keyWord)
        {
            var result = new List<SearchModel>();
            var query = new PhraseQuery();
            query.Add(new Term("value", keyWord));
            query.Slop = MaxLength;
            var sort = new Sort(new SortField("hot", SortField.INT, true));
            var filter = new QueryWrapperFilter(query);
            var docs = Searcher.Search(query, filter, 1000, sort).ScoreDocs;
            foreach (var scoreDoc in docs)
            {
                var model = new SearchModel();
                var document = Searcher.Doc(scoreDoc.Doc);
                model.Key = document.Get("key");
                model.Content = document.Get("content");
                model.Value = document.Get("value");
                int hot, size;
                int.TryParse(document.Get("hot"), out hot);
                int.TryParse(document.Get("size"), out size);
                model.Hot = hot;
                model.Size = size;
                result.Add(model);
            }
            return result;
        }

        public static void DeleteAllIndex()
        {
            Writer.DeleteAll();
            Writer.Commit();
        }

        public static void DeleteIndex(string key)
        {
            Writer.DeleteDocuments(new Term("key", key));
            Writer.Commit();
        }

        public static void Close()
        {
            Writer.Dispose();
            Reader.Dispose();
            WriterDirectory.Dispose();
            ReaderDirectory.Dispose();
            Searcher.Dispose();
        }
    }
}
