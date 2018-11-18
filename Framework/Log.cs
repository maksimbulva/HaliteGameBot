using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaliteGameBot.Framework
{
    static class Log
    {
        private static readonly List<string> _messageQueue = new List<string>();
        private static string _filename;

        static public void SetFileName(string filename)
        {
            _filename = filename;
        }

        static public void Write(string message)
        {
            if (_filename == null)
            {
                _messageQueue.Add(message);
            }
            else
            {
                WriteToFile(new string[] { message });
            }
        }

        static private void FlushMessageQueueToFile()
        {
            WriteToFile(_messageQueue);
            _messageQueue.Clear();
        }

        static private void WriteToFile(IEnumerable<string> messages)
        {
            using (var writer = new StreamWriter(_filename, append: true))
            {
                foreach (string message in messages)
                {
                    writer.WriteLine(message);
                }
            }
        }
    }
}
