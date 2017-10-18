using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser
{
    public class EMFPage
    {
        public EMFHeader _Header;
        public Image _Thumbnail;
        public Int32 _Scale = 20;
        //public int xds = 0;
        public EMFHeader Header
        {
            get
            {
                return _Header;
            }
        }
        public Image Thumbnail
        {
            get
            {
                return _Thumbnail;
            }
        }

        public EMFPage(BinaryReader FileReader)
        {
            //'\\ get the EMF page header
            long oldPos = FileReader.BaseStream.Position;
            _Header = new EMFHeader(FileReader);

            FileReader.BaseStream.Seek(oldPos, SeekOrigin.Begin);

            //'\\ get a stream for just this emf record...
            MemoryStream ThisEMFStream = new MemoryStream(FileReader.ReadBytes(_Header.FileSize), false);
            Metafile emfPage = new Metafile(ThisEMFStream);
            Image.GetThumbnailImageAbort AbortThumbna;
            AbortThumbna = AbortThumbnail;
            _Thumbnail = emfPage.GetThumbnailImage(_Header.FrameRect.Width / _Scale, _Header.FrameRect.Height / _Scale, AbortThumbna, IntPtr.Zero);

            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string min = DateTime.Now.Minute.ToString();
            string sec = DateTime.Now.Second.ToString();
            string ssec = DateTime.Now.Millisecond.ToString();

            _Thumbnail.Save("emf\\" + year + month + day + hour + min + sec + ssec + ".emf", ImageFormat.Emf);
            _Thumbnail.Dispose();
        }

        public bool AbortThumbnail()
        {
            return false;
        }
    }
}
