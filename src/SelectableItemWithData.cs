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

using NP.Utilities;

namespace NP.Concepts.Behaviors
{
    public class SelectableItemWithData<T> : 
        SelectableItem<SelectableItemWithData<T>>
    {
        public T Data { get; }

        public SelectableItemWithData(T data)
        {
            Data = data;
        }

        public override bool Equals(object obj)
        {
            if (obj is SelectableItemWithData<T> selectableItemWithData)
            {
                return Data.ObjEquals(selectableItemWithData.Data);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Data.GetHashCodeExtension();
        }

        public override string ToString()
        {
            return Data?.ToString();
        }

        public static implicit operator
            SelectableItemWithData<T>(T value)
        {
            return new SelectableItemWithData<T>(value);
        }

        public static implicit operator
            T(SelectableItemWithData<T> selectableItem)
        {
            return selectableItem.Data;
        }
    }

    public static class SelectableItemWithDataUtils
    {
        public static SelectableItemWithData<T> ToSelectableItem<T>(this T data)
        {
            return new SelectableItemWithData<T>(data);
        }
    }
}
