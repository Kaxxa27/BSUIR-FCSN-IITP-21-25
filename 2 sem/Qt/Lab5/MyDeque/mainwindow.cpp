#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    setFixedSize(800,520);
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_push_front_Button_clicked()
{
    d.push_front(rand() % 77);
    ui->Text->setText(d.ToString());
}


void MainWindow::on_push_back_Button_clicked()
{
    d.push_back(rand() % 55);
    ui->Text->setText(d.ToString());
}


void MainWindow::on_pop_front_Button_clicked()
{
    d.pop_front();
    ui->Text->setText(d.ToString());
}


void MainWindow::on_pop_back_Button_clicked()
{
    d.pop_back();
    ui->Text->setText(d.ToString());
}


void MainWindow::on_clearButton_clicked()
{
    d.clear();
    ui->Text->setText(d.ToString());
}


void MainWindow::on_emptyButton_clicked()
{
    ui->Text->setText(QString::number(d.empty()));
}


void MainWindow::on_sizeButton_clicked()
{
    ui->Text->setText(QString::number(d.size()));
}


void MainWindow::on_viewButton_clicked()
{
    ui->Text->setText(d.ToString());
}

