namespace ObjectPool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var set = ObjectPool<HashSet<int>>.Get();
            {
                //Do something..
            }
            ObjectPool<HashSet<int>>.Return(set);
        }
    }
}