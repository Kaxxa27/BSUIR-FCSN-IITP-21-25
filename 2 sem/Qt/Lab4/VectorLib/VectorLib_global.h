#ifndef VECTORLIB_GLOBAL_H
#define VECTORLIB_GLOBAL_H

#include <QtCore/qglobal.h>

#if defined(VECTORLIB_LIBRARY)
#  define VECTORLIB_EXPORT Q_DECL_EXPORT
#else
#  define VECTORLIB_EXPORT Q_DECL_IMPORT
#endif

#endif // VECTORLIB_GLOBAL_H
