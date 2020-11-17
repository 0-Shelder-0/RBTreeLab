using System;

namespace RBTreeLab.Interfaces
{
    public interface IInterpreter<TKey> where TKey : IComparable
    {
        void Run(IRedBlackTree<TKey> redBlackTree);
    }
}
