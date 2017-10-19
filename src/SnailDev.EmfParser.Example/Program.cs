using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"D:\00033.SPL", FileMode.Open);

            EMFSpoolParser parser = new EMFSpoolParser();
            var emfpages = parser.GetEmfStreams(fs);

            foreach (var emf in emfpages)
            {
                Metafile pageImage = new Metafile(emf);
                pageImage.Save(@"D:\00033_" + emfpages.IndexOf(emf) + ".emf", ImageFormat.Emf);
            }


            fs.Close();

            Console.ReadLine();
        }
    }
}
