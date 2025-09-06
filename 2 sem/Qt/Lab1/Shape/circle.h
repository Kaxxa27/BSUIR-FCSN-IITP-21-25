#ifndef CIRCLE_H
#define CIRCLE_H
#include "figure.h"

class MyDialogCircle : public QDialog
{
    Q_OBJECT

public:
    MyDialogCircle(QWidget *parent = nullptr);

    QPushButton *ButtonSave;
    QPushButton *ButtonClose;


    QSpinBox *LineWeight;

    QSpinBox *SpinRoX;
    QSpinBox *SpinRoY;

    int GETWeight();

    int RoX();
    int RoY();


};

class Circle : public Figure
{
    Q_OBJECT
public:
    Circle();
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
    MyDialogCircle* Dialog;

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


#endif // CIRCLE_H
