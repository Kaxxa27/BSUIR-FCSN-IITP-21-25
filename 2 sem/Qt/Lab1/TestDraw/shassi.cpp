#include "shassi.h"

shassi::shassi()
{
    speed = 5;
}

QRectF shassi::Shassi_obj() const
{
    return QRectF(25,53,20,20);
}

void shassi::paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget)
{
    QRectF shassi = Shassi_obj();

    QBrush Brush(Qt::red);

    painter->fillRect(shassi,Brush);
    painter->drawEllipse(QRectF(shassi));

}

void shassi::under_shassi()
{


}

void shassi::advance2(int phase)
{
    setPos(mapToParent(speed,0));
}

