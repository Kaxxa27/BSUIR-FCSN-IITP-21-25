#ifndef STRINGLIB_GLOBAL_H
#define STRINGLIB_GLOBAL_H

#include <QtCore/qglobal.h>

#if defined(STRINGLIB_LIBRARY)
#  define STRINGLIB_EXPORT Q_DECL_EXPORT
#else
#  define STRINGLIB_EXPORT Q_DECL_IMPORT
#endif

#endif // STRINGLIB_GLOBAL_H
