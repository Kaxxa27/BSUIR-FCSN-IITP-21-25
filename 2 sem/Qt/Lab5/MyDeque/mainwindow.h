#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include<mydeque.h>

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
    void on_push_front_Button_clicked();

    void on_push_back_Button_clicked();

    void on_pop_front_Button_clicked();

    void on_pop_back_Button_clicked();

    void on_clearButton_clicked();

    void on_emptyButton_clicked();

    void on_sizeButton_clicked();

    void on_viewButton_clicked();

private:
    Ui::MainWindow *ui;
    MyDeque<int> d;
};
#endif // MAINWINDOW_H
