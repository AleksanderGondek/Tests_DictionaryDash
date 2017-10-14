using System.Collections.Generic;

namespace DictionaryDash.Data.Utils
{
    public interface IFileWrapper
    {
        bool Exists(string pathToFile);
        IEnumerable<string> ReadLines(string pathToFile);
    }
}
