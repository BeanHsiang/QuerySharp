using System.Collections;

namespace QuerySharp
{
    internal class ConditionEnumerator : IEnumerator
    {
        private Condition _current;
        private readonly Condition _first;

        public ConditionEnumerator(Condition first)
        {
            _first = first;
        }

        public Condition Current
        {
            get { return _current; }
        }

        public bool MoveNext()
        {
            if (_first == null)
            {
                return false;
            }
            if (_current == null)
            {
                _current = _first;
                return true;
            }
            if (_current.Next != null)
            {
                _current = _current.Next;
                return true;
            }
            //_current = null;
            return false;
        }

        public void Reset()
        {
            _current = null;
        }

        object IEnumerator.Current
        {
            get { return _current; }
        }
    }
}
