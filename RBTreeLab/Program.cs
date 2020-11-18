using RBTreeLab.DataStructures;

namespace RBTreeLab
{
    static class Program
    {
        private static void Main()
        {
            var tree = new RedBlackTree<int>();
            var generator = new Generator();
            var list = generator.Generate(25, 1, 101);
            foreach (var e in list)
            {
                tree.Add(e);
            }
            tree.PrintTree();
        }
    }
}
