﻿// (c) Nick Polyak 2018 - http://awebpros.com/
// License: Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0.html)
//
// short overview of copyright rules:
// 1. you can use this framework in any commercial or non-commercial 
//    product as long as you retain this copyright message
// 2. Do not blame the author of this software if something goes wrong. 
// 
// Also, please, mention this software in any documentation for the 
// products that use it.

using System;

namespace NP.Concepts.Behaviors
{
    public class Removable : IRemovable
    {
        public event Action<IRemovable> RemoveEvent;

        public void Remove()
        {
            RemoveEvent?.Invoke(this);
        }
    }


    public interface IRemovableWrapper
    {
        Removable TheRemovable { get; }
    }
}
