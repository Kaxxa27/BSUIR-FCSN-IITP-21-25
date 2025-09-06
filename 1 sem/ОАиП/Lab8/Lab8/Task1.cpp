#include <iostream>
#include <string>
#include "stdio.h"

using namespace std;

struct Human {
	char name[80];// фио 
	char position[80];//должность
	int numotdel;// номер отдела
	int workexp;//стаж работы
	int age;//возраст
};

int checknum();
char checkanswer();
int checkchoose();

void addname(Human* arr, int i);
void addposition(Human* arr, int i);
void addnumotdel(Human* arr, int i);
void addage(Human* arr, int i);
void addworkexp(Human* arr, int i);

void prosmotr(Human* arr, int i);
Human* addstruct(Human* arr, int& k);
Human* deletestruct(Human* arr, int& k);
void findst(Human* arr, int& k);
void sort(Human* arr, Human* arr3, int& k);


int main() {

	setlocale(LC_CTYPE, "rus");

	int num = 0;
	int kol = 0;
	printf("Введите кол-во структур: ");
	num = checknum();
	cout << endl;
	int k = num;

	Human* arr = new Human[num];
	Human* arr3 = new Human[num];

	for (int i = 0; i < num; i++) {
		cout << ("Введите данные ") << i + 1 << (" сотрудника:") << endl;
		addname(arr, i);
		addposition(arr, i);
		addnumotdel(arr, i);
		addage(arr, i);
		addworkexp(arr, i);
		cout << endl;
	}

	while (true) {
		int choose;
		cout << ("Программа для хранения и редактирования информации о сотрдникам предприятия города N.\n\n");
		cout << ("\t\tМеню программы: \n");
		cout << ("\tПоказать список сотрудников (1).\n");
		cout << ("\tДобавить нового сотрудника (2).\n");
		cout << ("\tУдалить сотрудника (3).\n");
		cout << ("\tНайти сотрудника (4).\n");
		cout << ("\tСортировка от 20 лет стажа (5).\n");
		cout << ("\tВыход из программы. (6).\n\n");

		cout << ("Ваш ответ: ");
		choose = checkchoose();
		cout << endl;

		if (choose == 1) {
			if (k == 0) {
				cout << ("Список пуст!") << endl << endl;
			}
			else {
				for (int i = 0; i < k; i++) {
					prosmotr(arr, i);
				}
			}
		}
		else if (choose == 2) {
			arr = addstruct(arr, k);
		}
		else if (choose == 3) {
			if (k == 0) {
				cout << ("Список пуст! Вы не можете удалить элементы.\n\n");
			}
			else {
				arr = deletestruct(arr, k);
			}
		}
		else if (choose == 4) {
			findst(arr,k);
		}
		else if (choose == 5) {
			sort(arr, arr3, k);
		}
		else if (choose == 6) {
			cout << ("Программа завершена! Хорошего дня.\n");
			break;
		}
	}
	return 0; 
}

int checknum() {
	
	int n;
	while (true) {
		cin >> n;
		if (cin.fail() || cin.peek() != '\n') {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}

	return n;
}
char checkanswer() {
	char n;
	while (true) {
		cin >> n;
		if (n != 'y' && n != 'n') {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}
	return n;
}
int checkchoose() {
	int  n;
	while (true) {
		cin >> n;
		if (n != 1 && n != 2 && n != 3 && n != 4 && n != 5 && n != 6) {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}
	return n;
}

void addname(Human *arr, int i) {
	cin.ignore(10000, '\n');
	cout<<("Введите ФИО сотрудника: ");
	cin.getline(arr[i].name, 80);
	//system("Color 02");
}
void addposition(Human* arr, int i) {
	
	cout << ("Введите должность сотрудника: ");
	cin.getline(arr[i].position, 80);
	
}
void addnumotdel(Human* arr, int i) {
	
	cout<<("Введите номер отдела: ");
	arr[i].numotdel = checknum();
	
}
void addage(Human* arr, int i) {
	
	printf("Введите возраст сотрудника: ");
	arr[i].age = checknum();
	
}
void addworkexp(Human* arr, int i) {
	
	printf("Введите стаж работы: ");
	arr[i].workexp = checknum();

}

void prosmotr(Human* arr, int i) {//просмотр данных

	cout << ("_______________________________") << endl;
	cout << ("ФИО сотрудника: ") << arr[i].name << endl;
	cout << ("Должность: ") << arr[i].position << endl;
	cout << ("Возраст: ") << arr[i].age << endl;
	cout << ("Номер отдела: ") << arr[i].numotdel << endl;
	cout << ("Стаж работы: ") << arr[i].workexp << endl;
	cout << ("_______________________________") << endl << endl;
}
Human* addstruct(Human* arr, int& k) {
	int add;
	cout <<( "Введите кол-во сотрудников, которых нужно добавить: \n");
	add = checknum();
	Human* arr2 = new Human[k + add];
	for (int i = 0; i < k; i++) {
		arr2[i] = arr[i];
	}

	for (int i = k; i < k+add; i++) {
		cout << ("Введите данные ") << i + 1 << (" сотрудника:") << endl;
		addname(arr2, i);
		addposition(arr2, i);
		addnumotdel(arr2, i);
		addage(arr2, i);
		addworkexp(arr2, i);
		cout << endl;
	}

	k += add;
	delete[] arr;
	return arr2;
}
Human* deletestruct(Human* arr, int& k) {

	cout << ("Введите номер отдела для удаления сотрудников из списка: ");
	int number = checknum();

	Human* arr2 = new Human[k - 1];
	int count = 0;
	for (int i = 0; i < k; i++) {
		if (arr[i].numotdel == number) {
			count++;
		}
	}
	for (int i = 0, j = 0; i < k - count; j++) {
		if (arr[j].numotdel != number) {
			arr2[i] = arr[j];
			i++;

		}
	}
	k -= count;
	delete[] arr;
	return arr2;

}
void findst(Human* arr,int& k) {
	cout << ("По какому номеру отдела вы хотите искать: ");
	int NUMotdel = checknum();
	cout << endl << endl;
	bool pr = true;
	for (int i = 0; i < k; i++) {
		if (arr[i].numotdel == NUMotdel) {
			prosmotr(arr, i);
			pr = false;
		}
	}
	if (pr) {
		cout << ("Сотрудника с таким отделом не существует.\nПовторите ввод!\n\n");
	}
}
void sort(Human* arr, Human* arr3, int& k) {
	int count = 0;
	for (int i = 0; i < k; i++) {
		if (arr[i].workexp <= 20) {
			count++;
		}
		else {
			arr3[i - count] = arr[i];
		}
	}
	if (count) {
		cout << ("Нет в списке сотрудников у которых стаж больше 20 лет.") << endl;
	}
	else {
		k -= count;
		bool sort = true;
		int right = k - 1;
		int left = 1;
		do {
			bool sort = true;
			for (int i = left; i <= right; i++) {
				if (arr3[i - 1].workexp > arr3[i].workexp) {
					swap(arr3[i - 1], arr3[i]);
					sort = false;
				}
			}
			right--;
			for (int i = right; i >= left; i--) {
				if (arr3[i].workexp < arr3[i - 1].workexp) {
					swap(arr3[i], arr3[i - 1]);
					sort = false;
				}
			}
			left++;
		} while (sort == false);
		for (int i = 0; i < k; i++) {
			prosmotr(arr3, i);
		}
	}
}

//void addnewstruct(Human* arr, int& k) {
//		char sv[80];
//		for (int i = 0; i < size(arr[0].name); i++) {
//			sv[i] = arr[0].name[i];
//		}
//
//		k++;
//		arr = (Human*)realloc(arr,k);
//		cout << ("Введите данные нового сотрудника: ") << endl;
//		addname(arr, k-1);
//		addposition(arr, k-1);
//		addnumotdel(arr, k-1);
//		addage(arr, k-1);
//		addworkexp(arr, k-1);
//		cout << endl;
//
//		for (int i = 0; i < size(sv); i++) {
//			arr[0].name[i] = sv[i];
//		}
//}
//void deletestruct(Human* arr, int& k, int& kol) {
//			kol++;
//			Human arr2[20];
//			cout << ("С каким отделом удалить структуры?");
//			
//			int NUMotdel = checknum();
//			int count = 0;
//			for (int i = 0; i < k; i++) {
//				if (NUMotdel == arr[i].numotdel) {
//					count++;
//				}
//				else {
//					arr2[i-count] = arr[i];
//				}
//			}
//			k = k - count;
//			arr = (Human*)realloc(arr, k);
//			int p = k + kol;
//			for (int i = 0; i < k ; i++) {
//				arr[i] = arr2[i];
//			}
//	}
//

