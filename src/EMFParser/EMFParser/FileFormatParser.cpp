#include "stdafx.h"

#include <fstream>

#include "FileFormatParser.h"


FileFormatParser::FileFormatParser(const std::wstring &file_name)
    :IEMFParser(file_name) {

}

FileFormatParser::~FileFormatParser() {
}

const std::wstring FileFormatParser::GetText() {

    std::ifstream file(file_name_.c_str(), std::ios::binary);
    if (!file.is_open()) {
        text_.clear();
        return text_;
    }

    RecordHeader header = {0};

    file.read((char*)&header, sizeof(RecordHeader));

    EMFRecord *record = nullptr;
    EMRExtTextOutW *temp = nullptr;

    while (header.type != M_EMR_EOF) {

        if (M_EMR_EXTTEXTOUTW == header.type) {
            record = { 0 };
            record = (EMFRecord*)malloc(header.size);
            record->type = header.type;
            record->size = header.size;
            file.read((char*)record->data, header.size - sizeof(RecordHeader));

            temp = (PEMRExtTextOutW)record;
            text_.append((wchar_t*)temp->object.buf, temp->object.chars);
            text_.append(L"/r/n");

        } else {
            file.seekg(header.size - sizeof(RecordHeader), std::ios::cur);
        }

        file.read((char*)&header, sizeof(RecordHeader));
    }

    return text_;
}
