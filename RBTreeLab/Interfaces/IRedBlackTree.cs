namespace RBTreeLab.Interfaces
{
    public interface IRedBlackTree<T>
    {
        void Add(T key);
        void Delete(T key);
        NodeColor Find(T key);
        T Min();
        T Max();
        T FindNext(T key);
        T FindPrev(T key);
    }
}
