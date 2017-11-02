#pragma once
#include "EMFParser.h"


class ApiParser : public IEMFParser {
  public:
    ApiParser(const std::wstring &file_name);
    ~ApiParser();

    virtual const std::wstring GetText() override;

  private:
    std::wstring text_;

    friend int CALLBACK EnhMetaFileProc(HDC hdc, HANDLETABLE* handle_table,
                                        CONST ENHMETARECORD* emf_record, int handles, LPARAM data);
};
