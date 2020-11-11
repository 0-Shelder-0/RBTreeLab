using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            RestoringProperties(newNode);
        }

        public void Delete(TKey key)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public TKey FindPrev(TKey key)
        {
            throw new System.NotImplementedException();
        }

        private void RestoringProperties(TreeNode<TKey> node)
        {
            if (node == _root)
            {
                node.Color = NodeColor.Black;
                return;
            }
            while (node.Parent != null && node.Parent.Color == NodeColor.Red)
            {
                if (node.Parent.Parent.Left == node.Parent) //dad is left child
                {
                    node = node.Parent.Parent.Right != null
                               ? Repaint(node, node.Parent.Parent.Right, node.Parent.Parent)
                               : Rotate(node, node.Parent.Right, node.Parent.Parent, LeftRotate, RightRotate);
                }
                else
                {
                    node = node.Parent.Parent.Left != null
                               ? Repaint(node, node.Parent.Parent.Left, node.Parent.Parent)
                               : Rotate(node, node.Parent.Left, node.Parent.Parent, RightRotate, LeftRotate);
                }
            }
            _root.Color = NodeColor.Black;
        }

        private TreeNode<TKey> Repaint(TreeNode<TKey> node, TreeNode<TKey> uncle, TreeNode<TKey> grandfather)
        {
            if (uncle.Color == NodeColor.Red)
            {
                node.Parent.Color = NodeColor.Black;
                uncle.Color = NodeColor.Black;
                grandfather.Color = NodeColor.Red;
            }
            else
            {
                node.Parent.Color = NodeColor.Black;
                grandfather.Color = NodeColor.Red;
                if (node.Parent.Left == node && grandfather.Left == node.Parent)
                {
                    RightRotate(grandfather);
                }
                else
                {
                    LeftRotate(grandfather);
                }
            }
            return grandfather;
        }

        private TreeNode<TKey> Rotate(TreeNode<TKey> node,
                                      TreeNode<TKey> child,
                                      TreeNode<TKey> grandfather,
                                      Action<TreeNode<TKey>> firstRotate,
                                      Action<TreeNode<TKey>> secondRotate)
        {
            if (child == node)
            {
                node = node.Parent;
                firstRotate(node);
            }
            node.Parent.Color = NodeColor.Black;
            grandfather.Color = NodeColor.Red;
            secondRotate(grandfather);
            return node;
        }

        private void RightRotate(TreeNode<TKey> node)
        {
            var left = node.Left;
            var leftRight = node.Left.Right;
            left.Parent = node.Parent;
            if (node.Parent != null && node.Parent.Left == node)
            {
                node.Parent.Left = left;
            }
            else if (node.Parent != null)
            {
                node.Parent.Right = left;
            }
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
            if (node.Parent != null && node.Parent.Left == node)
            {
                node.Parent.Left = right;
            }
            else if (node.Parent != null)
            {
                node.Parent.Right = right;
            }
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
            return GetItemsRecursively(_root).Select(node => Tuple.Create(node.Key, node.Color)).GetEnumerator();
        }

        private static IEnumerable<TreeNode<TKey>> GetItemsRecursively(TreeNode<TKey> current)
        {
            if (current.Left != null)
            {
                foreach (var node in GetItemsRecursively(current.Left))
                {
                    yield return node;
                }
            }
            yield return current;
            if (current.Right != null)
            {
                foreach (var node in GetItemsRecursively(current.Right))
                {
                    yield return node;
                }
            }
        }
    }
}
