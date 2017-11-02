// SPLParser.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

#include <fstream>

#include "SPLParser.h"

SPLParser::SPLParser(const std::wstring& spl_file):file_name_(spl_file) {

}

SPLParser::~SPLParser() {
}

bool SPLParser::ExtractEMFFile() {
    std::ifstream file(file_name_, std::ios::binary);
    if (!file.is_open()) {
        return false;
    }

    //读取文件头
    DataHeader header = {0};
    file.read((char*)&header, sizeof(DataHeader));
    file.seekg(header.size - sizeof(DataHeader), std::ios::cur);

    //读取记录
    while (!file.eof()) {
        header = { 0 };
        file.read((char*)&header, sizeof(DataHeader));

        if (EMRI_METAFILE_DATA == header.type) {
            std::ofstream emf_file("001.emf", std::ios::binary | std::ios::trunc);
            char *buf = new char[header.size];
            memset(buf, 0, header.size);

            file.read(buf, header.size - sizeof(DataHeader));

            uint32_t read_count = (uint32_t)file.gcount();
            if (read_count != header.size - sizeof(DataHeader)) {
                return false;
            }
            emf_file.write(buf, header.size);
        } else if (header.size > sizeof(DataHeader)) {
            file.seekg(header.size - sizeof(DataHeader), std::ios::cur);
        }
    }

    return true;
}
