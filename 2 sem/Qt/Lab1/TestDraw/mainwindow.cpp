#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "rect.h"
#include "shassi.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    scene = new QGraphicsScene(this);

    ui->graphicsView->setScene(scene);
    ui->graphicsView->setRenderHint(QPainter::Antialiasing);

    scene->setSceneRect(-200,0,700,500);

    QPen mypen(Qt::blue);
    mypen.setWidth(1);

//    QLineF Topline(scene->sceneRect().topLeft(),scene->sceneRect().topRight());
//    QLineF Leftline(scene->sceneRect().topLeft(),scene->sceneRect().bottomLeft());
//    QLineF Rightline(scene->sceneRect().topRight(),scene->sceneRect().bottomRight());
//    QLineF Bottomline(scene->sceneRect().bottomLeft(),scene->sceneRect().bottomRight());
//    scene->addLine(Topline,mypen);
//    scene->addLine(Leftline,mypen);
//    scene->addLine(Rightline,mypen);
//    scene->addLine(Bottomline,mypen);
//    QLineF Rightline(800,-300,800,300);
//    scene->addLine(Rightline,mypen);
//    Rect *rec = new Rect();

    i = 0;

    rec = new QGraphicsRectItem(-200,250,200,50);
    scene->addItem(rec);
    rec->setBrush(Qt::darkGreen);

    shassi2 = new QGraphicsEllipseItem(-35,315,10,10);
    shassi3 = new QGraphicsEllipseItem(-165,315,10,10);

    shassi2->setBrush(Qt::black);
    shassi3->setBrush(Qt::black);

    lineGroup = new QGraphicsItemGroup();
    lineGroup->addToGroup(scene->addLine(-200,300,-250,250));
    lineGroup->addToGroup(scene->addLine(-60,220,-30,250));
    lineGroup->addToGroup(scene->addLine(-130,220,-160,250));
    lineGroup->addToGroup(scene->addLine(-250,250,-200,250));
    lineGroup->addToGroup(scene->addLine(-200,250,-250,200));
    lineGroup->addToGroup(scene->addLine(-250,200,-250,250));
    itemElemetLine = lineGroup;
    scene->addItem(itemElemetLine);

    line1 = new QGraphicsLineItem(-25,300,-30,320);
    line2 = new QGraphicsLineItem(-30,320,-35,300);
    line3 = new QGraphicsLineItem(-155,300,-160,320);
    line4 = new QGraphicsLineItem(-160,320,-165,300);

    rect1 = new QGraphicsRectItem(0,270,10,10);
    scene->addItem(rect1);
    rect1->setBrush(Qt::black);

    rect2 = new QGraphicsRectItem(4,280,5,60);
    scene->addItem(rect2);
    rect2->setBrush(Qt::gray);

    rect3 = new QGraphicsRectItem(4,210,5,60);
    scene->addItem(rect3);
    rect3->setBrush(Qt::gray);

    rect4 = new QGraphicsRectItem(-130,220,70,30);
    scene->addItem(rect4);
//    rect4->setBrush(Qt::Blue);


//   shassi *sh = new shassi();
//   scene->addItem(sh);

    mTimer = new QTimer(this);
   shTimer = new QTimer(this);

    connect(mTimer,SIGNAL(timeout()),this,SLOT(rec_go()));
//    connect(mTimer,SIGNAL(timeout()),scene,SLOT(advance()));
//    connect(mTimer,SIGNAL(timeout()),scene,SLOT(advance2()));
    mTimer->start(10);
    shTimer->start(100);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::SH()
{
    //держатели шасси
    scene->addItem(line1);
    scene->addItem(line2);
    scene->addItem(line3);
    scene->addItem(line4);

    line1->setPos(i,0);
    line2->setPos(i,0);
    line3->setPos(i,0);
    line4->setPos(i,0);

    //шасси
    scene->addItem(shassi2);
    scene->addItem(shassi3);

    shassi2->setPos(i,0);
    shassi3->setPos(i,0);

    if( i >= (scene->width()-100)){
        i = 0;
    }
}
void MainWindow::rec_go()
{
    rec->setPos(i,0);

    line1->setPos(i,0);
    line2->setPos(i,0);
    line3->setPos(i,0);
    line4->setPos(i,0);

    itemElemetLine->setPos(i,0);

    rect1->setPos(i,0);
    rect2->setPos(i,0);
    rect3->setPos(i,0);
    rect4->setPos(i,0);

    if( i >= (scene->width()-100)){
        i = 0;
    }
    i+= 1;
}

void MainWindow::on_button_shassi_clicked()
{
    //ВЫПУСК ШАССИ
    connect(mTimer,SIGNAL(timeout()),this,SLOT(SH()));
}
void MainWindow::on_pushButton_clicked()
{
    //ЗАКРЫТИЕ ШАССИ

    delete line1;
    delete line2;
    delete line3;
    delete line4;

    delete shassi2;
    delete shassi3;
    disconnect(mTimer,SIGNAL(timeout()),this,SLOT(SH()));


    line1 = new QGraphicsLineItem(-25,300,-30,320);
    line2 = new QGraphicsLineItem(-30,320,-35,300);
    line3 = new QGraphicsLineItem(-155,300,-160,320);
    line4 = new QGraphicsLineItem(-160,320,-165,300);

    shassi2 = new QGraphicsEllipseItem(-35,315,10,10);
    shassi3 = new QGraphicsEllipseItem(-165,315,10,10);

    shassi2->setBrush(Qt::black);
    shassi3->setBrush(Qt::black);
}

