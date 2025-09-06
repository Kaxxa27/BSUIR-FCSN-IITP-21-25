#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <bitset.h>


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
    void on_ResetPosButton_clicked();

    void on_TestButton_clicked();

    void on_SetPosButton_clicked();

    void on_FlipButton_clicked();

    void on_FlipPosButton_clicked();

    void on_CountButton_clicked();

    void on_SizeButton_clicked();

    void on_AllButton_clicked();

    void on_NodeButton_clicked();

    void on_AnyButton_clicked();

private:
    Ui::MainWindow *ui;

    bitset<8> bit_set;

    void FillInfo();


    void FillDopInfo(bool tmp);
    void FillDopInfo(size_t tmp);
};
#endif // MAINWINDOW_H
