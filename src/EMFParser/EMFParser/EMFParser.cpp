#include "stdafx.h"

#include "ApiParser.h"
#include "EMFParser.h"
#include "FileFormatParser.h"

EMFParser::EMFParser(const std::wstring &file_name, const ParserPolicy policy) {

    switch (policy) {
    case ParserPolicy::PP_WIN_API: {
        parser_ = new ApiParser(file_name);
        break;
    }
    case ParserPolicy::PP_FILE_FORMAT: {
        parser_ = new FileFormatParser(file_name);
        break;
    }
    default:
        break;
    }
}

EMFParser::~EMFParser() {
}

const std::wstring EMFParser::GetText() const {
    return parser_->GetText();
}
