#include "date.h"

Date::Date()
{

}

Date::Date(int day, int month, int year)
    : Day(day), Month(month), Year(year)
{

}

Date Date::NextDay()
{
    int MaxDay = Day, MaxMonth = Month, MaxYear = Year;
    if(Day < FindMaxDay()){
        MaxDay++;
    }
    else{
        MaxDay = 1;
        if(Month < 12){
            MaxMonth++;
        }
        else{
            MaxMonth = 1;
            MaxYear++;
        }
    }
    Date NextDay(MaxDay,MaxMonth,MaxYear);

   return NextDay;
}

Date Date::PreviousDay()
{
    int MinDay = Day, MinMonth = Month, MinYear = Year;
    if(Day > 1){
      MinDay -= 1;
    }
    else{
        MinDay = FindMinDay();
        if(Month > 1){
            MinMonth--;
        }
        else{
            MinMonth = 12;
            MinYear--;
        }
    }
   Date PreviousDay(MinDay,MinMonth,MinYear);

   return PreviousDay;
}

int Date::DaysTillYourBithday(Date BithdayDate)
{
    if(BithdayDate.Day == this->Day && BithdayDate.Month == this->Month && BithdayDate.Year == this->Year){
        return 0;
    }
    else{
        return abs(DurationBithday(*this,BithdayDate));
    }
}

int Date::Duration(Date  Current_Date)
{

    int Duration = 0;
    int LYearDays = 0;
    int CountYear = 0;
    int CountDay = abs(Current_Date.Day - this->Day);
    int MonthDays = 0;

    if(Current_Date.Year - this->Year == 0 && Current_Date.Month - this->Month == 0){
        return abs(CountDay);
    }
    if(Current_Date.Month > this->Month){
    CountYear = abs(Current_Date.Year - this->Year) - 1;
    }
    else{
        CountYear = abs(Current_Date.Year - this->Year);
    }

    if(IsLeap(Current_Date.Year) &&  IsLeap(this->Year)){
        LYearDays = (CountYear) / 4 + 1;
    }
    else if((!IsLeap(Current_Date.Year) &&  IsLeap(this->Year)) ||(IsLeap(Current_Date.Year) &&  !IsLeap(this->Year))){
        LYearDays = (CountYear) / 4 + 1;
    }
    else if(!IsLeap(Current_Date.Year) &&  !IsLeap(this->Year)){
        LYearDays = (CountYear) / 4;
    }

    if(Current_Date.Month < this->Month){
        if(Current_Date.Day < this->Day){
            MonthDays = Current_Date.Day + FindMaxDay(this->Month) - this->Day;

            for(int i = Current_Date.Month; i < this->Month; i++){
                MonthDays += FindMaxDay(i);
            }

            Duration = ((CountYear)*365 + LYearDays) + MonthDays;
        }
        else{
            for(int i = Current_Date.Month; i < this->Month; i++){
                MonthDays += FindMaxDay(i);
            }
            Duration = ((CountYear)*365 + LYearDays) + MonthDays + CountDay;
        }
    }
    else{
        if(Current_Date.Day < this->Day){
            MonthDays = Current_Date.Day + FindMaxDay(this->Month) - this->Day;
            for(int i = Current_Date.Month; i < this->Month; i++){
                MonthDays += FindMaxDay(i);
            }
            Duration = ((CountYear)*365 + LYearDays) + MonthDays;
        }
        else{
            for(int i = Current_Date.Month; i > this->Month; i--){
                MonthDays += FindMaxDay(i);
            }
            Duration = ((CountYear)*365 + LYearDays) + MonthDays + CountDay;
        }
    }

    return abs(Duration);
}

int Date::DurationBithday(Date  Current_Date, Date Bithday)
{

    int Duration = 0;
    int LYearDays = 0;
    int CountYear = 0;
    int CountDay = abs(Current_Date.Day - Bithday.Day);
    int MonthDays = 0;
    if(Current_Date.Year - Bithday.Year == 0 && Current_Date.Month - Bithday.Month == 0){
        if(Current_Date.Day < Bithday.Day){
            return CountDay;
        }
        else{
          return 365 - CountDay;
        }
    }
    if(Current_Date.Month > Bithday.Month){
    CountYear = abs(Current_Date.Year - Bithday.Year) - 1;
    }
    else{
        CountYear = abs(Current_Date.Year - Bithday.Year);
    }

    if(IsLeap(Current_Date.Year) &&  IsLeap(Bithday.Year)){
        LYearDays = (CountYear) / 4 + 1;
    }
    else if((!IsLeap(Current_Date.Year) &&  IsLeap(Bithday.Year)) ||(IsLeap(Current_Date.Year) &&  !IsLeap(Bithday.Year))){
        LYearDays = (CountYear) / 4 + 1;
    }
    else if(!IsLeap(Current_Date.Year) &&  !IsLeap(Bithday.Year)){
        LYearDays = (CountYear) / 4;
    }

    if(Current_Date.Month < Bithday.Month){
        if(Current_Date.Day < Bithday.Day){
            MonthDays = Current_Date.Day + FindMaxDay(Bithday.Month) - Bithday.Day;
           for(int i = Current_Date.Month; i < Bithday.Month; i++){
                  MonthDays += FindMaxDay(i);
            }
            Duration = ((CountYear)*365 + LYearDays) + MonthDays;
        }
        else{
            for(int i = Current_Date.Month; i < Bithday.Month; i++){
                MonthDays += FindMaxDay(i);

            }
            Duration = ((CountYear)*365 + LYearDays) + MonthDays + CountDay;
        }
    }
    else{
        if(Current_Date.Day < Bithday.Day){
            MonthDays = Current_Date.Day + FindMaxDay(Bithday.Month) - Bithday.Day;
            for(int i = Current_Date.Month; i < Bithday.Month; i++){
                   MonthDays += FindMaxDay(i);
             }

            Duration = ((CountYear)*365 + LYearDays) + MonthDays;
        }
        else{
            for(int i = Current_Date.Month; i > Bithday.Month; i--){
                MonthDays += FindMaxDay(i);
            }
            Duration = ((CountYear)*365 + LYearDays) + MonthDays + CountDay;
        }
    }

    return abs(Duration);
}

bool Date::IsLeap(int Year)//високосный год
{
    return ((Year % 4 == 0 && Year % 100 != 0) || Year % 400 == 0);
}

short Date::WeekNumber()
{
    int Days = 0;

    for(int i = 1; i < this->Month; i++){
        Days += FindMaxDay(i);
    }

    if (this->IsLeap(Year) && this->Month > 2){
        Days++;
    }

    Days += this->Day;
    int week = (Days / 7) + 1;

    return week;
}

int Date::FindMaxDay()
{
    MaxDay = 0;
    switch(Month){
     case 2 :
        return MaxDay = 28 + IsLeap(Year);
     case 4:
     case 6:
     case 9:
     case 11:
        return MaxDay = 30;
     default:
        return MaxDay = 31;
    }
}
int Date::FindMaxDay(int i)
{
    MaxDay = 0;
    switch(i){
     case 2 :
        return MaxDay = 28 + IsLeap(Year);
     case 4:
     case 6:
     case 9:
     case 11:
        return MaxDay = 30;
     default:
        return MaxDay = 31;
    }
}

int Date::FindMinDay()
{
    int MinDay = 0;
    switch(Month){
    case 3:
        return MinDay = 28 + IsLeap(Year);
     case 1:
     case 2:
     case 4:
     case 6:
     case 8:
     case 9:
     case 10:
     case 11:
        return MinDay = 31;
     default:
        return MinDay = 30;
    }
}

QString Date::GetDate_str()
{
    QString Date_InStrForFile;

    if(this->Day<10){
        Date_InStrForFile = "0" + QString::number(this->Day) + ".";
    }
    else{
        Date_InStrForFile = QString::number(this->Day) + ".";
    }

    if(this->Month < 10){
        Date_InStrForFile += "0" + QString::number(this->Month) + ".";
    }
    else{
        Date_InStrForFile += QString::number(this->Month) + ".";
    }

    Date_InStrForFile += QString::number(this->Year);
    return Date_InStrForFile;
}
