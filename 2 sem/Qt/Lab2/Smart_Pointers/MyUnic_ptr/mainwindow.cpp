#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QStandardItemModel>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);


    setFixedSize(QSize(242,400));

    MyUnicArr = new MyUnic_ptr<int>[5];
    for(int i = 0; i < 5; i++){
        MyUnicArr[i] = MyUnic_ptr<int>();
    }

//    QDialog *Dialog1 = new QDialog();
//    Dialog1->show();

    MainFunction();
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::MainFunction()
{

//    MyUnicArr[0] = MyUnic_ptr<int>(new int(12345));
//    MyUnicArr[1] = MyUnic_ptr<int>(new int(12345));
    ui->AddresInfo->resize(this->width()-25,this->height()-100);
    ui->AddresInfo->setColumnWidth(0,100);
    QStandardItemModel* AddresList = new QStandardItemModel(5, 2, this);

    ui->AddresInfo->setModel(AddresList);

    for(int i = 0; i < AddresList->rowCount(); i++){

        QString AddresPtr, AddresUnic;

//        AddresPtr.asprintf("%8p", MyUnicArr[i].operator->());
//        AddresUnic.asprintf("%8p", &MyUnicArr[i]);

      AddresList->setData(AddresList->index(i,0),QVariant("MyUnic_ptr" + QString::number(i + 1)));
      AddresList->setData(AddresList->index(i,1),QString::asprintf("%08p", MyUnicArr[i].operator->()));

    }

    AddresList->setHeaderData(0,Qt::Horizontal,"Name");
    AddresList->setHeaderData(1,Qt::Horizontal,"Addres_Ptr");

}

void MainWindow::StartFunc()
{
//    MyUnicArr[Dialog->GETIndex() - 1].reset(new int(rand() % 100));
//    MainFunction();
}



template<class F>
typename std::remove_reference<F>::type&& MainWindow::MyMove(F &&t) noexcept
{
    return static_cast<typename std::remove_reference<F>::type&&>(t);
}

void MainWindow::on_MakeButton_clicked()
{

    Dialog = new MyDialog(1);
    Dialog->show();

    if(Dialog->exec() == QDialog::Accepted){

        MyUnicArr[Dialog->GETIndex() - 1].reset(new int(rand() % 100));
        MainFunction();
    }


}


void MainWindow::on_SwapButton_clicked()
{
    Dialog = new MyDialog(2,0);
    Dialog->show();

    if(Dialog->exec() == QDialog::Accepted){
        MyUnicArr[Dialog->GETIndex() - 1].swap(MyUnicArr[Dialog->GETIndex2()- 1]);
        MainFunction();
    }
}


void MainWindow::on_RemoveButton_clicked()
{
    Dialog = new MyDialog(3);
    Dialog->show();

    if(Dialog->exec() == QDialog::Accepted){
        MyUnicArr[Dialog->GETIndex() - 1].release();
        MainFunction();
    }

}
void MainWindow::on_MoveButton_clicked()
{
    Dialog = new MyDialog(4,0);
    Dialog->show();

    if(Dialog->exec() == QDialog::Accepted){
        MyUnicArr[Dialog->GETIndex() - 1].Move(MyUnicArr[Dialog->GETIndex2() - 1]);
        MainFunction();
    }
}

MyDialog::MyDialog(int Num, QWidget*)
{

    switch(Num){
        case 1://Make
            this->setFixedSize(150,70);
            this->setWindowTitle("Make_Unic_ptr");
            break;
        case 3://Remove
            this->setFixedSize(150,70);
            this->setWindowTitle("Remove_Unic_ptr");
            break;
    }

    QLabel *TextIndex = new QLabel(this);
    TextIndex->setText("Номер Unic_ptr: ");
    TextIndex->setGeometry(10,10,100,15);

    IndexBox = new QSpinBox(this);
    IndexBox->setGeometry(100,10,30,20);
    IndexBox->setRange(1,5);


    ButtonSave = new QPushButton(this);
    ButtonSave->setText("Save");
    ButtonSave->setGeometry(10,35,60,20);

    ButtonClose = new QPushButton(this);
    ButtonClose->setText("Close");
    ButtonClose->setGeometry(80,35,60,20);

    connect( ButtonSave, SIGNAL(clicked()), this, SLOT(GETIndex()));
    connect( ButtonSave, SIGNAL(clicked()), this, SLOT(accept()));
    connect( ButtonClose, SIGNAL(clicked()), this, SLOT(reject()));

}

MyDialog::MyDialog(int Num, int x, QWidget *)
{
     switch(Num){
     case 2:{//Swap
         this->setFixedSize(160,110);
         this->setWindowTitle("Swap_Unic_ptr");
        }
     case 4:{//Move
         this->setFixedSize(160,110);
         this->setWindowTitle("Move_Unic_ptr");
        }
     }

     //Second ptr
     QLabel *TextIndex2 = new QLabel(this);
     TextIndex2->setText("Номер Unic_ptr№2: ");
     TextIndex2->setGeometry(10,30,110,45);

     IndexBox2 = new QSpinBox(this);
     IndexBox2->setGeometry(120,42,30,20);
     IndexBox2->setRange(1,5);

     //First ptr
     QLabel *TextIndex = new QLabel(this);
     TextIndex->setText("Номер Unic_ptr№1: ");
     TextIndex->setGeometry(10,10,110,15);

     IndexBox = new QSpinBox(this);
     IndexBox->setGeometry(120,10,30,20);
     IndexBox->setRange(1,5);


     ButtonSave = new QPushButton(this);
     ButtonSave->setText("Save");
     ButtonSave->setGeometry(10,75,60,20);

     ButtonClose = new QPushButton(this);
     ButtonClose->setText("Close");
     ButtonClose->setGeometry(90,75,60,20);

     connect( ButtonSave, SIGNAL(clicked()), this, SLOT(GETIndex()));
     connect( ButtonSave, SIGNAL(clicked()), this, SLOT(accept()));
     connect( ButtonClose, SIGNAL(clicked()), this, SLOT(reject()));
}

int MyDialog::GETIndex()
{
    return IndexBox->value();
}

int MyDialog::GETIndex2()
{
    return IndexBox2->value();
}
