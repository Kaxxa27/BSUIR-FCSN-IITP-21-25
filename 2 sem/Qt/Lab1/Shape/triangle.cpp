#include "triangle.h"

Triangle::Triangle()
{
    Center = QPointF(0,0);
    startPosX = 300;
    startPosY = 250;
    angle = 3;


    Dialog = new MyDialogTriangle();

    Dialog->activateWindow();
      if(Dialog->exec() == QDialog::Accepted){

          weight = Dialog->GETWeight();
          height = Dialog->GETWeight();
          RoX = Dialog->RoX();
          RoY = Dialog->RoY();
     }
      else{
          this->deleteLater();
     }
}

MyDialogTriangle::MyDialogTriangle(QWidget *)
{
    this->setWindowTitle("Треугольник");
    this->setGeometry(600,250,325,150);

    QLabel *TextWeight = new QLabel(this);
    TextWeight->setText("Введите сторону: ");
    TextWeight->setGeometry(25,20,100,15);

    LineWeight = new QSpinBox(this);
    LineWeight->setRange(1,500);
    LineWeight->setValue(100);
    LineWeight->setGeometry(25,40,100,20);

//RotationDialog

    QLabel *TextRo = new QLabel(this);
    TextRo->setText("Координаты точки поворота ");
    TextRo->setGeometry(155,0,150,15);

    QLabel *TextRoX = new QLabel(this);
    TextRoX->setText("Введите Х: ");
    TextRoX->setGeometry(175,20,100,15);

    SpinRoX = new QSpinBox(this);
    SpinRoX->setRange(1,500);
    SpinRoX->setValue(100);
    SpinRoX->setGeometry(175,40,100,20);

    QLabel *TextRoY = new QLabel(this);
    TextRoY->setText("Введите Y: ");
    TextRoY->setGeometry(175,60,100,15);

    SpinRoY = new QSpinBox(this);
    SpinRoY->setRange(1,500);
    SpinRoY->setValue(100);
    SpinRoY->setGeometry(175,80,100,20);

    ButtonSave = new QPushButton(this);
    ButtonSave->setText("Save");
    ButtonSave->setGeometry(25,110,50,20);

    ButtonClose = new QPushButton(this);
    ButtonClose->setText("Close");
    ButtonClose->setGeometry(200,110,50,20);

    connect( ButtonSave, SIGNAL(clicked()), this, SLOT(GETWeight()));
    connect( ButtonSave, SIGNAL(clicked()), this, SLOT(GETWeight()));

    connect( ButtonSave, SIGNAL(clicked()), this, SLOT(accept()));
    connect( ButtonClose, SIGNAL(clicked()), this, SLOT(reject()));
}

QRectF Triangle::boundingRect() const
{
    return QRectF(0,0,1200,1200);
}

void Triangle::paint(QPainter *painter, const QStyleOptionGraphicsItem *, QWidget *)
{


    QPolygon *Trian = new QPolygon;
    *Trian << QPoint(startPosX,startPosY) << QPoint(startPosX + weight,startPosY) <<
             QPoint(startPosX + weight / 2,startPosY - height / 2) << QPoint(startPosX,startPosY);
    painter->drawPolyline(*Trian);
   setFlags(QGraphicsItem::ItemIsMovable);

    ButtonInfo = new QPushButton("Info");
    ButtonInfo->setGeometry(650,0,80,30);
    scene()->addWidget(ButtonInfo);
    connect(ButtonInfo,SIGNAL(clicked()), this, SLOT(InfoAboutRect()));

    ButtonRotation = new QPushButton("Rotation");
    ButtonRotation->setGeometry(650,40,80,30);
    scene()->addWidget(ButtonRotation);
    connect(ButtonRotation, SIGNAL(clicked()),this,SLOT(RotatoinRect()));
}

void Triangle::InfoAboutRect()
{
    QRectF rec = boundingRect();
    Center = QPointF(rec.x(),rec.y());
    int Area = (weight * weight * sqrt(3)) / 4 ;
    int Perimetr = weight * 3;

    int CentrX = this->x() + weight / 2 ;
    int CentrY = this->y() + height / 2 ;


    info = new QDialog();
    info->setWindowTitle("Информация");


    QLabel *InfoLabel = new QLabel(info);
    InfoLabel->setGeometry(70,0,100,100);
    InfoLabel->setText("Площадь: " + QString::number(Area)
                       + "\nПериметр: " + QString::number(Perimetr)
                       + "\nЦентр масс:\nПо Х: " + QString::number(CentrX)
                       + "\nПо Y: " + QString::number(CentrY));

    info->open();
}

void Triangle::RotatoinRect()
{

  setTransformOriginPoint(RoX,RoY);
  this->setRotation(angle);
  angle += 3;
}



int MyDialogTriangle::GETWeight()
{
    return LineWeight->value();
}
int MyDialogTriangle::RoX()
{
    return SpinRoX->value();
}

int MyDialogTriangle::RoY()
{
    return SpinRoY->value();
}
