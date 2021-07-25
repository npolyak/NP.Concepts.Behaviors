namespace NP.Concepts.Behaviors
{
    public interface ISearchable
    {
        string OriginalSearchableStr { get; }

        string SearchableStr { get; }

        string SearchStr { get; set; }
    }

    public static class SearchableUtils
    {
        public static bool SearchStrFound(this ISearchable searchable)
        {
            return string.IsNullOrWhiteSpace(searchable?.SearchStr) ||
                    searchable.SearchableStr?.Contains(searchable.SearchStr) == true;
        }
    }
}
