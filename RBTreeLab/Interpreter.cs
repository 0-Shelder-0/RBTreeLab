using System;
using RBTreeLab.Interfaces;

namespace RBTreeLab
{
    public class Interpreter : IInterpreter<int>
    {
        public void Run(IRedBlackTree<int> redBlackTree)
        {
            while (true)
            {
                Console.WriteLine();
                var command = Console.ReadLine();
                var items = command.Split(' ');
                var commandNumber = Convert.ToInt32(items[0]);
                switch (commandNumber)
                {
                    case 1:
                        redBlackTree.Add(Convert.ToInt32(items[1]));
                        redBlackTree.PrintTree();
                        break;
                    case 2:
                        DeleteKey(items[1], redBlackTree);
                        break;
                    case 3:
                    case 6:
                    case 7:
                        DoFindCommands(items[1], commandNumber, redBlackTree);
                        break;
                    case 4:
                        Console.WriteLine(redBlackTree.Min());
                        break;
                    case 5:
                        Console.WriteLine(redBlackTree.Max());
                        break;
                    default:
                        Console.WriteLine("Incorrect input. Try again.");
                        break;
                }
            }
        }

        private void DeleteKey(string keyItem, IRedBlackTree<int> redBlackTree)
        {
            var key = Convert.ToInt32(keyItem);
            if (redBlackTree.Find(key) != null)
            {
                redBlackTree.Delete(Convert.ToInt32(keyItem));
                redBlackTree.PrintTree();
            }
            else
            {
                Console.WriteLine("The tree does not have a vertex with such a key.");
            }
        }

        private void DoFindCommands(string keyItem, int commandNumber, IRedBlackTree<int> redBlackTree)
        {
            var key = Convert.ToInt32(keyItem);
            if (redBlackTree.Find(key) != null)
            {
                switch (commandNumber)
                {
                    case 3:
                        Console.WriteLine(redBlackTree.Find(key));
                        break;
                    case 6:
                        if (key == redBlackTree.Max())
                        {
                            Console.WriteLine("The tree does not have the vertex");
                        }
                        else
                        {
                            Console.WriteLine(redBlackTree.FindNext(key));
                        }
                        break;
                    case 7:
                        if (key == redBlackTree.Min())
                        {
                            Console.WriteLine("The tree does not have the vertex");
                        }
                        else
                        {
                            Console.WriteLine(redBlackTree.FindPrev(key));
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("The tree does not have a vertex with such a key.");
            }
            
        }
    }
}
