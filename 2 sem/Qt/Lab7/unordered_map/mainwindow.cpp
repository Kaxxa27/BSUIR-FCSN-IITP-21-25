#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    model = new QStandardItemModel();

    for (int i = 0; i < 10 ; ++i )
    {
        mp.insert(pair<int,int>(rand(),i));
    }
    FillInfo();
}
MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::FillInfo()
{
    delete model;

    int arr_size = mp.arr_size();

    std::vector<std::list<pair<int,int>>> tmp = mp.get_Arr();

    model = new QStandardItemModel(0, arr_size, this);

    for (int i = 0; i < arr_size; i++)
        model->setHeaderData(i, Qt::Horizontal, QString::number(i));

    ui->tableView->setModel(model);

    for (int i = 0; i < arr_size; i++)
    {
        int row = 0;
        for (const auto& it : tmp[i])
        {
            if (row > model->rowCount() - 1)
                model->insertRow(row);

            QModelIndex index = model->index(row++, i);

            QString text =  QString::number(it.first);

            model->setData(index, text);

            model->item(index.row(), index.column())->setEditable(false);

        }

    ui->tableView->horizontalHeader()->setSectionResizeMode(QHeaderView::ResizeToContents);
    ui->amountLabel->setText(QString::number(mp.get_count_elem()));
    }
}

void MainWindow::on_addButton_clicked()
{
    mp.insert(pair<int,int>(ui->addSpinBox->value(),77));
    FillInfo();
}


void MainWindow::on_deleteButton_clicked()
{
    mp.erase(ui->deleteSpinBox->value());
    FillInfo();
}


void MainWindow::on_clearButton_clicked()
{
    mp.clear();
    FillInfo();
}


void MainWindow::on_fillButton_clicked()
{
    mp.clear();
    int size = ui->fillSpinBox->value();
    for (int i = 0; i < size ; ++i )
    {
        mp.insert(pair<int,int>(rand(),i));
    }
    FillInfo();
}


void MainWindow::on_findButton_clicked()
{
    if(mp.contains(ui->findSpinBox->value()))
    {
       ui->findLabel->setText("True");
    }
    else ui->findLabel->setText("False");
    FillInfo();
}


void MainWindow::on_pushButton_clicked()
{
    ui->max_load_factor_label->setText(QString::number(mp.max_load_factor()));
}

