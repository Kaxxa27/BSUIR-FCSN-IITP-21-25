#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "rect.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    scene = new QGraphicsScene(this);

    ui->graphicsView->setScene(scene);
    ui->graphicsView->setRenderHint(QPainter::Antialiasing);

    scene->setSceneRect(250,-100,300,300);

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

    QLineF Rightline(800,-300,800,300);

    scene->addLine(Rightline,mypen);



    mTimer = new QTimer(this);
    connect(mTimer,SIGNAL(timeout()),scene,SLOT(advance()));
    mTimer->start(100);


}

MainWindow::~MainWindow()
{
    delete ui;
}

