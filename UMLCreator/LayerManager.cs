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
        }

        public void Add(Class c)
        {
            LayerNode node = new LayerNode(c);
            if(_first == null)
            {
                _first = node;
                _last = node;
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
