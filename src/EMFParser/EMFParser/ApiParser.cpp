#include "stdafx.h"

#include <fstream>

#include "ApiParser.h"

ApiParser::ApiParser(const std::wstring &file_name)
    :IEMFParser(file_name) {
}

ApiParser::~ApiParser() {
}

int CALLBACK EnhMetaFileProc(HDC hdc, HANDLETABLE* handle_table,
                             CONST ENHMETARECORD* emf_record, int handles, LPARAM data) {

    PlayEnhMetaFileRecord(hdc, handle_table, emf_record, handles);

    PEMREXTTEXTOUTW ptext = NULL;

    if (emf_record->iType == 84) {
        ptext = (PEMREXTTEXTOUTW)emf_record;
        WCHAR *psrc = (WCHAR*)ptext + ptext->emrtext.offString / sizeof(WCHAR);
        ApiParser *pthis = (ApiParser*)data;
        pthis->text_.append(psrc, ptext->emrtext.nChars);
        pthis->text_.append(L"\r\n");
    }

    return TRUE;
}

const std::wstring ApiParser::GetText() {

    HENHMETAFILE hemf = GetEnhMetaFile(file_name_.c_str());

    if (NULL == hemf) {
        text_.clear();
        return text_;
    }

    EnumEnhMetaFile(NULL, hemf, EnhMetaFileProc, this, NULL);
    DeleteEnhMetaFile(hemf);

    return text_;
}


