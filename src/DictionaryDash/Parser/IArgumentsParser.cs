namespace DictionaryDash.Parser
{
    public interface IArgumentsParser
    {
        string PathToInputFile { get; set; }
        void HandleArguments(string[] args);
    }
}
