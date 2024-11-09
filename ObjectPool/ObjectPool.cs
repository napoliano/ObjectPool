using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool
{
    public static class ObjectPool<T> where T : class, new()
    {
        [ThreadStatic]
        private static ObjectPoolBuffer<T> _objectPoolBuffer = new();


        public static T Get()
        {
            return _objectPoolBuffer.Get();
        }

        public static void Return(T obj)
        {
            _objectPoolBuffer.Return(obj);
        }
    }
}
