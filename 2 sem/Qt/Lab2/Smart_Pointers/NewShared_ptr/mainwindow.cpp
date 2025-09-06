#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    setFixedSize(QSize(500,500));

    SharedArr = new MyShared<int>[10];

    SharedArr[0] = MyShared<int>(new int(10));
    for(int i = 1; i < 10; i++){
        SharedArr[i] = SharedArr[0];
    }
//    MyShared<int> gg (new int(10));
//    SharedArr[6] = gg;
    MainFunc();
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::MainFunc()
{
    ui->NewSharedTable->resize(this->width()-25,this->height()-100);

    QStandardItemModel* AddresList = new QStandardItemModel(10, 3, this);
    ui->NewSharedTable->setModel(AddresList);

    for(int i = 0; i < AddresList->rowCount(); i++){

      AddresList->setData(AddresList->index(i,0),QVariant("MyShared_ptr" + QString::number(i + 1)));
      AddresList->setData(AddresList->index(i,1),QVariant( SharedArr[i].Use_Count()));
      AddresList->setData(AddresList->index(i,2),QString::asprintf("%08p", SharedArr[i].get()));
    }

    AddresList->setHeaderData(0,Qt::Horizontal,"Name");
    AddresList->setHeaderData(1,Qt::Horizontal,"CountSharedptr");
    AddresList->setHeaderData(2,Qt::Horizontal,"Addres_Ptr");

}
