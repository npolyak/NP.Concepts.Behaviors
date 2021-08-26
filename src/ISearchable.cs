// (c) Nick Polyak 2021 - http://awebpros.com/
// License: MIT License (https://opensource.org/licenses/MIT)
//
// short overview of copyright rules:
// 1. you can use this framework in any commercial or non-commercial 
//    product as long as you retain this copyright message
// 2. Do not blame the author of this software if something goes wrong. 
// 
// Also, please, mention this software in any documentation for the 
// products that use it.

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
