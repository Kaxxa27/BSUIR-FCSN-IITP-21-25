#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include<unoredered_map.h>
#include<QStandardItemModel>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_addButton_clicked();

    void on_deleteButton_clicked();

    void on_clearButton_clicked();

    void on_fillButton_clicked();

    void on_findButton_clicked();

    void on_pushButton_clicked();

private:
    Ui::MainWindow *ui;

    unoredred_map<int,int> mp;

    QStandardItemModel* model;

    void FillInfo();
};
#endif // MAINWINDOW_H
