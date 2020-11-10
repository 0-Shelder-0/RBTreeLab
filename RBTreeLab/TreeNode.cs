namespace RBTreeLab
{
    public class TreeNode<T>
    {
        public T Key { get; }
        public NodeColor Color;
        public TreeNode<T> Right { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Parent { get; set; }

        public TreeNode() { }

        public TreeNode(T key, NodeColor color)
        {
            Key = key;
            Color = color;
        }
    }
}
