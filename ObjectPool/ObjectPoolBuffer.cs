using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool
{
    public class ObjectPoolBuffer<T> where T : class, new()
    {
        private static readonly int s_capacity = 128;
        private static readonly int s_rentCount = 32;

        private T?[] _pool = new T?[s_capacity];

        private int _top = -1;


        public T Get()
        {
            if (_top == -1)
            {
                GlobalObjectPool<T>.Get(ref _pool, 0, s_rentCount);
                _top = s_rentCount - 1;
            }

            T obj = _pool[_top];
            _pool[_top] = null;
            --_top;

            return obj;
        }

        public void Return(T obj)
        {
            if (_top == (s_capacity - 1))
            {
                int offset = s_capacity - s_rentCount;
                GlobalObjectPool<T>.Return(ref _pool, offset, s_rentCount);
                _top = offset - 1;
            }

            ++_top;
            _pool[_top] = obj;
        }
    }
}
