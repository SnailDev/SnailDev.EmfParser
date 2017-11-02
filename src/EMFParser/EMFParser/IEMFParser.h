#pragma once

#include <string>


class IEMFParser {

  public:
    IEMFParser(const std::wstring &file_name);
    ~IEMFParser();

    virtual const std::wstring GetText() = 0;

  protected:
    const std::wstring file_name_;
};



