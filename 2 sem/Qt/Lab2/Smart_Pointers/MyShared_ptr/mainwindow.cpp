#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    setFixedSize(QSize(800,400));

    SharedArr = new MyShared<int>[10]();
    WeakArr = new Weak<int>[10]();

    SharedArr[0] = MyShared<int>(new int(10));
    for(int i = 1; i < 10; i++){
        SharedArr[i] = SharedArr[0];
    }

    for(int i = 0; i < 10; i++){
        WeakArr[i] = SharedArr[i];
    }

    MyShared<int> gg (new int(10));

//    WeakArr[0] = gg;
//    WeakArr[1] = WeakArr[0];
//    qDebug() << WeakArr[5].Use_Weak_Count();

    MainFunc();

//****************************************************
//    for(int i = 0; i < 10; i++){
//        qDebug() << SharedArr[i].get();
//    }

//    for(int i = 0; i < 10; i++){
//        qDebug() << SharedArr[i].operator*();
//    }
//    struct hel{
//        int* ptr;
//        int count;
//    };
//    hel* h = new hel();
//    qDebug() << h->count;
//    qDebug() << &h->count;

}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::MainFunc()
{
    ui->SharedTable->resize(325,328);
    ui->WeakTable->resize(425,328);

    //shared
    QStandardItemModel* AddresList = new QStandardItemModel(10, 3, this);
    ui->SharedTable->setModel(AddresList);

    for(int i = 0; i < AddresList->rowCount(); i++){

      AddresList->setData(AddresList->index(i,0),QVariant("MyShared_ptr" + QString::number(i + 1)));
      AddresList->setData(AddresList->index(i,1),QVariant( SharedArr[i].Use_Count()));
      AddresList->setData(AddresList->index(i,2),QString::asprintf("%08p", SharedArr[i].get()));
    }

    AddresList->setHeaderData(0,Qt::Horizontal,"Name");
    AddresList->setHeaderData(1,Qt::Horizontal,"CountSharedptr");
    AddresList->setHeaderData(2,Qt::Horizontal,"Addres_Ptr");

    //weak
    QStandardItemModel* AddresWeak = new QStandardItemModel(10, 4, this);
    ui->WeakTable->setModel(AddresWeak);

    for(int i = 0; i < AddresWeak->rowCount(); i++){

     AddresWeak->setData(AddresWeak->index(i,0),QVariant("MyWeak_ptr" + QString::number(i + 1)));
      AddresWeak->setData(AddresWeak->index(i,1),QVariant( WeakArr[i].Use_Shared_Count()));
      AddresWeak->setData(AddresWeak->index(i,2),QVariant( WeakArr[i].Use_Weak_Count()));
      AddresWeak->setData(AddresWeak->index(i,3),QString::asprintf("%08p", WeakArr[i].operator->()));
    }

    AddresWeak->setHeaderData(0,Qt::Horizontal,"Name");
    AddresWeak->setHeaderData(2,Qt::Horizontal,"CountWeakptr");
    AddresWeak->setHeaderData(1,Qt::Horizontal,"CountSharedptr");
    AddresWeak->setHeaderData(3,Qt::Horizontal,"Addres_Ptr");
}


MyDialog::MyDialog( QWidget *parent)
{
    this->setFixedSize(150,70);
    this->setWindowTitle("Edit_Shared_ptr");


    QLabel *TextIndex = new QLabel(this);
    TextIndex->setText("Номер Shared: ");
    TextIndex->setGeometry(10,10,100,15);

    IndexBox = new QSpinBox(this);
    IndexBox->setGeometry(100,10,30,20);
    IndexBox->setRange(1,10);


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

MyDialog::MyDialog(int Num, QWidget *parent)
{
    switch(Num){
    case 1:{//Assign
        this->setFixedSize(160,110);
        this->setWindowTitle("Assign_Shared_ptr");
       }
        break;
    case 2:{//Swap
        this->setFixedSize(160,110);
        this->setWindowTitle("Swap_Shared_ptr");
       }
        break;
    }

    //Second ptr
    QLabel *TextIndex2 = new QLabel(this);
    TextIndex2->setText("Номер Shared№2: ");
    TextIndex2->setGeometry(10,30,110,45);

    IndexBox2 = new QSpinBox(this);
    IndexBox2->setGeometry(120,42,30,20);
    IndexBox2->setRange(1,10);

    //First ptr
    QLabel *TextIndex = new QLabel(this);
    TextIndex->setText("Номер Shared№1: ");
    TextIndex->setGeometry(10,10,110,15);

    IndexBox = new QSpinBox(this);
    IndexBox->setGeometry(120,10,30,20);
    IndexBox->setRange(1,10);


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

void MainWindow::on_EditSharedButton_clicked()
{
    Dialog = new MyDialog();
    Dialog->show();

    if(Dialog->exec() == QDialog::Accepted){

        SharedArr[Dialog->GETIndex() - 1] = new int(77);
        MainFunc();
    }
}


void MainWindow::on_AssignButton_clicked()
{
    Dialog = new MyDialog(1);
    Dialog->show();

    if(Dialog->exec() == QDialog::Accepted){
        SharedArr[Dialog->GETIndex() - 1] = SharedArr[Dialog->GETIndex2()- 1];
        MainFunc();
    }
}


void MainWindow::on_SwapSharedButton_clicked()
{
    Dialog = new MyDialog(2);
    Dialog->show();

    if(Dialog->exec() == QDialog::Accepted){
        SharedArr[Dialog->GETIndex() - 1].swap(SharedArr[Dialog->GETIndex2()- 1]);
        MainFunc();
    }
}


void MainWindow::on_WeakToWeakButton_clicked()
{
    DialogWeak = new MyDialogWeak(1);
    DialogWeak->show();

    if(DialogWeak->exec() == QDialog::Accepted){
        WeakArr[DialogWeak->GETIndex2Weak() - 1] = WeakArr[DialogWeak->GETIndexWeak()- 1];
        MainFunc();
    }
}


void MainWindow::on_SharedToWeakButton_clicked()
{
    DialogWeak = new MyDialogWeak(2);
    DialogWeak->show();

    if(DialogWeak->exec() == QDialog::Accepted){
        WeakArr[DialogWeak->GETIndexWeak() - 1] = SharedArr[DialogWeak->GETIndex2Weak()- 1];
        MainFunc();
    }
}


void MainWindow::on_WeakToSharedButton_clicked()
{
    DialogWeak = new MyDialogWeak(3);
    DialogWeak->show();

    if(DialogWeak->exec() == QDialog::Accepted){
        SharedArr[DialogWeak->GETIndexWeak() - 1] = WeakArr[DialogWeak->GETIndex2Weak() - 1].Lock();
        MainFunc();
    }
}


MyDialogWeak::MyDialogWeak(int Num, QWidget *parent)
{
    switch(Num){
    case 1:{//Weak->Weak
        this->setFixedSize(160,110);
        this->setWindowTitle("Weak->Weak");

        QLabel *TextIndex2 = new QLabel(this);
        TextIndex2->setText("Номер Weak№2: ");
        TextIndex2->setGeometry(10,30,110,45);

        QLabel *TextIndex = new QLabel(this);
        TextIndex->setText("Номер Weak№1: ");
        TextIndex->setGeometry(10,10,110,15);
       }
        break;
    case 2:{//Shared->Weak
        this->setFixedSize(160,110);
        this->setWindowTitle("Shared->Weak");

        QLabel *TextIndex2 = new QLabel(this);
        TextIndex2->setText("Номер Shared: ");
        TextIndex2->setGeometry(10,30,110,45);

        QLabel *TextIndex = new QLabel(this);
        TextIndex->setText("Номер Weak: ");
        TextIndex->setGeometry(10,10,110,15);

       }
        break;
    case 3:{//Weak->Shared
        this->setFixedSize(160,110);
        this->setWindowTitle("Weak->Shared");

        QLabel *TextIndex2 = new QLabel(this);
        TextIndex2->setText("Номер Weak: ");
        TextIndex2->setGeometry(10,30,110,45);

        QLabel *TextIndex = new QLabel(this);
        TextIndex->setText("Номер Shared: ");
        TextIndex->setGeometry(10,10,110,15);
       }
        break;
    }

    IndexBox2Weak = new QSpinBox(this);
    IndexBox2Weak->setGeometry(120,42,30,20);
    IndexBox2Weak->setRange(1,10);


    IndexBoxWeak = new QSpinBox(this);
    IndexBoxWeak->setGeometry(120,10,30,20);
    IndexBoxWeak->setRange(1,10);


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

int MyDialogWeak::GETIndexWeak()
{
 return IndexBoxWeak->value();
}

int MyDialogWeak::GETIndex2Weak()
{
 return IndexBox2Weak->value();
}
