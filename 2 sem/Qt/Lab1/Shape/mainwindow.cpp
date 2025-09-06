#include "mainwindow.h"
#include "ui_mainwindow.h"


MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    scene = new QGraphicsScene(this);
    scene->setSceneRect(0,0,700,500);
    ui->graphicsView->setScene(scene);

}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::drawFigure() {

    QWidget* drawingWindow = new QWidget();
    drawingWindow->setWindowFlags(Qt::Tool);
    drawingWindow->setWindowTitle("Рисование");
    drawingWindow->setFixedSize(800, 600);

    MyPain* paint = new MyPain(drawingWindow);
    paint->setFixedSize(800, 600);
    drawingWindow->show();
    drawingWindow->activateWindow();
}


void MainWindow::on_action_2_triggered()
{
    scene->clear();

    scene->addItem(new class Rectan());

}


void MainWindow::on_action_triggered()
{
    scene->clear();

    scene->addItem(new class Square());
}


void MainWindow::on_action_3_triggered()
{
    scene->clear();
    scene->addItem(new class Circle());
}


void MainWindow::on_action_4_triggered()
{
    scene->clear();
    scene->addItem(new class Romb());
}


void MainWindow::on_action_5_triggered()
{
    scene->clear();
    scene->addItem(new class Triangle());
}


void MainWindow::on_action_6_triggered()
{
    scene->clear();
    scene->addItem(new class Poly());
}


void MainWindow::on_action_7_triggered()
{
    scene->clear();
    scene->addItem(new class Star());
}


void MainWindow::on_action_8_triggered()
{
    scene->clear();
    scene->addItem(new class Line());
}


void MainWindow::on_action_9_triggered()
{
    drawFigure();
}

