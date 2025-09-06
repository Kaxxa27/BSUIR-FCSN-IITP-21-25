#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <list.h>
#include <QFileDialog>

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
    void on_FileWay_clicked();

    void on_ShowButton_clicked();

    void on_AddButton_clicked();

    void on_DeleteButton_clicked();

    void on_SaveWeightSortButton_clicked();

    void on_HeightSortButton_clicked();

    void on_AgeSortButton_clicked();

private:
    Ui::MainWindow *ui;

    List Competitors;

    void File(QFile &MyFile);
    void SetInfoTable();
    QString MyFile;
};
#endif // MAINWINDOW_H
