//
// ObservableCollectionExtended.cs
//
// Author:
//       Bart King <github@cleardemon.com>
//
// Copyright (c) 2016 Bart King
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ClearDemon.Utility.Data
{
    public class ObservableCollectionExtended<T> : ObservableCollection<T>
    {
        public ObservableCollectionExtended() { }

        public ObservableCollectionExtended(IEnumerable<T> items) : base(items) { }

        public void AddRange(IEnumerable<T> items)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            // create shallow copy of the enumerable items to prevent multiple iterations
            var itemList = items.ToList();
            foreach(var i in itemList)
                Items.Add(i);

            if(itemList.Count > 0)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemList));
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            var itemsRemoved = new List<T>();
            foreach(var item in items)
            {
                if(Items.Remove(item))
                    itemsRemoved.Add(item);
            }

            if(itemsRemoved.Count > 0)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, itemsRemoved));
        }

        public void ReplaceRange(IEnumerable<T> items)
        {
            var oldItems = Items.ToList();
            Items.Clear();

            foreach(var item in items)
                Items.Add(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItems, Items));
        }

        /// <summary>
        /// Replaces the collection with a single item, raising the changed notification once.
        /// </summary>
        /// <param name="item">Item.</param>
        public void Replace(T item)
        {
            if(EqualityComparer<T>.Default.Equals(item, default(T)))
                throw new ArgumentNullException(nameof(item));

            ReplaceRange(new[] { item });
        }

        /// <summary>
        /// Replaces a single item in the collection at a specific index, raising a changed notification for it.
        /// </summary>
        /// <remarks>Useful to update a single item in the collection without rebuilding the entire list.</remarks>
        /// <param name="item">Item.</param>
        /// <param name="index">Index.</param>
        public void ReplaceAt(T item, int index)
        {
            if(EqualityComparer<T>.Default.Equals(item, default(T)))
                throw new ArgumentNullException(nameof(item));
            if(index < 0 || index >= Items.Count)
                throw new IndexOutOfRangeException();

            var oldItem = Items[index];
            Items[index] = item;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItem, item));
        }

        /// <summary>
        /// Raises a change notification for the entire collection. Useful for when mass changes have been made to the items for databinding.
        /// </summary>
        public void ChangedAll()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, Items, Items));
        }

        /// <summary>
        /// Raises a change notification for a single item in the collection that has been modified. Useful for databound items that cannot pass back this change to the containing list.
        /// </summary>
        /// <param name="item">Item that changed. Does nothing if the item is not in the collection.</param>
        public void Changed(T item)
        {
            try
            {
                if(Items.Contains(item))
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, item));
            }
            catch(ArgumentOutOfRangeException)
            {
            }
        }
    }
}

