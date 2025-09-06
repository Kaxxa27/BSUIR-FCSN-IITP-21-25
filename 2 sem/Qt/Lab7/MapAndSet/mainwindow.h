#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <map.h>
#include <set.h>

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
    void on_MapInsertButton_clicked();

    void on_MapEraseButton_clicked();

    void on_MapAtButton_clicked();

    void on_MapSizeButton_clicked();

    void on_mapPrintAll_clicked();

    void on_MapClearButton_clicked();

    void on_SetInsertButton_clicked();

    void on_SetEraseButton_2_clicked();

    void on_SetClearButton_clicked();

    void on_SetPrintAll_clicked();

    void on_SetSizeButton_clicked();

private:
    Ui::MainWindow *ui;
    My_Set<int> s;
    My_Map<int,int> m;
};
#endif // MAINWINDOW_H
