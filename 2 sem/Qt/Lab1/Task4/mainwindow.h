#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QComboBox>
#include <QFile>
#include <QMessageBox>
#include <QRegularExpression>
#include <workplan.h>
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
    void on_pushButton_clicked();

    void on_AddWorkers_clicked();

    void on_DeleteButton_clicked();

    void on_SortButton_clicked();

    void on_SumTime_clicked();

    void on_SaveFileWay_clicked();

private:
    Ui::MainWindow *ui;
    WorkPlan Worker;
    void File(QFile &MyFile);
    void AddToComboBox();
    int CountStartDeleteNum();
    int DeleteNUM = 0;
    int PosOfTextInFile();
    int CountSeek;

    void FileWay(QFile &MyFile);

    void MainFunc();


};
#endif // MAINWINDOW_H
