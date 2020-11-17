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
                var tokens = command.Split(' ');
                switch (tokens[0])
                {
                    case "1":
                    {
                        redBlackTree.Add(Convert.ToInt32(tokens[1]));
                        redBlackTree.PrintTree();
                        break;
                    }
                    case "2":
                    {
                        redBlackTree.Delete(Convert.ToInt32(tokens[1]));
                        redBlackTree.PrintTree();
                        break;
                    }
                    case "3":
                    {
                        Console.WriteLine(redBlackTree.Find(Convert.ToInt32(tokens[1])));
                        break;
                    }
                    case "4":
                    {
                        Console.WriteLine(redBlackTree.Min());
                        break;
                    }
                    case "5":
                    {
                        Console.WriteLine(redBlackTree.Max());
                        break;
                    }
                    case "6":
                    {
                        Console.WriteLine(redBlackTree.FindNext(Convert.ToInt32(tokens[1])));
                        break;
                    }
                    case "7":
                    {
                        Console.WriteLine(redBlackTree.FindPrev(Convert.ToInt32(tokens[1])));
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Incorrect input. Try again.");
                        break;
                    }
                }
            }
        }
    }
}
