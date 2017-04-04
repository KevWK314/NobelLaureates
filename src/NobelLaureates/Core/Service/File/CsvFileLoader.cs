using FileHelpers;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace NobelLaureates.Core.Service.File
{
    public class CsvFileLoader : ICsvFileLoader
    {
        public T[] LoadFile<T>(string filename) where T : class
        {
            return LoadFileImpl<T>(filename);
        }

        private T[] LoadFileImpl<T>(string filename) where T : class
        {
            var engine = new FileHelperEngine<T>();
            engine.Encoding = System.Text.Encoding.UTF8;
            return engine.ReadFile(filename);
        }
    }
}
