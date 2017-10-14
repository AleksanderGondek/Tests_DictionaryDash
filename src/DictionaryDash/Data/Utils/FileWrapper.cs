using System.Collections.Generic;
using System.IO;

namespace DictionaryDash.Data.Utils
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public IEnumerable<string> ReadLines(string pathToFile)
        {
            return File.ReadLines(pathToFile);
        }
    }
}
