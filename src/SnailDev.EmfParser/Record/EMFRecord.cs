using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace SnailDev.EmfParser
{
    public class EMFRecord
    {
        #region "public properties";
        public Int32 _Seek;
        public Int32 _Type;
        public Int32 _Size;
        #endregion;

        #region "public interface";
        public EmfPlusRecordType Type
        {
            get
            {
                // return CType(_Type, EmfPlusRecordType);
                return ((EmfPlusRecordType)(_Type));
            }
        }

        public Int32 Size
        {
            get
            {
                return _Size;
            }
        }

        public Int32 Seek
        {
            get
            {
                return _Seek;
            }
        }
        #endregion;

        #region "public constructor";
        public EMFRecord(BinaryReader SpoolBinaryReader)
        {

            _Seek = (Int32)SpoolBinaryReader.BaseStream.Position;
            _Type = SpoolBinaryReader.ReadInt32();
            _Size = SpoolBinaryReader.ReadInt32();
            if (Type == EmfPlusRecordType.EmfExtTextOutA || Type == EmfPlusRecordType.EmfExtTextOutW)
            {
                EMFTextRecord foo = new EMFTextRecord(_Size - 8, SpoolBinaryReader);
                Debug.WriteLine(foo.Text);
            }

        }
        #endregion
    }
}
