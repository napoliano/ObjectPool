Thread-safe object pool.  
Use the following:
```
var set = ObjectPool<HashSet<int>>.Get();
{
    //Do something..
}
ObjectPool<HashSet<int>>.Return(set);
```
