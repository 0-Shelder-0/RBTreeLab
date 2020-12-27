using System;
using RBTreeLab.DataStructures;

namespace RBTreeLab
{
    static class Program
    {
        private static void Main()
        {
            var tree = new RedBlackTree<int>();
            tree.PrintTree();
            var interpreter = new Interpreter();
            interpreter.Run(tree, Console.OpenStandardInput(), Console.OpenStandardOutput());
        }
    }
}
