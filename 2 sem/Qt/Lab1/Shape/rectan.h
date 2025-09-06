#ifndef RECTAN_H
#define RECTAN_H
#include "figure.h"

class MyDialogRect : public QDialog
{
    Q_OBJECT

public:
    MyDialogRect(QWidget *parent = nullptr);

    QPushButton *ButtonSave;
    QPushButton *ButtonClose;

    QSpinBox *LineWeight;
    QSpinBox *LineHeight;

    QSpinBox *SpinRoX;
    QSpinBox *SpinRoY;

    int GETWeight();
    int GETHeight();

    int RoX();
    int RoY();


};

class Rectan : public Figure
{
    Q_OBJECT
public:
    Rectan();
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



#endif // RECTAN_H
