#include <iostream>
#include <string>
#include <fstream>
#include <windows.h>

using namespace std;

struct Human {
	char name[80];// фио 
	char country[80];// страна
	char name_team[80];// команда
	int nummember;// номер игрока
	int weight;// вес
	int age;// возраст
	int height;// рост
};

int checknum();
char checkanswer();
int checkchoose();

void addname(Human* arr, int i);
void addcountry(Human* arr, int i);
void addnameteam(Human* arr, int i);
void addage(Human* arr, int i);
void addnummember(Human* arr, int i);
void addweight(Human* arr, int i);
void addheight(Human* arr, int i);

void createarr(Human* arr, fstream& fs, int& k);//files
void savedata(Human* arr, int& k);

void prosmotr(Human* arr, int i);
Human* addstruct(Human* arr, int& k);
Human* deletestruct(Human* arr, int& k);
void findst(Human* arr, int& k);
void sort(Human* arr, int& k);


int main() {

	setlocale(LC_CTYPE, "rus");

	string path = "MyData.txt";
	fstream fs;
	fs.open(path, fstream::in | fstream::out | fstream::app);
	if (!fs.is_open()) {
		cout << ("Файл не удалось открыть!") << endl;
	}

	cout << endl;
	int k = 0;
	fs >> k;

	Human* arr = (Human*)malloc(k * sizeof(Human));

	createarr(arr, fs, k);//заполнение массива

	while (true) {
		int choose;
		cout << ("Программа для хранения и редактирования информации о участниках соревнования.\n\n");
		cout << ("\t\tМеню программы: \n");
		cout << ("\tПоказать список участников (1).\n");
		cout << ("\tДобавить нового участника (2).\n");
		cout << ("\tУдалить участника (3).\n");
		cout << ("\tНайти участника (4).\n");
		cout << ("\tВывод самой молодой команды (5).\n");
		cout << ("\tСохранение в файл (6).\n");
		cout << ("\tВыход из программы. (7).\n\n");

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
			findst(arr, k);
		}
		else if (choose == 5) {
			sort(arr, k);
		}
		else if (choose == 6) {
			savedata(arr, k);
		}
		else if (choose == 7) {
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
		if (n != 1 && n != 2 && n != 3 && n != 4 && n != 5 && n != 6 && n != 7) {
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

void addname(Human* arr, int i) {//фио
	
	cout << ("Введите фамилию участника: ");
	cin.getline(arr[i].name, 80);
	
	//system("Color 02");
}
void addcountry(Human* arr, int i) {
	cin.ignore(10000, '\n');
	cout << ("Введите страну: ");
	cin.getline(arr[i].country, 80);
	

}
void addnameteam(Human* arr, int i) {

	cout << ("Введите название команды: ");
	cin.getline(arr[i].name_team, 80);
	

}
void addage(Human* arr, int i) {

	printf("Введите возраст участника: ");
	arr[i].age = checknum();

}
void addnummember(Human* arr, int i) {

	printf("Введите номер участника: ");
	arr[i].nummember = checknum();

}
void addweight(Human* arr, int i) {

	printf("Введите вес участника: ");
	arr[i].weight = checknum();

}
void addheight(Human* arr, int i) {

	printf("Введите рост участника: ");
	arr[i].height = checknum();

}

void prosmotr(Human* arr, int i) {//просмотр данных

	cout << ("_______________________________") << endl;
	cout << ("Наименование страны: ") << arr[i].country << endl;
	cout << ("Название команды: ") << arr[i].name_team << endl;
	cout << ("Фамилия участника: ") << arr[i].name << endl;
	cout << ("Игровой номер: ") << arr[i].nummember << endl;
	cout << ("Возраст участника: ") << arr[i].age << endl;
	cout << ("Вес участника: ") << arr[i].weight << endl;
	cout << ("Рост участника: ") << arr[i].height << endl;
	cout << ("_______________________________") << endl << endl;
}
void createarr(Human* arr, fstream& fs, int& k) {

	for (int i = 0; i < k; i++) {
		
		fs >> arr[i].country;
		fs >> arr[i].name_team;
		fs >> arr[i].name;
		fs >> arr[i].nummember;
		fs >> arr[i].age;
		fs >> arr[i].weight;
		fs >> arr[i].height;
	}
}
void savedata(Human* arr, int& k) {
	string path2 = "NewData.txt";
	ofstream fs;
	fs.open(path2);
	fs << k;
	fs << "\n";
	fs << "*****************************";
	for (int i = 0; i < k; i++) {
		fs << "\n";
		fs << ("Наименование страны: ") << arr[i].country;
		fs << "\n";
		fs << ("Название команды: ")<< arr[i].name_team;
		fs << "\n";
		fs << ("Фамилия участника: ")<< arr[i].name;
		fs << "\n";
		fs << ("Игровой номер: ") << arr[i].nummember;
		fs << "\n";
		fs << ("Возраст участника: ") << arr[i].age;
		fs << "\n";
		fs << ("Вес участника: ") << arr[i].weight;
		fs << "\n";
		fs << ("Рост участника: ") << arr[i].height;
		fs << "\n";
		fs << "*****************************";
	}
	cout << ("Файл успешно сохранен! ") << endl << endl;
}
Human* addstruct(Human* arr, int& k) {
	int add;
	cout << ("Введите кол-во участников, которых нужно добавить: \n");
	add = checknum();
	Human* arr2 = (Human*)malloc((k + add) * sizeof(Human));
	for (int i = 0; i < k; i++) {
		arr2[i] = arr[i];
	}

	for (int i = k; i < k + add; i++) {
		cout << ("Введите данные ") << i + 1 << (" участника:") << endl;
		addcountry(arr2, i);
		addnameteam(arr2, i);
		addname(arr2, i);
		addnummember(arr2, i);
		addage(arr2, i);
		addweight(arr2, i);
		addheight(arr2, i);
		cout << endl;
	}

	k += add;
	delete[] arr;
	return arr2;
}
Human* deletestruct(Human* arr, int& k) {

	cout << ("Введите игровой номер для удаления участника из списка: ");
	int number = checknum();

	Human* arr2 = (Human*)malloc((k -1) * sizeof(Human));
	int count = 0;
	for (int i = 0; i < k; i++) {
		if (arr[i].nummember == number) {
			count++;
		}
	}
	for (int i = 0, j = 0; i < k - count; j++) {
		if (arr[j].nummember != number) {
			arr2[i] = arr[j];
			i++;

		}
	}
	k -= count;
	delete[] arr;
	return arr2;

}
void findst(Human* arr, int& k) {
	cout << ("По какому игровому номеру вы хотите искать: ");
	int NUMotdel = checknum();
	cout << endl << endl;
	bool pr = true;
	for (int i = 0; i < k; i++) {
		if (arr[i].nummember == NUMotdel) {
			prosmotr(arr, i);
			pr = false;
		}
	}
	if (pr) {
		cout << ("Сотрудника с таким отделом не существует.\nПовторите ввод!\n\n");
	}
}
void sort(Human* arr, int& k) {
	int min_age = arr[0].age;
	int count = 0;
	for (int i = 1; i < k ; i++) {
		if (arr[i].age < min_age) {
			min_age = arr[i].age;
			count = i;
		}
	}
	cout << ("Самая молодая команда (самый молодой игрок): \n");
	prosmotr(arr, count);
}