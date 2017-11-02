#pragma once

#ifdef EMFPARSER_EXPORTS
#define EMFPARSERAPI __declspec(dllexport)
#else
#define EMFPARSERAPI __declspec(dllimport)
#endif

#include <string>

#include "IEMFParser.h"

enum class ParserPolicy {
    PP_WIN_API,
    PP_FILE_FORMAT
};

class EMFPARSERAPI EMFParser {
  public:
    EMFParser(const std::wstring &file_name, const ParserPolicy policy);
    ~EMFParser();

    const std::wstring GetText() const;

  private:
    IEMFParser *parser_;

};

