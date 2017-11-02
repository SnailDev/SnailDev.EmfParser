#pragma once

#ifdef SPLPARSER_EXPORTS
#define SPLPARSERAPI __declspec(dllexport)
#else
#define SPLPARSERAPI __declspec(dllimport)
#endif

#include <string>

#pragma warning(disable:4200 4251)

class SPLPARSERAPI SPLParser {
  public:
    SPLParser(const std::wstring &spl_file);
    ~SPLParser();

    bool ExtractEMFFile();

  private:
    const std::wstring file_name_;

};

typedef enum {
    EMRI_METAFILE = 0x00000001,
    EMRI_ENGINE_FONT = 0x00000002,
    EMRI_DEVMODE = 0x00000003,
    EMRI_TYPE1_FONT = 0x00000004,
    EMRI_PRESTARTPAGE = 0x00000005,
    EMRI_DESIGNVECTOR = 0x00000006,
    EMRI_SUBSET_FONT = 0x00000007,
    EMRI_DELTA_FONT = 0x00000008,
    EMRI_FORM_METAFILE = 0x00000009,
    EMRI_BW_METAFILE = 0x0000000A,
    EMRI_BW_FORM_METAFILE = 0x0000000B,
    EMRI_METAFILE_DATA = 0x0000000C,
    EMRI_METAFILE_EXT = 0x0000000D,
    EMRI_BW_METAFILE_EXT = 0x0000000E,
    EMRI_ENGINE_FONT_EXT = 0x0000000F,
    EMRI_TYPE1_FONT_EXT = 0x00000010,
    EMRI_DESIGNVECTOR_EXT = 0x00000011,
    EMRI_SUBSET_FONT_EXT = 0x00000012,
    EMRI_DELTA_FONT_EXT = 0x00000013,
    EMRI_PS_JOB_DATA = 0x00000014,
    EMRI_EMBED_FONT_EXT = 0x00000015
} SPLRecordType;

typedef struct tagSPLHeader {
    uint32_t version;
    uint32_t header_size;
    uint32_t doc_name_offset;
    uint32_t device_name_offset;
    uint16_t  header_data[];
} SPLHeader, *PSPLHeader;

typedef struct tagDataRecord {
    uint32_t type;
    uint32_t size;
    uint32_t data[];
} DataRecord, *PDataRecord;

typedef struct tagDataHeader {
    uint32_t type;
    uint32_t size;
} DataHeader, *PDataHeader;

typedef struct tagPageRecord {
    uint32_t type;
    uint32_t size;
    uint32_t data[];
} PageRecord, *PPageRecord;


