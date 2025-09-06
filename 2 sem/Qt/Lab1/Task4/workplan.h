#ifndef WORKPLAN_H
#define WORKPLAN_H

#include <QString>
#include <QDate>
#include <QVector>
#include <QRegularExpression>
#include <QRegularExpressionMatch>
#include <QRegularExpressionMatchIterator>
#include <qregularexpression.h>
#include <iostream>
#include <regex>

class WorkPlan
{

    struct Worker
    {
        QString FIO;
        QString ProjectName;
        QString ProjectTask;

        QString D;
        QString S;
        QString E;

        QDate Deadline;
        QDate StartTime;
        QDate EndTime;
    };

    QVector<Worker> Workers;

public:
    WorkPlan();

    void AddToStruct(QString FIO, QString ProjectName, QString ProjectTask ,QString D,
                     QString S,QString E, QDate Deadline, QDate StartTime, QDate EndTime);
    QString ShowInfo();
    QString AddToComboBoxWorkers(int i);
    int WorkerSize();
    void ClearMyVector();
    int SizeOfMyVector();
    void DeleteWorker(int num);
    QString AddNewInfoToFile(int i);

    bool CorrectDate(QString Date);
    bool IsLeap(int Year);

    QString SortEndDate();  
    QString ListSumTime();
};

#endif // WORKPLAN_H
