#include "mypain.h"

MyPain::MyPain(QWidget *parent)
    : QWidget(parent)
{
    setMouseTracking(true);
    LeftMousButPressed = false;
    MouseDraw = false;

    MyPen.setWidth(5);
    MyPen.setColor(Qt::green);
}


void MyPain::paintEvent(QPaintEvent *) {

    QPainter painter(this);

    painter.setPen(MyPen);
    QVector<QLineF> lines;

    for (int i = 0; i < pointList.size() - 1; i++) {
        QLineF line(pointList[i], pointList[i+1]);
        lines.push_back(line);
    }

    if (MouseDraw && pointList.size() > 0) {
        QLineF line(pointList.back(), endPoint);
        lines.push_back(line);
    }

    painter.drawLines(lines);
}


void MyPain::mousePressEvent(QMouseEvent *) {

    if (!LeftMousButPressed) {
        pointList.clear();
        LeftMousButPressed = true;
        update();
    }
}


void MyPain::mouseMoveEvent(QMouseEvent *event) {

    if (LeftMousButPressed) {
        endPoint = event->pos();
        MouseDraw = true;
        update();
    }
}


void MyPain::mouseReleaseEvent(QMouseEvent *event) {

    if (LeftMousButPressed) {
        pointList.push_back(event->pos());
        MouseDraw = false;
        update();
    }
}


void MyPain::mouseDoubleClickEvent(QMouseEvent *) {

    LeftMousButPressed = false;
    pointList.push_back(pointList[0]);
    update();
}
