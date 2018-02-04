namespace StringSearch.Core
{
    public interface IPuzzleEntry
    {
        string HiddenValue { get; }
        string DisplayValue { get; }
    }
}