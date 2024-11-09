using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool
{
    public static class GlobalObjectPool<T> where T : class, new()
    {
        private static Queue<T> _pool = new();
        private static object _lock = new();


        public static void Get(ref T?[] buffer, int offset, int count)
        {
            Monitor.Enter(_lock);

            for (int i = 0; i < count; ++i)
            {
                int index = offset + i;

                if (_pool.Count > 0)
                {
                    buffer[index] = _pool.Dequeue();
                }
                else
                {
                    buffer[index] = new();
                }
            }

            Monitor.Exit(_lock);
        }

        public static void Return(ref T?[] buffer, int offset, int count)
        {
            Monitor.Enter(_lock);

            for (int i = 0; i < count; ++i)
            {
                int index = offset + i;

                var obj = buffer[index];
                if (obj == null)
                    continue;

                buffer[index] = null;

                _pool.Enqueue(obj);
            }

            Monitor.Exit(_lock);
        }
    }
}
