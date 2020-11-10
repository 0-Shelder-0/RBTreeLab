using System;
using System.Collections.Generic;

namespace RBTreeLab.Interfaces
{
    public interface IRedBlackTree<TKey> : IEnumerable<Tuple<TKey, NodeColor>> where TKey : IComparable
    {
        void Add(TKey key);
        void Delete(TKey key);
        NodeColor Find(TKey key);
        TKey Min();
        TKey Max();
        TKey FindNext(TKey key);
        TKey FindPrev(TKey key);
    }
}
