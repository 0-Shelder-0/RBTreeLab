namespace RBTreeLab.DataStructures
{
    public class TreeNode<TKey>
    {
        public NodeColor Color;
        public TKey Key { get; set; }
        public bool IsNull { get; }
        public TreeNode<TKey> Parent { get; set; }
        public TreeNode<TKey> Left { get; set; }
        public TreeNode<TKey> Right { get; set; }

        public TreeNode()
        {
            Color = NodeColor.Black;
            IsNull = true;
        }

        public TreeNode(TKey key, NodeColor color)
        {
            Key = key;
            Color = color;
            IsNull = false;
        }
    }
}
