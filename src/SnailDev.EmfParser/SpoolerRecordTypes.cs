using System;
using System.Collections.Generic;
using System.Text;

namespace SnailDev.EmfParser
{
    /// <summary>
    /// SPL数据类型
    /// </summary>
    public enum SpoolerRecordTypes
    {
        SRT_EOF = 0x0,                  //' int32 zero
        SRT_RESERVED_1 = 0x1,           //' 1                                   */
        SRT_FONTDATA = 0x2,             //' 2 Font Data                         */
        SRT_DEVMODE = 0x3,              //' 3 DevMode                           */
        SRT_FONT2 = 0x4,                //' 4 Font Data                         */
        SRT_RESERVED_5 = 0x5,           //' 5                                   */
        SRT_FONT_MM = 0x6,              //' 6 Font Data (Multiple Master)       */
        SRT_FONT_SUB1 = 0x7,            //' 7 Font Data (SubsetFont 1)          */
        SRT_FONT_SUB2 = 0x8,            //' 8 Font Data (SubsetFont 2)      
        SRT_RESERVED_9 = 0x9,
        SRT_UNKNOWN = 0x10,             //' 10 int unknown...
        SRT_RESERVED_A = 0xA,
        SRT_RESERVED_B = 0xB,
        SRT_PAGE = 0xC,                 //' 12  Enhanced Meta File (EMF)        */
        SRT_EOPAGE1 = 0xD,              //' 13  EndOfPage                       */
        SRT_EOPAGE2 = 0xE,              //' 14  EndOfPage                       */
        SRT_EXT_FONT = 0xF,             //' 15  Ext Font Data                   */
        SRT_EXT_FONT2 = 0x10,           //' 16  Ext Font Data                   */
        SRT_EXT_FONT_MM = 0x11,         //' 17  Ext Font Data (Multiple Master)
        SRT_EXT_FONT_SUB1 = 0x12,       //' 18  Ext Font Data (SubsetFont 1)    */
        SRT_EXT_FONT_SUB2 = 0x13,       //' 19  Ext Font Data (SubsetFont 2)    */
        SRT_EXT_PAGE = 0x14,            //' 20  Enhanced Meta File? 
        SRT_JOB_INFO = 0x10000          //' // int length, wchar jobDescription
    }
}
