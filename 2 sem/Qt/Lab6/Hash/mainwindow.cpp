#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    setFixedSize(size());

    Hash_Table = new HashTable(10);

    model = new QStandardItemModel();

}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_addButton_clicked()
{
    Hash_Table->push(ui->addSpinBox->value());
    FillTableInfo();
}


void MainWindow::on_deleteButton_clicked()
{
    Hash_Table->pop(ui->deleteSpinBox->value());
    FillTableInfo();
}


void MainWindow::on_clearButton_clicked()
{
    Hash_Table->clear();
    FillTableInfo();
}


void MainWindow::on_findButton_clicked()
{

    if(Hash_Table->contains(ui->findSpinBox->value()))
    {
       ui->findLabel->setText("True");
    }
    else ui->findLabel->setText("False");
    FillTableInfo();
}


void MainWindow::on_fillButton_clicked()
{
    int count = ui->fillSpinBox->value();

    Hash_Table->clear();

    while(Hash_Table->get_size() != count)
    {
        Hash_Table->push(rand());
    }

    FillTableInfo();
}


void MainWindow::on_maxButton_clicked()
{
    ui->maxLine->setText(QString::number(Hash_Table->get_max()));
    FillTableInfo();
}

void MainWindow::FillTableInfo()
{
    delete model;

    int arr_size = Hash_Table->get__arrsize();
    My_Stack<int>* arr = Hash_Table->get_arr();

    My_Stack<int>* tmp = new My_Stack<int>[arr_size];
    for (int i = 0; i < arr_size; i++)
        tmp[i] = arr[i];

    model = new QStandardItemModel(0, arr_size, this);

    for (int i = 0; i < arr_size; i++)
        model->setHeaderData(i, Qt::Horizontal, QString::number(i));

    ui->tableView->setModel(model);

    for (int i = 0; i < arr_size; i++) {
        int row = 0;

        while (!tmp[i].empty()) {
            if (row > model->rowCount() - 1)
                model->insertRow(row);

            QModelIndex index = model->index(row++, i);

            model->setData(index, QString::number(tmp[i].top()));
            model->item(index.row(), index.column())->setEditable(false);
            tmp[i].pop();
        }
    }

    ui->tableView->horizontalHeader()->setSectionResizeMode(QHeaderView::ResizeToContents);
    ui->amountLabel->setText(QString::number(Hash_Table->get_size()));
    delete[] tmp;
}

