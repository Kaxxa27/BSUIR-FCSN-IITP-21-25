#include "rect.h"

Rect::Rect()
{
    speed = 20;
}

QRectF Rect::boundingRect() const
{
    return QRectF(0,0,100,50);
}

void Rect::paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget)
{
    QRectF rec = boundingRect();
    QBrush Brush(Qt::black);
    if(scene()->collidingItems(this).isEmpty()){
        //no collision
        Brush.setColor(Qt::green);
    }
    else{
        DoCollision();
    }
    painter->fillRect(rec,Brush);
    painter->drawRect(rec);
}

void Rect::advance(int phase)
{
    if(!phase) return;

    QPointF location = this->pos();

    setPos(mapToParent(speed,0));
}

void Rect::DoCollision()
{
    setPos(0,0);
}
