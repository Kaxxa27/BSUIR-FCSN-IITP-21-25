#ifndef STAR_H
#define STAR_H
#include "figure.h"


class MyDialogStar : public QDialog
{
    Q_OBJECT
public:
    MyDialogStar(QWidget *parent = nullptr);

    QPushButton *ButtonSave;
    QPushButton *ButtonClose;


    QSpinBox *LineWeight;
    QSpinBox *LineHeight;

    QSpinBox *SpinRoX;
    QSpinBox *SpinRoY;

    int GETCount();
    int GETRadius();

    int RoX();
    int RoY();

};
class Star : public Figure
{
    Q_OBJECT
public:
    Star();
    virtual QRectF boundingRect() const override;
    virtual void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget) override;
private:
    QPushButton *ButtonInfo;

    QPushButton *ButtonLeft;
    QPushButton *ButtonRight;
    QPushButton *ButtonUp;
    QPushButton *ButtonDown;
    QPushButton *ButtonRotation;
    QPushButton *ButtonRotationOff;

    QDialog *info;
    MyDialogStar* Dialog;

    int Count;
    int R;

    QPoint* points;

    int weight;
    int height;

    int RoX;
    int RoY;

    QPointF Center;
    int startPosX;
    int startPosY;
    int angle;

    QTimer *RoTimer;

private slots:
    void InfoAboutRect();
    void RotatoinRect();
};


#endif // STAR_H
