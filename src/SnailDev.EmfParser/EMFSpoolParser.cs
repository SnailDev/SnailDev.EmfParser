using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser
{
    public class EMFSpoolParser
    {
        public int _Copies = 1; //The number of copies per page
        public int _Pages = 0;

        /// <summary>
        /// get emf stream
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
            EMFMetaRecordHeader recNext = NextHeader(spoolBinaryReader);

            while (recNext.iType != SpoolerRecordTypes.SRT_EOF)
            {
                if (recNext.iType == SpoolerRecordTypes.SRT_PAGE)
                {
                    _Pages += 1;
                }

                var emfStream = SkipAHeader(recNext, spoolBinaryReader);
                if (emfStream != null) emfStreams.Add(emfStream);

                recNext = NextHeader(spoolBinaryReader);
            }
            spoolBinaryReader.Close();
            spoolFileStream.Close();

            return emfStreams;
        }

        public int GetTruePageCount(string spoolFilename)
        {
            string ShadowFilename;
            ShadowFilename = spoolFilename;
            // Open a binary reader for the spool file;
            FileStream SpoolFileStream = new System.IO.FileStream(spoolFilename, FileMode.Open, FileAccess.Read);
            BinaryReader SpoolBinaryReader = new BinaryReader(SpoolFileStream, System.Text.Encoding.Unicode);
            Int32 iFoo;
            iFoo = SpoolBinaryReader.ReadInt32();
            while (iFoo != (int)SpoolerRecordTypes.SRT_DEVMODE && iFoo != (int)SpoolerRecordTypes.SRT_PAGE && iFoo != (int)SpoolerRecordTypes.SRT_EXT_PAGE)
            {
                iFoo = SpoolBinaryReader.ReadInt32();
            }
            SpoolBinaryReader.BaseStream.Seek(-4, SeekOrigin.Current);
            //Read the spooler records and count the total pages
            EMFMetaRecordHeader recNext = NextHeader(SpoolBinaryReader);

            while (recNext.iType != SpoolerRecordTypes.SRT_EOF)
            {
                if (recNext.iType == SpoolerRecordTypes.SRT_PAGE)
                {
                    _Pages += 1;
                }

                SkipAHeader(recNext, SpoolBinaryReader);
                recNext = NextHeader(SpoolBinaryReader);
            }
            SpoolBinaryReader.Close();
            SpoolFileStream.Close();
            return _Pages * _Copies;
        }

        public EMFMetaRecordHeader NextHeader(BinaryReader spoolBinaryReader)
        {

            EMFMetaRecordHeader recRet = new EMFMetaRecordHeader();
            // get the record type
            recRet.Seek = (int)spoolBinaryReader.BaseStream.Position;
            try
            {
                recRet.iType = (SpoolerRecordTypes)spoolBinaryReader.ReadInt32();
            }
            catch (EndOfStreamException e)
            {
                recRet.iType = SpoolerRecordTypes.SRT_EOF;
                return new EMFMetaRecordHeader();
            }
            // Get the record size
            recRet.nSize = spoolBinaryReader.ReadInt32();
            return recRet;
        }

        public Stream SkipAHeader(EMFMetaRecordHeader header, BinaryReader spoolBinaryReader)
        {
            Stream emfStream = null;
            if (header.nSize <= 0)
            {
                header.nSize = 8;
            }
            if (header.iType == SpoolerRecordTypes.SRT_JOB_INFO)
            {
                byte[] JobInfo;
                JobInfo = spoolBinaryReader.ReadBytes(header.nSize);
                spoolBinaryReader.BaseStream.Seek(header.Seek + header.nSize, SeekOrigin.Begin);
            }
            else if (header.iType == SpoolerRecordTypes.SRT_EOF)
            {
                // End of file reached..do nothing
            }
            else if (header.iType == SpoolerRecordTypes.SRT_DEVMODE)
            {
                // Spool job DEVMODE
                DevMode _dmThis = new DevMode(spoolBinaryReader);
                _Copies = _dmThis.Copies;
                spoolBinaryReader.BaseStream.Seek(header.Seek + 8 + header.nSize, SeekOrigin.Begin);
            }
            else if (header.iType == SpoolerRecordTypes.SRT_PAGE || header.iType == SpoolerRecordTypes.SRT_EXT_PAGE)
            {
                // 
                // ProcessEMFRecords(Header, SpoolBinaryReader);
                // EMFPage ThisPage = new EMFPage(SpoolBinaryReader);
                emfStream = GetEMFStream(spoolBinaryReader);
            }
            else if (header.iType == SpoolerRecordTypes.SRT_EOPAGE1 || header.iType == SpoolerRecordTypes.SRT_EOPAGE2)
            {
                // int plus long
                if (header.nSize == 0x8)
                {
                    spoolBinaryReader.BaseStream.Seek(header.Seek + header.nSize + 8, SeekOrigin.Begin);
                }
            }
            else if (header.iType == SpoolerRecordTypes.SRT_UNKNOWN)
            {
                spoolBinaryReader.BaseStream.Seek(header.Seek + 4, SeekOrigin.Begin);
            }
            else
            {
                spoolBinaryReader.BaseStream.Seek(header.Seek + header.nSize, SeekOrigin.Begin);
            }

            return emfStream;
        }

        public Stream GetEMFStream(BinaryReader spoolBinaryReader)
        {
            long oldPos = spoolBinaryReader.BaseStream.Position;

            var header = new EMFHeader(spoolBinaryReader);
            // spoolBinaryReader.BaseStream.Seek(oldPos, SeekOrigin.Begin);
            // var record = new EMFRecord(spoolBinaryReader);
            spoolBinaryReader.BaseStream.Seek(oldPos, SeekOrigin.Begin);

            // get a stream for just this emf record...
            return new MemoryStream(spoolBinaryReader.ReadBytes(header.FileSize), false);
        }
    }
}
