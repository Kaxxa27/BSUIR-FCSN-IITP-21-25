#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>

#include <QWidget>
#include <QtCore>
#include <QtGui>
#include <QGraphicsView>
#include <QGraphicsScene>
#include <QGraphicsItem>
#include <QLabel>
#include <QGraphicsRectItem>
#include <QGraphicsEllipseItem>
#include <QGraphicsLineItem>
#include <QGraphicsPixmapItem>
#include <QTimer>
#include <QPushButton>
#include <QLineEdit>
#include <QMessageBox>


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
    void push();
    void lineps();
    void on_pushButton_clicked();
//    void start_Timer();

protected slots:
    void paint(QPaintEvent *e);

private:
    int count = 0;
    Ui::MainWindow *ui;
    QPushButton *button;
    QLineEdit *line;
    QLabel *lab;
    QString name;
    QTimer *timer;
    QLayout *horizon;
    QPaintEvent *e;


  QGraphicsScene* scene;
};
#endif // MAINWINDOW_H
