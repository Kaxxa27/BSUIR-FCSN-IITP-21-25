#include "pch.h"

TEST(TestCaseName, TestName) {
	int* arr = new int[5];
	arr[0] = -1;
	arr[1] = -1;
	arr[2] = 0;
	arr[3] = 0;
	arr[4] = 0;
	int count = 0;
	odna_tret(arr, 5,count, 0);
	dwe_treti(arr, 5, count, 0);

	EXPECT_EQ(count, 3);
  
}
TEST(TestCaseName2, TestName2) {
	int sum = 0; 
	Sumchet(777, sum);
	EXPECT_EQ(sum, 201537);

}
TEST(TestCaseName1, TestName1) {
	int sum = 0;
	Sumchet(1, sum);
	EXPECT_EQ(sum, 1);

}
TEST(TestCaseName3, TestName3) {
	int sum = 0;
	Sumchet(INT16_MAX, sum);
	EXPECT_EQ(sum, 357913941);

}
TEST(TestCaseName4, TestName4) {
	int sum = 0;
	Sumchet(109, sum);
	EXPECT_EQ(sum, 4013);

}
TEST(TestCaseName5, TestName5) {
	int sum = 0;
	S(1,10, sum);
	EXPECT_EQ(sum, 46);

}
TEST(TestCaseName6, TestName6) {
	int sum = 0;
	S(10, 20, sum);
	EXPECT_EQ(sum, 48);

}