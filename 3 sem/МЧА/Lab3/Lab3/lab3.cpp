
#include <iostream>
#include <vector>
#include <exception>
#include <iomanip>
#include <map>
#include "Polynomial.h"
#include "Result.h"
using namespace std;


// x^3 + ax^2 + bx + c = 0

int main() {
    
    setlocale(LC_ALL, "Rus");
    cout.setf(ios_base::fixed);

    double LSegment = -10;
    double RSegment = 10;


    double a = -6.4951;
    double b = -31.2543;
    double c = 23.1782;

    Polynomial polynomial(a, b, c);


    cout << "Считать корни с точностью eps = ";
    double eps; cin >> eps;

    cout << "Выводить знаков после запятой в корнях: ";
    int signs = 0; cin >> signs;

    cout << "Отделять корни табличным методом с шагом: ";
    double step = 0; cin >> step;

    cout << "Отрезок: [" << setprecision(signs) << LSegment << ", " 
         << setprecision(signs) << RSegment << "]\n";

    int rootsNumber = polynomial.RootsNumber(Segment(LSegment, RSegment));
    cout << "Корней: " << rootsNumber << "\n\n";

    vector<Segment> segmentsWithRoots = polynomial.SegmentsWithRoots(Segment(LSegment, RSegment), step);

    if (rootsNumber != segmentsWithRoots.size()) {
        cout << "Обнаружен корень четной кратности. \n";
        cout << "Необходимо отделить корни вручную. \n\n";

        segmentsWithRoots.clear();
        segmentsWithRoots.resize(rootsNumber);

        for (int i = 0; i < rootsNumber; i++) {
            cout << "Отрезок с корнем " << i + 1 << ": \n";
            cin >> segmentsWithRoots[i].a >> segmentsWithRoots[i].b;
        }
    }
    else {
        cout << "Отрезки: \n";
        for (const auto segment : segmentsWithRoots)
            cout << "[" << setprecision(signs) << segment.a << ", " << segment.b << "]\n";
    }

    map<int, string> methodsNames({ make_pair(0, "Метод половинного деления: "),
                                    make_pair(1, "Метод хорд: "),
                                    make_pair(2, "Метод Ньютона: ") });

    map<string, vector<Result>> results;
    for (int i = 0; i < rootsNumber; i++) {
        results["Метод половинного деления: "].push_back(polynomial.HalfDivision(segmentsWithRoots[i], eps));
        results["Метод хорд: "].push_back(polynomial.ChordMethod(segmentsWithRoots[i], eps));
        results["Метод Ньютона: "].push_back(polynomial.NewtonMethod(segmentsWithRoots[i], eps));
    }

    for (int i = 0; i < rootsNumber; i++) {

        cout << "\nОтрезок [" << setprecision(signs) << segmentsWithRoots[i].a
            << ", " << setprecision(signs) << segmentsWithRoots[i].b << "]: \n";

        
        for (int j = 0; j < 3; j++) {
            cout << setw(27) << right << methodsNames[j];

            if (results[methodsNames[j]][i].iterations == -1)
                cout << "решение данным методом на данном отрезке невозможно \n"; 
            else
                cout << setw(signs + 3) << right << setprecision(signs) << results[methodsNames[j]][i].root 
                     << ". Итераций: " << results[methodsNames[j]][i].iterations << "\n";
        }
    }
}
