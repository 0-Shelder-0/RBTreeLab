using System.IO;
using System.Text;

namespace RBTreeLab
{
    public static class StreamExtension
    {
        public static void StreamWrite(this Stream stream, string text = null)
        {
            stream.Write(Encoding.Default.GetBytes($"{text}"));
        }

        public static void StreamWriteLine(this Stream stream, string text = null)
        {
            stream.Write(Encoding.Default.GetBytes($"{text}\r\n"));
        }
    }
}
