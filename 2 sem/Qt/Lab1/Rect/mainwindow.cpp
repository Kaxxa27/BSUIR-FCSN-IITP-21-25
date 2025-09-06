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

    QLineF Rightline(800,-300,800,300);

    scene->addLine(Rightline,mypen);

    Rect *item = new Rect();
    scene->addItem(item);

    mTimer = new QTimer(this);
    connect(mTimer,SIGNAL(timeout()),scene,SLOT(advance()));
    mTimer->start(100);


}

MainWindow::~MainWindow()
{
    delete ui;
}

