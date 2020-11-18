using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBTreeLab.Interfaces;

namespace RBTreeLab
{
    public class RedBlackTree<TKey> : IRedBlackTree<TKey> where TKey : IComparable
    {
        private TreeNode<TKey> _root;

        public RedBlackTree() { }

        public RedBlackTree(TKey key)
        {
            _root = new TreeNode<TKey>(key, NodeColor.Black);
        }

        public void Add(TKey key)
        {
            if (_root == null)
            {
                _root = new TreeNode<TKey>(key, NodeColor.Black);
                return;
            }
            var newNode = new TreeNode<TKey>(key, NodeColor.Red);
            var node = new TreeNode<TKey>();
            var current = _root;
            while (current != null)
            {
                node = current;
                current = current.Key.CompareTo(key) > 0
                              ? current.Left
                              : current.Right;
            }
            newNode.Parent = node;
            if (node.Key.CompareTo(key) > 0)
            {
                node.Left = newNode;
            }
            else
            {
                node.Right = newNode;
            }
            FixAdding(newNode);
        }

        public void Delete(TKey key)
        {
            var current = Search(key);
            if (current == null)
            {
                throw new KeyNotFoundException($"This key not found: {key}");
            }

            var countChildren = GetCountChildren(current);
            TreeNode<TKey> nullNode;
            var node = countChildren switch
                       {
                           0 => DeletingWithoutChildren(current, out nullNode),
                           1 => DeletingWithChild(current, out nullNode),
                           _ => DeletingWithChildren(current, out nullNode)
                       };
            if (nullNode != null)
            {
                FixDeleting(node, nullNode);
            }
        }

        public NodeColor? Find(TKey key)
        {
            return Search(key)?.Color;
        }

        public TKey Min()
        {
            var node = new TreeNode<TKey>();
            var current = _root;
            while (current != null)
            {
                node = current;
                current = current.Left;
            }
            return node.Key;
        }

        public TKey Max()
        {
            var node = new TreeNode<TKey>();
            var current = _root;
            while (current != null)
            {
                node = current;
                current = current.Right;
            }
            return node.Key;
        }

        public TKey FindNext(TKey key)
        {
            if (key.CompareTo(Max()) == 0)
            {
                throw new InvalidOperationException($"The current key: {key} is the max!");
            }

            var current = Search(key);
            var right = current.Right;
            var node = new TreeNode<TKey>();
            if (right == null)
            {
                node = current.Parent;
                while (node.Key.CompareTo(current.Key) < 0)
                {
                    node = node.Parent;
                }
                return node.Key;
            }
            while (right != null)
            {
                node = right;
                right = right.Left;
            }
            return node.Key;
        }

        public TKey FindPrev(TKey key)
        {
            if (key.CompareTo(Min()) == 0)
            {
                throw new InvalidOperationException($"The current key: {key} is the min!");
            }

            var current = Search(key);
            var left = current.Left;
            var node = new TreeNode<TKey>();
            if (left == null)
            {
                node = current.Parent;
                while (node.Key.CompareTo(current.Key) > 0)
                {
                    node = node.Parent;
                }
                return node.Key;
            }
            while (left != null)
            {
                node = left;
                left = left.Right;
            }
            return node.Key;
        }

        public void PrintTree()
        {
            if (_root != null)
            {
                PrintSubtrees(new StringBuilder(), _root, true);
            }
        }

        private void PrintSubtrees(StringBuilder indent, TreeNode<TKey> current, bool isTail)
        {
            if (current.Right != null)
            {
                PrintSubtrees(new StringBuilder()
                             .Append(indent)
                             .Append(isTail ? "│   " : "    "), current.Right, false);
            }

            Console.Write(indent);
            Console.Write(isTail ? "└── " : "┌── ");

            if (current.Color == NodeColor.Red)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(current.Key);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(current.Key);
                Console.ResetColor();
            } // print current node

            if (current.Left != null)
            {
                PrintSubtrees(new StringBuilder()
                             .Append(indent)
                             .Append(isTail ? "    " : "│   "), current.Left, true);
            }
        }

        private void FixAdding(TreeNode<TKey> node)
        {
            if (node == _root)
            {
                node.Color = NodeColor.Black;
                return;
            }
            while (node.Parent?.Color == NodeColor.Red)
            {
                var grandFather = node.Parent.Parent;
                var uncle = grandFather.Left == node.Parent
                                ? grandFather.Right
                                : grandFather.Left;
                if (uncle?.Color == NodeColor.Red)
                {
                    node = RecoloringFix(node, uncle, grandFather);
                }
                else
                {
                    node = RotationFix(node, grandFather);
                }
            }
            _root.Color = NodeColor.Black;
        }

        private TreeNode<TKey> RecoloringFix(TreeNode<TKey> node, TreeNode<TKey> uncle, TreeNode<TKey> grandFather)
        {
            node.Parent.Color = NodeColor.Black;
            uncle.Color = NodeColor.Black;
            grandFather.Color = NodeColor.Red;
            return grandFather;
        }

        private TreeNode<TKey> RotationFix(TreeNode<TKey> node, TreeNode<TKey> grandFather)
        {
            if (node.Parent == grandFather.Left && node == node.Parent.Right)
            {
                LeftRotate(node.Parent);
                node = node.Left;
            }
            else if (node.Parent == grandFather.Right && node == node.Parent.Left)
            {
                RightRotate(node.Parent);
                node = node.Right;
            }
            node.Parent.Color = NodeColor.Black;
            grandFather.Color = NodeColor.Red;
            if (node.Parent.Left == node)
            {
                RightRotate(grandFather);
            }
            else
            {
                LeftRotate(grandFather);
            }
            return grandFather;
        }

        private TreeNode<TKey> DeletingWithoutChildren(TreeNode<TKey> node, out TreeNode<TKey> nullNode)
        {
            var parent = node.Parent;
            nullNode = null;
            if (node == _root)
            {
                _root = null;
                return null;
            }
            if (node.Color == NodeColor.Black && !node.IsNull)
            {
                var current = node;
                node = new TreeNode<TKey> {Parent = node.Parent};
                Change(parent, current, node);
                nullNode = node;
                return node;
            }
            Change(parent, node, null);
            node.Parent = null;

            return node;
        }

        private void Change(TreeNode<TKey> parent, TreeNode<TKey> current, TreeNode<TKey> node)
        {
            if (parent?.Left == current)
            {
                parent.Left = node;
            }
            else if (parent?.Right != null)
            {
                parent.Right = node;
            }
        }

        private TreeNode<TKey> DeletingWithChild(TreeNode<TKey> node, out TreeNode<TKey> nullNode)
        {
            var child = node.Left ?? node.Right;
            (child.Key, node.Key) = (node.Key, child.Key);
            nullNode = null;
            DeletingWithoutChildren(child, out nullNode);
            return child;
        }

        private TreeNode<TKey> DeletingWithChildren(TreeNode<TKey> node, out TreeNode<TKey> nullNode)
        {
            var next = node.Key.CompareTo(Max()) == 0 ? null : Search(FindNext(node.Key));
            (node.Key, next.Key) = (next.Key, node.Key);
            var countChildren = GetCountChildren(next);
            nullNode = null;
            node = countChildren == 0
                       ? DeletingWithoutChildren(next, out nullNode)
                       : DeletingWithChild(next, out nullNode);
            return node;
        }

        private void FixDeleting(TreeNode<TKey> node, TreeNode<TKey> nullNode)
        {
            while (node.Parent != null && node.Color == NodeColor.Black)
            {
                if (node.Parent.Left == node)
                {
                    var brother = GetBrother(node);
                    var rightChild = brother?.Right;
                    var leftChild = brother?.Left;
                    node = Fix(node, brother, leftChild, rightChild, LeftRotate, RightRotate);
                }
                else if (node.Parent != null)
                {
                    var brother = GetBrother(node);
                    var rightChild = brother?.Right;
                    var leftChild = brother?.Left;
                    node = Fix(node, brother, rightChild, leftChild, RightRotate, LeftRotate);
                }
            }

            if (nullNode.Parent != null)
            {
                DeletingWithoutChildren(nullNode, out _);
            }
            node.Color = NodeColor.Black;
            _root.Color = NodeColor.Black;
        }

        private TreeNode<TKey> Fix(TreeNode<TKey> node,
                                   TreeNode<TKey> brother,
                                   TreeNode<TKey> firstNephew,
                                   TreeNode<TKey> secondNephew,
                                   Action<TreeNode<TKey>> firstRotate,
                                   Action<TreeNode<TKey>> secondRotate)
        {
            switch (brother.Color)
            {
                case NodeColor.Red:
                    node.Parent.Color = NodeColor.Red;
                    brother.Color = NodeColor.Black;
                    firstRotate(node.Parent);
                    return node;
                case NodeColor.Black when ChildrenIsBlack(brother):
                    brother.Color = NodeColor.Red;
                    return node.Parent;
                case NodeColor.Black when ChildIsRed(firstNephew, secondNephew):
                    (brother.Color, firstNephew.Color) = (firstNephew.Color, brother.Color);
                    secondRotate(brother);
                    return node;
                case NodeColor.Black:
                {
                    if (secondNephew?.Color == NodeColor.Red)
                    {
                        (brother.Color, node.Parent.Color) = (node.Parent.Color, brother.Color);
                        secondNephew.Color = NodeColor.Black;
                        firstRotate(node.Parent);
                    }
                    break;
                }
            }
            return _root;
        }

        private bool ChildIsRed(TreeNode<TKey> firstChild, TreeNode<TKey> secondChild)
        {
            return (secondChild == null || secondChild.Color == NodeColor.Black) &&
                   firstChild?.Color == NodeColor.Red;
        }

        private static bool ChildrenIsBlack(TreeNode<TKey> node)
        {
            return node.Left == null &&
                   node.Right == null ||
                   node.Left?.Color == NodeColor.Black &&
                   node.Right?.Color == NodeColor.Black;
        }

        private TreeNode<TKey> GetBrother(TreeNode<TKey> node)
        {
            if (node?.Parent != null)
            {
                return node.Parent.Right == node
                           ? node.Parent.Left
                           : node.Parent.Right;
            }
            return null;
        }

        private int GetCountChildren(TreeNode<TKey> node)
        {
            var count = node.Left == null ? 0 : 1;
            if (node.Right != null)
            {
                count++;
            }
            return count;
        }

        private void RightRotate(TreeNode<TKey> node)
        {
            var left = node.Left;
            var leftRight = left.Right;
            left.Parent = node.Parent;
            Change(node.Parent, node, left);
            if (_root == node)
            {
                _root = left;
            }
            node.Parent = left;
            node.Left = leftRight;
            left.Right = node;
            if (leftRight != null)
            {
                leftRight.Parent = node;
            }
        }

        private void LeftRotate(TreeNode<TKey> node)
        {
            var right = node.Right;
            var rightLeft = right.Left;
            right.Parent = node.Parent;
            Change(node.Parent, node, right);
            if (_root == node)
            {
                _root = right;
            }
            node.Parent = right;
            node.Right = rightLeft;
            right.Left = node;
            if (rightLeft != null)
            {
                rightLeft.Parent = node;
            }
        }

        private TreeNode<TKey> Search(TKey key)
        {
            var current = _root;
            while (current != null && current.Key.CompareTo(key) != 0)
            {
                current = current.Key.CompareTo(key) > 0
                              ? current.Left
                              : current.Right;
            }
            return current;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tuple<TKey, NodeColor>> GetEnumerator()
        {
            return GetItemsRecursively(_root)
                  .Select(node => Tuple.Create(node.Key, node.Color))
                  .GetEnumerator();
        }

        private static IEnumerable<TreeNode<TKey>> GetItemsRecursively(TreeNode<TKey> current)
        {
            if (current?.Left != null)
            {
                foreach (var node in GetItemsRecursively(current.Left))
                {
                    yield return node;
                }
            }
            if (current != null)
            {
                yield return current;
            }
            if (current?.Right != null)
            {
                foreach (var node in GetItemsRecursively(current.Right))
                {
                    yield return node;
                }
            }
        }
    }
}
