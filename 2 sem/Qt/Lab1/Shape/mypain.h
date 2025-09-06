#ifndef MYPAIN_H
#define MYPAIN_H
#include "figure.h"

#include <QWidget>
#include <QPainter>
#include <QMouseEvent>

class MyPain : public QWidget
{
    Q_OBJECT
public:
    MyPain(QWidget *parent = nullptr);
protected:
    void paintEvent(QPaintEvent *event);
    void mousePressEvent(QMouseEvent *event);
    void mouseMoveEvent(QMouseEvent *event);
    void mouseReleaseEvent(QMouseEvent *event);
    void mouseDoubleClickEvent(QMouseEvent *event);

    bool LeftMousButPressed;
    bool MouseDraw;
    bool Del;

    QVector<QPointF> pointList;
    QPointF endPoint;
    QPen MyPen;

};

#endif // MYPAIN_H
