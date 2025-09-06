#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    setFixedSize(758,393);

   FillInfo();

   ui->StrOfBits->setDisabled(true);
   ui->StrOfNumber->setDisabled(true);
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_ResetPosButton_clicked()
{
    size_t pos = ui->ResetPosSpinBox->value();
    bit_set.reset(pos);
    FillInfo();
}


void MainWindow::on_TestButton_clicked()
{
    size_t pos = ui->TestPosSpinBox->value();

    FillDopInfo(bit_set.test(pos));
}


void MainWindow::on_SetPosButton_clicked()
{
    size_t pos = ui->SetPosSpinBox->value();
    bit_set.set(pos);
    FillInfo();
}


void MainWindow::on_FlipButton_clicked()
{
    bit_set.flip();
    FillInfo();
}


void MainWindow::on_FlipPosButton_clicked()
{
    size_t pos = ui->FlipPosSpinBox->value();
    bit_set.flip(pos);
    FillInfo();
}


void MainWindow::on_CountButton_clicked()
{
    FillDopInfo(bit_set.count());
}


void MainWindow::on_SizeButton_clicked()
{
    FillDopInfo(bit_set.size());
}


void MainWindow::on_AllButton_clicked()
{
     FillDopInfo(bit_set.all());
}


void MainWindow::on_NodeButton_clicked()
{
    FillDopInfo(bit_set.none());
}


void MainWindow::on_AnyButton_clicked()
{
    FillDopInfo(bit_set.any());
}

void MainWindow::FillInfo()
{
    ui->StrOfBits->clear();
    ui->StrOfNumber->clear();

    ui->StrOfBits->setText(bit_set.to_string());
    ui->StrOfNumber->setText(QString::number(bit_set.to_ulong()));
}


void MainWindow::FillDopInfo(bool tmp)
{
    ui->DopInfoTable->clear();

    if(tmp){ ui->DopInfoTable->setText("True");}
    else   { ui->DopInfoTable->setText("False");}

}

void MainWindow::FillDopInfo(size_t tmp)
{
    ui->DopInfoTable->clear();

    ui->DopInfoTable->setText(QString::number(tmp));
}

