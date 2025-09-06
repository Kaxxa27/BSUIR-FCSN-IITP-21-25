#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_MapInsertButton_clicked()
{
    int key = ui->MapKeySpin->value();
    int value = ui->MapValueSpin->value();
    m[key] = value;
}


void MainWindow::on_MapEraseButton_clicked()
{
    int key = ui->MapEraseKeySpin->value();
    m.erase(key);
}


void MainWindow::on_MapAtButton_clicked()
{
    ui->MapInfoTable->clear();
    int key = ui->MapAtKeySpin->value();
    int value = m[key];
    ui->MapInfoTable->append("VALUE: " + QString::number(value));
}


void MainWindow::on_MapSizeButton_clicked()
{
    ui->MapInfoTable->clear();
    ui->MapInfoTable->append("SIZE: " + QString::number(m.size()));
}


void MainWindow::on_mapPrintAll_clicked()
{
    ui->MapInfoTable->clear();
    QString ans;
    for (auto p : m){
        ans += "mp[" + QString::number(p.first) + "] = " + QString::number(p.second) + "\n";
    }
    ui->MapInfoTable->append(ans.isEmpty() ? "Empty" : ans);
}


void MainWindow::on_MapClearButton_clicked()
{
    m.clear();
}


void MainWindow::on_SetInsertButton_clicked()
{
     s.insert(ui->SetKeySpin->value());
}


void MainWindow::on_SetEraseButton_2_clicked()
{
    s.erase(ui->SetEraseKeySpin->value());
}


void MainWindow::on_SetClearButton_clicked()
{
    s.clear();
}


void MainWindow::on_SetPrintAll_clicked()
{
    ui->SetInfoTable->clear();
    QString ans;
    for (auto& val : s){
        ans += QString::number(val) + " ";
    }
    ui->SetInfoTable->append(ans.isEmpty() ? "Empty" : ans);
}


void MainWindow::on_SetSizeButton_clicked()
{
    ui->SetInfoTable->clear();
    int size = s.size();
    ui->SetInfoTable->append("SIZE: " + QString::number(size));
}

