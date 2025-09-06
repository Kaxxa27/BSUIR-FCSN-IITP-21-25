#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}


void Normalize(MyVector<int>& c) {
    for (int i = 0; i < c.size() - 1; ++i) {
        c[i + 1] += c[i] >> 1;
        c[i] &= 1;
    }
}

void AddZero(MyVector<int>& c, int x) {
    c.resize(x + 1);
    c[x] = 0;
}

MyVector<int> AddNum(const MyVector<int>& a, const MyVector<int>& b, int k) {

    MyVector<int> c(std::max(a.size(), b.size()) + 1);

    for (int i = 0; i < c.size(); ++i) {
        if (i < a.size()) {
            c[i] += a[i];
        }
        if (i < b.size()) {
            c[i] += b[i];
        }
    }

    Normalize(c);
    AddZero(c, k);

    return c;
}

int GetBit(MyVector<int> v, int b) {
    return v[b] & 1;
}

MyVector<int> Multiply(const MyVector<int>& a, int x, int k) {

    MyVector<int> b = a;
    b.push_back(0);

    for (int i = 0; i < b.size(); ++i) {
        b[i] *= x;
    }

    Normalize(b);
    AddZero(b, k);

    return b;
}

void GenerateSequence(MyVector<int> n, const MyVector<MyVector<int>>& p, int k, int i, MyVector<pair<int, MyVector<int>> >& v) {
    if (i == k) {
        n.resize(k + 1);
        if (GetBit(n, k)) {
            v.push_back(make_pair(k, n));
        }
        return;
    }

    if (GetBit(n, i)) {
        return;
    }

    GenerateSequence(n, p, k, i + 1, v);

    if (i) {
        GenerateSequence(AddNum(n, p[i], k + 1), p, k, i + 1, v);
    }
    return;
}



void MainWindow::on_number_editingFinished()
{
    update();
    int n = ui->number->value();
    if (n == 1){
        ui->textBrowser->setText("1");
        update();
        return;
    }
    ui->number->setValue(1);
    --n;

    MyVector<pair<int, MyVector<int>>> sequence;

    for (int k = 1; n / 2 >= sequence.size(); ++k) {
        MyVector<int> a(k + 1);
        a[k] = 1;
        MyVector<MyVector<int>> b(k + 1);
        b[0].push_back(1);
        AddZero(b[0], k + 1);
        for (int i = 0; i < k; ++i) {
            b[i + 1] = Multiply(b[i], 10, k + 1);
        }
        GenerateSequence(a, b, k, 0, sequence);
    }


    for (int i = 0; i < sequence.size(); ++i)
        std::reverse(sequence[i].second.data(), sequence[i].second.data() + sequence[i].second.size());

    std::sort(sequence.data(), sequence.data() + sequence.size());
    //    }
    --n;
    MyVector<int> w = sequence[n / 2].second;
    std::reverse(w.data(), w.data() + w.size());

    if (n % 2) {
        w[0] ^= 1;
    }
    QString answer;
    for (int k = sequence[n / 2].first; k >= 0; --k) {
        answer += QString::number(w[k] & 1);
    }

    ui->textBrowser->setText(answer);
    update();
}
