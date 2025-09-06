#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);


    for (int i = 0; i < 10 ; ++i ) {
        q.push(rand() % 32);
    }

   Present();
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::Present()
{
    ui->first_queue->setText(q.to_String());
    ui->second_queue->setText(q.to_String());
}


void MainWindow::on_PushButton_clicked()
{
    q.push(rand() % 66);
    Present();
}


void MainWindow::on_ClearButton_clicked()
{
    q.clear();
    Present();
}


void MainWindow::on_EmptyButton_clicked()
{
    if(q.empty()) ui->first_queue->setText("Queue is empty :D");
    else ui->first_queue->setText("Queue not is empty :D");
}


void MainWindow::on_PopButton_clicked()
{
    q.pop();
    Present();
}


void MainWindow::on_SizeButton_clicked()
{
   if(q.empty()) {
       ui->first_queue->setText("Size: 0");
   }
   else   ui->first_queue->setText("Size: " + QString::number(q.Size() - 1));
}


void MainWindow::on_Search_Button_clicked()
{
    q.search_min();
    Present();
    ui->Search_Button->setDisabled(true);
}


void MainWindow::on_ViewButton_clicked()
{
    Present();
}

