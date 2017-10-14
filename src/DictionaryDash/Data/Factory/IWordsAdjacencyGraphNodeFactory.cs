using DictionaryDash.Data.Model;

namespace DictionaryDash.Data.Factory
{
    public interface IWordsAdjacencyGraphNodeFactory
    {
        IWordsAdjacencyGraphNode Create(string word = null);
    }
}
