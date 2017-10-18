using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser
{
    public class EMFTextRecord
    {
        #region "public members";
        public Int32 _Top;
        public Int32 _Left;
        public Int32 _Bottom;
        public Int32 _Right;
        public Int32 _GraphicsMode;
        public float _scaleX;
        public float _scaleY;
        //EMRTEXT
        public Int32 _PTx;
        public Int32 _PTy;
        public Int32 _nChars;
        public Int32 _offString;
        public Int32 _Options;
        public Int32 _TxtTop;
        public Int32 _TxtLeft;
        public Int32 _TxtBottom;
        public Int32 _TxtRight;
        public Int32 _offDX;
        public string _Text;
        #endregion

        public string Text
        {
            get
            {
                return _Text;
            }
        }

        public EMFTextRecord(int Length, BinaryReader SpoolBinaryReader)
        {

            _Top = SpoolBinaryReader.ReadInt32();
            _Left = SpoolBinaryReader.ReadInt32();
            _Bottom = SpoolBinaryReader.ReadInt32();
            _Right = SpoolBinaryReader.ReadInt32();
            _GraphicsMode = SpoolBinaryReader.ReadInt32();
            _scaleX = SpoolBinaryReader.ReadSingle();
            _scaleY = SpoolBinaryReader.ReadSingle();
            _PTx = SpoolBinaryReader.ReadInt32();
            _PTy = SpoolBinaryReader.ReadInt32();
            _nChars = SpoolBinaryReader.ReadInt32();
            _offString = SpoolBinaryReader.ReadInt32();
            _Options = SpoolBinaryReader.ReadInt32();
            _TxtTop = SpoolBinaryReader.ReadInt32();
            _TxtLeft = SpoolBinaryReader.ReadInt32();
            _TxtBottom = SpoolBinaryReader.ReadInt32();
            _TxtRight = SpoolBinaryReader.ReadInt32();
            _offDX = SpoolBinaryReader.ReadInt32();
            if (_offString >= 76)
            {
                SpoolBinaryReader.BaseStream.Seek(_offString - 76, SeekOrigin.Current);
                Char[] chars;
                chars = SpoolBinaryReader.ReadChars(_nChars);
                _Text = new String(chars);
            }
        }
    }
}
