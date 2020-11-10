using System;

namespace RBTreeLab
{
    static class Program
    {
        private static void Main()
        {
            var t = new RebBlackTree<int>();
            for (var i = 20; i > 0; i--)
            {
                t.Add(i);
            }
            foreach (var tuple in t)
            {
                Console.WriteLine($"{tuple.Item1} {tuple.Item2.ToString()}");
            }
        }
    }
}
