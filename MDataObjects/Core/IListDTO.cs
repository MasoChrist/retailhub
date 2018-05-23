using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace MDataObjects
{

    public enum ItemState:int
    {
        AddedOrUpdated = 1,
        Removed = 2
    }
    public class ListItemState<TDTO>
    {
        public  TDTO Item { get; set; }
        public  ItemState State { get; set; }

    }
    /// <summary>
    /// this Implementation of ICollection will hold  a list of all the items ever inserted
    /// all deleted items will be stored in DeletedElements
    /// every DTO should use these lists to track the changes to commit in the datasource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DTOStatedList<T>: IList<T> where T:IDTO<IKey>
    {
        private List<T> _deletedElements = new List<T>();
        private List<T> _updatedItems = new List<T>();
        /// <summary>
        /// Wraps the elements on a StatedList
        /// </summary>
        public List<ListItemState<T>> StatedList
        {
            get
            {
                return _deletedElements.Select(x => new ListItemState<T> {Item = x, State = ItemState.Removed}).Union(

                    _updatedItems.Select(x => new ListItemState<T> {Item = x, State = ItemState.AddedOrUpdated})
                ).ToList();
            }
            set
            {
                _updatedItems = value.Where(x => x.State == ItemState.AddedOrUpdated).Select(x => x.Item).ToList();
                _deletedElements = value.Where(x => x.State == ItemState.Removed).Select(x => x.Item).ToList();
            }
        }
        public  DTOStatedList

        (IEnumerable<T> list)
        {
            _updatedItems = list.ToList();
        }
      
        #region ICollection

        public IEnumerator<T> GetEnumerator()
        {
            return _updatedItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (item != null)
            {
                var toDel = _deletedElements.FirstOrDefault(x => x.Identifier.Equals(item.Identifier));
                if (toDel != null) _deletedElements.Remove(toDel);
            }
            _updatedItems.Add(item);
        }

        public void Clear()
        {
            _deletedElements.AddRange(_updatedItems.Where(x=> !_deletedElements.Any(y=> x.Identifier.Equals(y.Identifier))));
            _updatedItems.Clear();
        }

        public bool Contains(T item)
        {
            return _updatedItems.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _updatedItems.CopyTo(array,arrayIndex);
            _deletedElements.RemoveAll(x => array.Any(y => y.Identifier.Equals(x.Identifier)));
        }

        public bool Remove(T item)
        {          
            _deletedElements.Add(item);
            return _updatedItems.Remove(item);
        }

        public int Count => _updatedItems.Count;
        public bool IsReadOnly { get; } = false;
        public int IndexOf(T item)
        {
            return _updatedItems.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _updatedItems.Insert(index,item);
            _deletedElements.RemoveAll(x => x.Identifier.Equals(item.Identifier));
        }

        public void RemoveAt(int index)
        {
            _deletedElements.Add(_updatedItems[index]);
            _updatedItems.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _updatedItems[index]; }
            set { _updatedItems[index] = value; }
        }

        #endregion 
    }

  
}
