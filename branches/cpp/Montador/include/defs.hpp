#ifndef DEFS_HPP
#define DEFS_HPP

#define LOG10 1.301029995663981195

#define ISWHITESPACE(c) ((c)==' ' || (c)=='\t')
#define ISEOL(c) ((c)=='\r' || (c)=='\n')

typedef enum Exceptions {eFileNotFound,eInvalidFormat,eUnmatchedExpression,eOpenString} e_exception;

#endif // DEFS_HPP

