#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QPushButton>

#include <QGraphicsView>
#include <QGraphicsScene>
#include <QGraphicsItem>
#include <QTimer>

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
    void rec_go();
    void on_button_shassi_clicked();
    void SH();
    void on_pushButton_clicked();

private:
    Ui::MainWindow *ui;
    QGraphicsScene *scene;
    QTimer *mTimer;
    QTimer *shTimer;
    QGraphicsRectItem *rec;
    QGraphicsEllipseItem *shassi2;
    QGraphicsEllipseItem *shassi3;

    int i;

    QGraphicsLineItem *line1;
    QGraphicsLineItem *line2;
    QGraphicsLineItem *line3;
    QGraphicsLineItem *line4;
    QGraphicsLineItem *line5;
    QGraphicsLineItem *line6;

    QGraphicsRectItem *rect1;
    QGraphicsRectItem *rect2;
    QGraphicsRectItem *rect3;
    QGraphicsRectItem *rect4;

    QGraphicsItemGroup *lineGroup;
    QGraphicsItem* itemElemetLine;
};
#endif // MAINWINDOW_H
