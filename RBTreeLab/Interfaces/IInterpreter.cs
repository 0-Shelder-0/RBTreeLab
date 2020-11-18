using System;
using System.IO;

namespace RBTreeLab.Interfaces
{
    public interface IInterpreter<TKey> where TKey : IComparable
    {
        void Run(IRedBlackTree<TKey> redBlackTree, Stream input, Stream output);
    }
}
