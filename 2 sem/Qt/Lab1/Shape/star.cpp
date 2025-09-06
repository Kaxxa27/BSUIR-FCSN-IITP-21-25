#include "star.h"

Star::Star()
{
    Center = QPointF(0,0);
    startPosX = 300;
    startPosY = 250;
    angle = 3;

    Dialog = new MyDialogStar();

    Dialog->activateWindow();
      if(Dialog->exec() == QDialog::Accepted){

          Count = Dialog->GETCount();

          points = new QPoint[2 * Count];
          R = Dialog->GETRadius();
     }
      else{
          this->deleteLater();
     }

}

MyDialogStar::MyDialogStar(QWidget *)
{
    this->setWindowTitle("Звезда");
    this->setGeometry(600,250,325,150);

    QLabel *TextWeight = new QLabel(this);
    TextWeight->setText("Введите кол-во вершин: ");
    TextWeight->setGeometry(25,20,150,15);

    LineWeight = new QSpinBox(this);
    LineWeight->setRange(4,50);
    LineWeight->setValue(25);
    LineWeight->setGeometry(25,40,100,20);

    QLabel *TextHeihgt = new QLabel(this);
    TextHeihgt->setText("Введите радиус: ");
    TextHeihgt->setGeometry(25,60,100,15);

    LineHeight = new QSpinBox(this);
    LineHeight->setRange(50,200);
    LineHeight->setValue(100);
    LineHeight->setGeometry(25,80,100,20);

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

QRectF Star::boundingRect() const
{
    return QRectF(0,0,1200,1200);
}

void Star::paint(QPainter *painter, const QStyleOptionGraphicsItem *, QWidget *)
{
    double alpha = 3.14159 / Count;

    for (int i = 0; i < 2 * Count; i++) {
        if (i % 2) {
            points[i] = QPoint(R * cos(alpha * i), R * sin(alpha * i)) + QPoint(startPosX,startPosY);

        } else {
            points[i] = QPoint(0.5 * R * cos(alpha * i), 0.5 * R * sin(alpha * i)) + QPoint(startPosX,startPosY);
        }

    }

    QPolygon *Trian = new QPolygon;

    for (int i = 0; i < 2 * Count; i++){
        *Trian << points[i];
    }
    *Trian << points[0];
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

void Star::InfoAboutRect()
{
    QRectF rec = boundingRect();
    Center = QPointF(rec.x(),rec.y());
    int Area = abs(10 * sin(36 ) * sin(36) * tan(18) * R * R);
    int Perimetr = 2 * sqrt(pow(points[0].x() - points[1].x(), 2) + pow(points[0].y() - points[1].y(), 2)) * Count;


    int CentrX = this->x();
    int CentrY = this->y();


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

void Star::RotatoinRect()
{

  setTransformOriginPoint(RoX,RoY);
  this->setRotation(angle);
  angle += 3;
}



int MyDialogStar::GETCount()
{
    return LineWeight->value();
}
int MyDialogStar::GETRadius()
{
    return LineHeight->value();
}
int MyDialogStar::RoX()
{
    return SpinRoX->value();
}

int MyDialogStar::RoY()
{
    return SpinRoY->value();
}
