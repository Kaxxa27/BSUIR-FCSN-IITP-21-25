#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

//    ui->label->clear();
//    ui->label->setGeometry(QRect(QPoint(160,60),QSize(100,100)));

    button = new QPushButton("Click me",this);
    button->setGeometry(QRect(QPoint(150,50),QSize(100,30)));
    connect(button,SIGNAL(clicked()), this,SLOT(paint()));

//    line = new QLineEdit(this);
//    line->setGeometry(QRect(QPoint(50,50),QSize(100,30)));
//    connect(line,SIGNAL(getText()),this, SLOT(lineps));

//    timer = new QTimer(this);
//    connect(timer,SIGNAL(timeout()),this,SLOT(start_Timer()));
//    timer->start();//2.5 sec

}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::push(){

    lab = new QLabel(this);
    lab->setGeometry(QRect(QPoint(100,500),QSize(200,200)));
//    line->setText("Prosto txt");
    name = QString(line->text());
   //QMessageBox::information(this,"Title",line->text());
    ui->label->setText(name);
}
//void MainWindow::start_Timer(){
//    ui->label->setNum(count++);
//    if(count == 20000){
//        ui->label->close();
//    }
//}
void MainWindow::lineps(){

}
void MainWindow::on_pushButton_clicked()
{
    ui->label->setText("Button was clicked");
    ui->label->show();
}
void MainWindow::paint(QPaintEvent *e){
   QPainter painter(this);

   QRect rec(100,100,100,100);
   QPen pen1(Qt::blue);
   pen1.setWidth(7);

   painter.setPen(pen1);
   painter.drawRect(rec);
}
