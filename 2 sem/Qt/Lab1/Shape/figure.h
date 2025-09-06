#ifndef FIGURE_H
#define FIGURE_H

#include <QGraphicsItem>
#include <QGraphicsView>
#include <QGraphicsScene>
#include <QInputDialog>
#include <QWidget>
#include <QMessageBox>
#include <QTimer>
#include <QSpinBox>
#include <QLabel>
#include <QPushButton>
#include <QGraphicsSceneWheelEvent>
#include <QGraphicsItem>


class Figure : public QGraphicsItem, public QObject
{
public:

    Figure();
    virtual QRectF boundingRect() const override;
    virtual void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget) override;


};



#endif // FIGURE_H
