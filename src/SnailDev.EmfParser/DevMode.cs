using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser
{
    public class DevMode
    {
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public char[] dmDeviceName = new char[64];
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public short dmOrientation;
        public short dmPaperSize;
        public short dmPaperLength;
        public short dmPaperWidth;
        public short dmScale;
        public short dmCopies;
        public short dmDefaultSource;
        public short dmPrintQuality;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public char[] dmFormName = new char[32];
        public short dmUnusedPadding;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;

        public short Copies
        {
            get
            {
                return dmCopies;
            }
        }

        public bool Collate
        {
            get
            {
                return (dmCollate > 0);
            }
        }

        public DevMode(BinaryReader FileReader)
        {
            dmDeviceName = FileReader.ReadChars(64);
            //'FileReader.BaseStream.Seek(64, IO.SeekOrigin.Current)
            dmSpecVersion = FileReader.ReadInt16();
            dmDriverVersion = FileReader.ReadInt16();
            dmSize = FileReader.ReadInt16();
            dmDriverExtra = FileReader.ReadInt16();
            dmFields = FileReader.ReadInt32();
            dmOrientation = FileReader.ReadInt16();
            dmPaperSize = FileReader.ReadInt16();
            dmPaperLength = FileReader.ReadInt16();
            dmPaperWidth = FileReader.ReadInt16();
            dmScale = FileReader.ReadInt16();
            dmCopies = FileReader.ReadInt16();
            dmDefaultSource = FileReader.ReadInt16();
            dmPrintQuality = FileReader.ReadInt16();
            dmColor = FileReader.ReadInt16();
            dmDuplex = FileReader.ReadInt16();
            dmYResolution = FileReader.ReadInt16();
            dmTTOption = FileReader.ReadInt16();
            dmCollate = FileReader.ReadInt16();
            dmFormName = FileReader.ReadChars(32); //32 chars
            dmUnusedPadding = FileReader.ReadInt16();
            dmBitsPerPel = FileReader.ReadInt32();
            dmPelsWidth = FileReader.ReadInt32();
            dmPelsHeight = FileReader.ReadInt32();
            dmDisplayFlags = FileReader.ReadInt32();
            dmDisplayFrequency = FileReader.ReadInt32();
            dmICMMethod = FileReader.ReadInt32();
            dmICMIntent = FileReader.ReadInt32();
            dmMediaType = FileReader.ReadInt32();
            dmDitherType = FileReader.ReadInt32();
            dmReserved1 = FileReader.ReadInt32();
            dmReserved2 = FileReader.ReadInt32();
            dmPanningWidth = FileReader.ReadInt32();
            dmPanningHeight = FileReader.ReadInt32();
        }
    }
}
