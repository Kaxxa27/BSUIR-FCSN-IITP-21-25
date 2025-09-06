#ifndef SHASSI_H
#define SHASSI_H

#include "rect.h"
#include <QPainter>
#include <QGraphicsItem>
#include <QGraphicsScene>
#include <QTimer>
#include <QLabel>

class shassi :  public Rect
{
public:
    shassi();
    QRectF Shassi_obj() const;
    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget);
    QTimer shTimer;




protected:
    void under_shassi();
    void advance2(int phase);
private:
    qreal speed;
};

#endif // SHASSI_H
