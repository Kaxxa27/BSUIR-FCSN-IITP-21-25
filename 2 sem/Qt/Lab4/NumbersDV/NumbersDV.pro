QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++11

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    main.cpp \
    mainwindow.cpp

HEADERS += \
    mainwindow.h

FORMS += \
    mainwindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target

win32:CONFIG(release, debug|release): LIBS += -L$$PWD/../build-VectorLib-Desktop_Qt_6_2_3_MinGW_64_bit-Debug/release/ -lVectorLib
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/../build-VectorLib-Desktop_Qt_6_2_3_MinGW_64_bit-Debug/debug/ -lVectorLib

INCLUDEPATH += $$PWD/../VectorLib
DEPENDPATH += $$PWD/../VectorLib
