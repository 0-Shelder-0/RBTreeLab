using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RBTreeLab.Interfaces;

namespace RBTreeLab
{
    public class Interpreter : IInterpreter<int>
    {
        public void Run(IRedBlackTree<int> redBlackTree, Stream input, Stream output)
        {
            while (true)
            {
                output.StreamWriteLine();
                var command = GetInput(input);
                var items = command.Result.Split(' ');
                var commandNumber = Convert.ToInt32(items[0]);
                switch (commandNumber)
                {
                    case 1:
                        redBlackTree.Add(Convert.ToInt32(items[1]));
                        redBlackTree.PrintTree();
                        break;
                    case 2:
                        DeleteKey(redBlackTree, items[1], output);
                        break;
                    case 3:
                    case 6:
                    case 7:
                        DoFindCommands(redBlackTree, commandNumber, items[1], output);
                        break;
                    case 4:
                        output.StreamWriteLine(redBlackTree.Min().ToString());
                        break;
                    case 5:
                        output.StreamWriteLine(redBlackTree.Max().ToString());
                        break;
                    default:
                        output.StreamWriteLine("Incorrect input. Try again.");
                        break;
                }
            }
        }

        private async Task<string> GetInput(Stream input)
        {
            var buffer = new byte[100];
            await input.ReadAsync(buffer);
            return Encoding.Default.GetString(buffer);
        }

        private void DeleteKey(IRedBlackTree<int> redBlackTree, string keyItem, Stream output)
        {
            var key = Convert.ToInt32(keyItem);
            if (redBlackTree.Find(key) != null)
            {
                redBlackTree.Delete(Convert.ToInt32(keyItem));
                redBlackTree.PrintTree();
            }
            else
            {
                output.StreamWriteLine("The tree does not have a node with such a key.");
            }
        }

        private void DoFindCommands(IRedBlackTree<int> redBlackTree, int commandNumber, string keyItem, Stream output)
        {
            var key = Convert.ToInt32(keyItem);
            if (redBlackTree.Find(key) != null)
            {
                switch (commandNumber)
                {
                    case 3:
                        output.StreamWriteLine(redBlackTree.Find(key).ToString());
                        break;
                    case 6:
                        output.StreamWriteLine(key == redBlackTree.Max()
                                                   ? "The tree does not have the node"
                                                   : redBlackTree.FindNext(key).ToString());
                        break;
                    case 7:
                        output.StreamWriteLine(key == redBlackTree.Min()
                                                   ? "The tree does not have the node"
                                                   : redBlackTree.FindPrev(key).ToString());
                        break;
                }
            }
            else
            {
                output.StreamWriteLine("The tree does not have a node with such a key.");
            }
        }
    }
}
