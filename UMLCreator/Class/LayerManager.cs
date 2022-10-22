using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLCreator
{
    public class LayerManager : IEnumerable
    {
        private LayerNode? _first;
        private LayerNode? _last;
        public int Count { get; private set; }
        public LayerManager()
        {
            _first = null;
            _last = null;
            Count = 0;
        }

        public void Add(Class c)
        {
            LayerNode node = new LayerNode(c);
            if(_first == null)
            {
                _first = node;
                _last = node;
                Count++;
                return;
            }

            _last.Next = node;
            _last = node;

            Count++;
        }

        public IEnumerator GetEnumerator()
        {
            LayerNode current = _first;
            while (current != null)
            {
                if(current.Value != null)
                    yield return current.Value;
                current = current.Next;
            }
        }

        public void Remove(Class c)
        {
            LayerNode previous = null;
            LayerNode current = _first;
            while(current != null)
            {
                if(current.Value == c)
                {
                    if (current == _last)
                    {
                        _last = previous;
                    }
                    if (current == _first)
                    {
                        _first = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                        Count--;
                        break;
                    }
                }
                previous = current;
                current = current.Next;
            }
        }
        public void MoveUp(Class c)
        {
            this.Remove(c);
            this.Add(c);
        }
        public bool Contains(string s)
        {
            LayerNode current = _first;
            bool found = false;
            while(current != null)
            {
                found = current.Value.Name == s;
                if (found)
                    break;
                current = current.Next;
            }

            return found;
        }

        public void Clear()
        {
            _first = null;
            _last = null;
            Count = 0;
        }

        public void LoadFromList(List<Class> classes)
        {
            Clear();
            foreach(Class c in classes)
            {
                Add(c);
            }
        }

        public List<Class> ToList()
        {
            if(Count == 0)
            {
                return new List<Class>();
            }

            List<Class> classes = new List<Class>();
            LayerNode? current = _first;
            while(current != null)
            {
                classes.Add(current.Value);
                current = current.Next;
            }

            return classes;
        }
    }

    public class LayerNode
    {
        public Class Value { get; set; }
        public LayerNode? Next { get; set; }
        public LayerNode(Class value)
        {
            this.Value = value;
            this.Next = null;
        }
    }
}
