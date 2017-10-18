using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"D:\123.SPL", FileMode.Open);

            EMFSpoolParser parser = new EMFSpoolParser();
            parser.GetEmfStreams(fs);

            fs.Close();

            Console.ReadLine();
        }
    }
}
