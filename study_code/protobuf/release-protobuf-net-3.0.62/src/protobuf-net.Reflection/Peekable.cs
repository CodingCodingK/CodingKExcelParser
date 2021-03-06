using System;
using System.Collections.Generic;

namespace ProtoBuf.Reflection
{
    internal sealed class Peekable<T> : IDisposable
    {
        public override string ToString()
        {
            return Peek(out T val) ? (val?.ToString() ?? "(null)") : "(EOF)";
        }
        private readonly IEnumerator<T> _iter;
        private T _peek, _prev;
        private bool _havePeek, _eof;
        public Peekable(IEnumerable<T> sequence)
        {
            _iter = sequence.GetEnumerator();
        }
        public T Previous => _prev;
        public bool Consume()
        {
            bool haveData = _havePeek || Peek(out T _);
            _prev = _peek;
            _havePeek = false;
            return haveData;
        }
        public bool Peek(out T next)
        {
            if (!_havePeek)
            {
                if (_iter.MoveNext())
                {
                    _prev = _peek;
                    _peek = _iter.Current;
                    _havePeek = true;
                }
                else
                {
                    _eof = true;
                    _havePeek = false;
                }
            }
            if (_eof)
            {
                next = default;
                return false;
            }
            next = _peek;
            return true;
        }
        public void Dispose() => _iter?.Dispose();
    }
}
