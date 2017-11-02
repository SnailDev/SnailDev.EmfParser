// SPLParser.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

#include "IEMFParser.h"

IEMFParser::IEMFParser(const std::wstring &file_name)
    :file_name_(file_name) {

}

IEMFParser::~IEMFParser() {

}

