#ifndef RECT_H
#define RECT_H

#include <QPainter>
#include <QGraphicsItem>
#include <QGraphicsScene>

class Rect : public QGraphicsItem
{
public:
    Rect();
    QRectF boundingRect() const;
    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget);

protected:
    void advance(int phase);

private:
    qreal angle;
    qreal speed;
    void DoCollision();
    void Chassi();

};

#endif // RECT_H
