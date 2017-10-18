using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser
{
    public class EMFSpoolfileReader
    {
        public int _Copies = 1; //The number of copies per page
        public int _Pages = 0;

        /// <summary>
        /// 获取EMF流
        /// </summary>
        /// <param name="spoolFileStream"></param>
        /// <returns></returns>
        public List<Stream> GetEmfStreams(Stream spoolFileStream)
        {
            List<Stream> emfStreams = new List<Stream>();
            BinaryReader spoolBinaryReader = new BinaryReader(spoolFileStream, System.Text.Encoding.Unicode);
            Int32 iFoo = spoolBinaryReader.ReadInt32();
            while (iFoo != (int)SpoolerRecordTypes.SRT_DEVMODE && iFoo != (int)SpoolerRecordTypes.SRT_PAGE && iFoo != (int)SpoolerRecordTypes.SRT_EXT_PAGE)
            {
                iFoo = spoolBinaryReader.ReadInt32();
            }
            spoolBinaryReader.BaseStream.Seek(-4, SeekOrigin.Current);

            //Read the spooler records and count the total pages
            EMFMetaRecordHeader recNext = NextHeader(ref spoolBinaryReader);

            while (recNext.iType != SpoolerRecordTypes.SRT_EOF)
            {
                if (recNext.iType == SpoolerRecordTypes.SRT_PAGE)
                {
                    _Pages += 1;
                }

                var emfStream = SkipAHeader(recNext, ref spoolBinaryReader);//有一个call
                if (emfStream != null) emfStreams.Add(emfStream);

                recNext = NextHeader(ref spoolBinaryReader);
            }
            spoolBinaryReader.Close();
            spoolFileStream.Close();

            return emfStreams;
        }

        public int GetTruePageCount(string SpoolFilename)
        {
            string ShadowFilename;
            ShadowFilename = SpoolFilename;
            // Open a binary reader for the spool file;
            FileStream SpoolFileStream = new System.IO.FileStream(SpoolFilename, FileMode.Open, FileAccess.Read);
            BinaryReader SpoolBinaryReader = new BinaryReader(SpoolFileStream, System.Text.Encoding.Unicode);
            Int32 iFoo;
            iFoo = SpoolBinaryReader.ReadInt32();
            while (iFoo != (int)SpoolerRecordTypes.SRT_DEVMODE && iFoo != (int)SpoolerRecordTypes.SRT_PAGE && iFoo != (int)SpoolerRecordTypes.SRT_EXT_PAGE)
            {
                iFoo = SpoolBinaryReader.ReadInt32();
            }
            SpoolBinaryReader.BaseStream.Seek(-4, SeekOrigin.Current);
            //Read the spooler records and count the total pages
            EMFMetaRecordHeader recNext = NextHeader(ref SpoolBinaryReader);

            while (recNext.iType != SpoolerRecordTypes.SRT_EOF)
            {
                if (recNext.iType == SpoolerRecordTypes.SRT_PAGE)
                {
                    _Pages += 1;
                }

                SkipAHeader(recNext, ref SpoolBinaryReader);//有一个call
                recNext = NextHeader(ref SpoolBinaryReader);
            }
            SpoolBinaryReader.Close();
            SpoolFileStream.Close();
            return _Pages * _Copies;
        }

        public EMFMetaRecordHeader NextHeader(ref BinaryReader SpoolBinaryReader)
        {

            EMFMetaRecordHeader recRet = new EMFMetaRecordHeader();
            //\\ get the record type
            recRet.Seek = (int)SpoolBinaryReader.BaseStream.Position;
            try
            {
                recRet.iType = (SpoolerRecordTypes)SpoolBinaryReader.ReadInt32();
            }
            catch (EndOfStreamException e)
            {
                recRet.iType = SpoolerRecordTypes.SRT_EOF;
                return new EMFMetaRecordHeader();
            }
            //\\ Get the record size
            recRet.nSize = SpoolBinaryReader.ReadInt32();
            return recRet;
        }

        public Stream SkipAHeader(EMFMetaRecordHeader Header, ref BinaryReader SpoolBinaryReader)
        {
            Stream emfStream = null;
            if (Header.nSize <= 0)
            {
                Header.nSize = 8;
            }
            if (Header.iType == SpoolerRecordTypes.SRT_JOB_INFO)
            {
                byte[] JobInfo;
                JobInfo = SpoolBinaryReader.ReadBytes(Header.nSize);
                SpoolBinaryReader.BaseStream.Seek(Header.Seek + Header.nSize, SeekOrigin.Begin);
            }
            else if (Header.iType == SpoolerRecordTypes.SRT_EOF)
            {
                //\\ End of file reached..do nothing
            }
            else if (Header.iType == SpoolerRecordTypes.SRT_DEVMODE)
            {
                //'\\ Spool job DEVMODE
                DevMode _dmThis = new DevMode(SpoolBinaryReader);
                _Copies = _dmThis.Copies;
                SpoolBinaryReader.BaseStream.Seek(Header.Seek + 8 + Header.nSize, SeekOrigin.Begin);
            }
            else if (Header.iType == SpoolerRecordTypes.SRT_PAGE || Header.iType == SpoolerRecordTypes.SRT_EXT_PAGE)
            {
                //\\ 
                // ProcessEMFRecords(Header, SpoolBinaryReader);//有一个call
                // EMFPage ThisPage = new EMFPage(SpoolBinaryReader);
                emfStream = GetEMFStream(SpoolBinaryReader);
            }
            else if (Header.iType == SpoolerRecordTypes.SRT_EOPAGE1 || Header.iType == SpoolerRecordTypes.SRT_EOPAGE2)
            {
                //'\\ int plus long
                if (Header.nSize == 0x8)
                {
                    SpoolBinaryReader.BaseStream.Seek(Header.Seek + Header.nSize + 8, SeekOrigin.Begin);
                }
            }
            else if (Header.iType == SpoolerRecordTypes.SRT_UNKNOWN)
            {
                SpoolBinaryReader.BaseStream.Seek(Header.Seek + 4, SeekOrigin.Begin);
            }
            else
            {
                SpoolBinaryReader.BaseStream.Seek(Header.Seek + Header.nSize, SeekOrigin.Begin);
            }

            return emfStream;
        }

        public Stream GetEMFStream(BinaryReader FileReader)
        {
            long oldPos = FileReader.BaseStream.Position;
            var _header = new EMFHeader(FileReader);
            FileReader.BaseStream.Seek(oldPos, SeekOrigin.Begin);

            //'\\ get a stream for just this emf record...
            return new MemoryStream(FileReader.ReadBytes(_header.FileSize), false);
        }
    }
}
