#include "pch.h"

TEST(TestCaseName, Test) {

	bool k = true;
	bool p = true;
	bool q = true;

	char** str = new char* [3];
	char** str2 = new char* [3];

	for (int i = 0; i < 3; i++) {
		str[i] = new char[100];
	}
	for (int i = 0; i < 3; i++) {
		str2[i] = new char[100];
	}

	str[0][0] = 'l';
	str[0][1] = 'o';
	str[0][2] = 'l';
	str[0][3] = '\0';

	str[1][0] = 'b';
	str[1][1] = 'a';
	str[1][2] = 'a';
	str[1][3] = '\0';

	str[2][0] = 'p';
	str[2][1] = 'o';
	str[2][2] = 'p';
	str[2][3] = '\0';


	int h = 0;
	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < strlen(str[i]); j++) {
			if (str[i][j] != ' ') {
				str2[i][h] = str[i][j];
				h++;
			}
		}
		h = 0;
	}

	int count = 0;
	int count1 = 0;
	int count2 = 0;

	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < strlen(str[i]); j++) {
			if (i == 0 && str[i][j] == ' ') {
				count++;
			}
			if (i == 1 && str[i][j] == ' ') {
				count1++;
			}
			if (i == 2 && str[i][j] == ' ') {
				count2++;
			}
		}
	}

	polindrom(str2, str, k, p, q, count, count1, count2);


  EXPECT_EQ(k, true);
}

TEST(TestCaseName1, Test1) {

	bool k = true;
	bool p = true;
	bool q = true;

	char** str = new char* [3];
	char** str2 = new char* [3];

	for (int i = 0; i < 3; i++) {
		str[i] = new char[100];
	}
	for (int i = 0; i < 3; i++) {
		str2[i] = new char[100];
	}

	str[0][0] = 'l';
	str[0][1] = 'o';
	str[0][2] = 'l';
	str[0][3] = '\0';

	str[1][0] = 'b';
	str[1][1] = 'a';
	str[1][2] = 'a';
	str[1][3] = '\0';

	str[2][0] = 'p';
	str[2][1] = 'o';
	str[2][2] = 'p';
	str[2][3] = '\0';


	int h = 0;
	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < strlen(str[i]); j++) {
			if (str[i][j] != ' ') {
				str2[i][h] = str[i][j];
				h++;
			}
		}
		h = 0;
	}

	int count = 0;
	int count1 = 0;
	int count2 = 0;

	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < strlen(str[i]); j++) {
			if (i == 0 && str[i][j] == ' ') {
				count++;
			}
			if (i == 1 && str[i][j] == ' ') {
				count1++;
			}
			if (i == 2 && str[i][j] == ' ') {
				count2++;
			}
		}
	}

	polindrom(str2, str, k, p, q, count, count1, count2);


	EXPECT_EQ(q, true);
}

TEST(TestCaseName2, Test2) {

	bool k = true;
	bool p = true;
	bool q = true;

	char** str = new char* [3];
	char** str2 = new char* [3];

	for (int i = 0; i < 3; i++) {
		str[i] = new char[100];
	}
	for (int i = 0; i < 3; i++) {
		str2[i] = new char[100];
	}

	str[0][0] = 'l';
	str[0][1] = 'o';
	str[0][2] = 'l';
	str[0][3] = '\0';

	str[1][0] = 'b';
	str[1][1] = 'a';
	str[1][2] = 'a';
	str[1][3] = '\0';

	str[2][0] = 'p';
	str[2][1] = 'o';
	str[2][2] = 'p';
	str[2][3] = '\0';


	int h = 0;
	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < strlen(str[i]); j++) {
			if (str[i][j] != ' ') {
				str2[i][h] = str[i][j];
				h++;
			}
		}
		h = 0;
	}

	int count = 0;
	int count1 = 0;
	int count2 = 0;

	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < strlen(str[i]); j++) {
			if (i == 0 && str[i][j] == ' ') {
				count++;
			}
			if (i == 1 && str[i][j] == ' ') {
				count1++;
			}
			if (i == 2 && str[i][j] == ' ') {
				count2++;
			}
		}
	}

	polindrom(str2, str, k, p, q, count, count1, count2);


	EXPECT_EQ(p, false);
}

TEST(№3, Test3) {
	char str[] = { "Too swift for Theex, too quick for Gallow" };
	char str2[100];
	char str3[] = { "Tu svift for Ziks, tu kvik for Galov" };
	text(str, str2);
	bool k = true;
	for (int i = 0; i < strlen(str3); i++) {
		if (str2[i] != str3[i]) {
			k = false;
		}
	}
	EXPECT_EQ(k,true);
}

TEST(№4, Test4) {
	char str[] = { "Too strong for young Prince Joseph to follow" };
	char str2[100];
	char str3[] = { "Tu strong for ung Prinse Josef to folov" };
	text(str, str2);
	bool k = true;
	for (int i = 0; i < strlen(str3); i++) {
		if (str2[i] != str3[i]) {
			k = false;
		}
	}
	EXPECT_EQ(k, true);
}

TEST(№5, Test5) {
	char str[] = { "Too strong for young Prince Joseph to follow" };
	char str2[100];
	char str3[] = { "Это неверный результат." };
	text(str, str2);
	bool k = true;
	for (int i = 0; i < strlen(str3); i++) {
		if (str2[i] != str3[i]) {
			k = false;
		}
	}
	EXPECT_EQ(k, false);
}

TEST(№6, Test6) {
	char str[] = { "lol"};
	int chetchik = 0;
	int count = -1;
	bool m = 1;
	EXPECT_EQ(DlinaPolindroma(str, chetchik, count, m), 2);
}

TEST(№7, Test7) {
	char str[] = { "A" };
	int chetchik = 0;
	int count = -1;
	bool m = 1;
	EXPECT_EQ(DlinaPolindroma(str, chetchik, count, m), -1);
	
}

TEST(№8, Test8) {
	char str[] = { "privet" };
	int chetchik = 0;
	int count = -1;
	bool m = 1;
	EXPECT_EQ(DlinaPolindroma(str, chetchik, count, m), 6);
}

TEST(№9, Test9) {
	char str[] = { "HOLLOH" };
	int chetchik = 0;
	int count = -1;
	bool m = 1;
	EXPECT_EQ(DlinaPolindroma(str, chetchik, count, m), 5);
}

TEST(№10, Test10) {
	char str[] = { "1234e-2e-2e-2" };
	int str2[100];
	double SUM = 0.0;

	EXPECT_EQ(SumChicel(str, str2, SUM) , 12.32);
}

TEST(№11, Test11) {
	char str[] = { "1234e-2e-2e-2e+-2" };
	int str2[100];
	double SUM = 0.0;

	EXPECT_EQ(SumChicel(str, str2, SUM), 10.32);
}

TEST(№12, Test12) {
	char str[] = { "25sdfjdshf50vjdfjkg25kfkjg77" };
	int str2[100];
	double SUM = 0.0;

	EXPECT_EQ(SumChicel(str, str2, SUM), 177);
}

TEST(№13, Test13) {
	char str[] = { "10e10" };
	int str2[100];
	double SUM = 0.0;

	EXPECT_EQ(SumChicel(str, str2, SUM), 10e10);
}
//
//TEST(№, Test) {
//
//	EXPECT_EQ(1, 1);
//}







