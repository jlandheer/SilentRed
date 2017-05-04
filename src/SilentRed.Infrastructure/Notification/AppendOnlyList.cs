using System.Collections.Generic;
using System.Linq;

namespace SilentRed.Infrastructure
{
    /// <summary>
    /// Threadsafe lijst om alleen aan toe te voegen.
    /// Je kunt alleen de hele lijst opvragen (let op, dan is een lock actief)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AppendOnlyList<T>
    {
        public void Add(T value)
        {
            lock (_lock)
            {
                _list.Add(value);
            }
        }

        public IList<T> ToList()
        {
            lock (_lock)
            {
                return _list.ToList();
            }
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _list.Count;
                }
            }
        }

        private readonly List<T> _list = new List<T>();
        private readonly object _lock = new object();
    }
}
