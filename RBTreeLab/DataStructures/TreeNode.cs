namespace RBTreeLab.DataStructures
{
    public class TreeNode<T>
    {
        public NodeColor Color;
        public T Key { get; set; }
        public bool IsNull { get; }
        public TreeNode<T> Parent { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public TreeNode()
        {
            Color = NodeColor.Black;
            IsNull = true;
        }

        public TreeNode(T key, NodeColor color)
        {
            Key = key;
            Color = color;
            IsNull = false;
        }
    }
}
