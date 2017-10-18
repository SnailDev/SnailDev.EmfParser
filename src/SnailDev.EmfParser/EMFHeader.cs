using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser
{
    public class EMFHeader
    {
        //'\\ EMR record header
        public Int32 _iType;
        public Int32 _nSize;
        //'\\ Boundary rectangle
        public Int32 _rclBounds_Left;
        public Int32 _rclBounds_Top;
        public Int32 _rclBounds_Right;
        public Int32 _rclBounds_Bottom;
        //'\\ Frame rectangle
        public Int32 _rclFrame_Left;
        public Int32 _rclFrame_Top;
        public Int32 _rclFrame_Right;
        public Int32 _rclFrame_Bottom;
        //'\\ "Signature"
        public byte _signature_1;
        public byte _signature_2;  //'E
        public byte _signature_3;  //'M
        public byte _signature_4;  //'F
        //'\\ nVersion
        public Int32 _nVersion;
        public Int32 _ntypes;
        public Int32 _nRecords;
        public Int32 _nHandles;
        public Int16 _sReversed;
        public Int16 _nDescription;
        public Int32 _offDescription;
        public Int32 _nPalEntries;
        public Int32 _szlDeviceWidth;
        public Int32 _szlDeviceHeight;
        public Int32 _szlDeviceWidthMilimeters;
        public Int32 _szlDeviceHeightMilimeters;
        public Int32 _cbPixelFormat;
        public Int32 _offPixelFormat;
        public bool _bOpenGL;
        public Int32 _szlMicrometersWidth;
        public Int32 _szlMicrometersHeight;
        public char[] _Description;
        public Rectangle BoundaryRect
        {
            get
            {
                return new Rectangle(_rclBounds_Left, _rclBounds_Top, _rclBounds_Right, _rclBounds_Bottom);
            }
        }
        public Rectangle FrameRect
        {
            get
            {
                return new Rectangle(_rclFrame_Left, _rclFrame_Top, _rclFrame_Right, _rclFrame_Bottom);
            }
        }

        public int Size
        {
            get
            {
                return _nSize;
            }
        }

        public int RecordCount
        {
            get
            {
                return _nRecords;
            }
        }

        public int FileSize
        {
            get
            {
                return _ntypes;
            }
        }
        public EMFHeader(BinaryReader SpoolBinaryReader)
        {

            _iType = SpoolBinaryReader.ReadInt32();
            _nSize = SpoolBinaryReader.ReadInt32();
            //'\\ Boundary rectangle
            _rclBounds_Left = SpoolBinaryReader.ReadInt32();
            _rclBounds_Top = SpoolBinaryReader.ReadInt32();
            _rclBounds_Right = SpoolBinaryReader.ReadInt32();
            _rclBounds_Bottom = SpoolBinaryReader.ReadInt32();
            //'\\ Frame(帧) rectangle(矩形)
            _rclFrame_Left = SpoolBinaryReader.ReadInt32();
            _rclFrame_Top = SpoolBinaryReader.ReadInt32();
            _rclFrame_Right = SpoolBinaryReader.ReadInt32();
            _rclFrame_Bottom = SpoolBinaryReader.ReadInt32();
            //'\\ "Signature(署名)"
            _signature_1 = SpoolBinaryReader.ReadByte();
            _signature_2 = SpoolBinaryReader.ReadByte();
            _signature_3 = SpoolBinaryReader.ReadByte();
            _signature_4 = SpoolBinaryReader.ReadByte();
            //'\\ nVersion
            _nVersion = SpoolBinaryReader.ReadInt32();
            _ntypes = SpoolBinaryReader.ReadInt32();
            _nRecords = SpoolBinaryReader.ReadInt32();
            _nHandles = SpoolBinaryReader.ReadInt16();
            _sReversed = SpoolBinaryReader.ReadInt16();
            _nDescription = (short)SpoolBinaryReader.ReadInt32();
            _offDescription = SpoolBinaryReader.ReadInt32();
            _nPalEntries = SpoolBinaryReader.ReadInt32();
            _szlDeviceWidth = SpoolBinaryReader.ReadInt32();
            _szlDeviceHeight = SpoolBinaryReader.ReadInt32();
            _szlDeviceWidthMilimeters = SpoolBinaryReader.ReadInt32();
            _szlDeviceHeightMilimeters = SpoolBinaryReader.ReadInt32();
            if (_nSize > 88)
            {
                _cbPixelFormat = SpoolBinaryReader.ReadInt32();
                _offPixelFormat = SpoolBinaryReader.ReadInt32();
                _bOpenGL = SpoolBinaryReader.ReadBoolean();
            }
            if (_nSize > 100)
            {
                _szlMicrometersWidth = SpoolBinaryReader.ReadInt32();
                _szlMicrometersHeight = SpoolBinaryReader.ReadInt32();
            }
            if (_nDescription > 0)
            {
                _Description = SpoolBinaryReader.ReadChars(_nDescription);
            }
        }
    }

    public struct EMFMetaRecordHeader
    {
        public Int32 Seek;
        public SpoolerRecordTypes iType;
        public Int32 nSize;
    }
}
