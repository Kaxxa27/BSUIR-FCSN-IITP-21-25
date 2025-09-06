#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QGraphicsView>
#include <QGraphicsScene>
#include <QGraphicsItem>
#include <QTimer>
#include <QMessageBox>
#include <QPolygon>

#include <QWidget>
#include <QTimer>
#include <QResizeEvent>

#include "rectan.h"
#include "square.h"
#include "circle.h"
#include "romb.h"
#include "triangle.h"
#include <poly.h>
#include <star.h>
#include <line.h>
#include <mypain.h>



QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
 Q_OBJECT
public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();


public slots:
    void on_action_2_triggered();

private slots:
    void on_action_triggered();

    void on_action_3_triggered();

    void on_action_4_triggered();

    void on_action_5_triggered();

    void on_action_6_triggered();

    void on_action_7_triggered();

    void on_action_8_triggered();

    void on_action_9_triggered();


private:
    Ui::MainWindow *ui;
    QGraphicsScene *scene;
    void drawFigure();


  ;
};
#endif // MAINWINDOW_H
