using System;
using System.Collections.Generic;

namespace MCHA_Lab2_SimpleIter_Method_And_Zeidel
{
    class GaussMethod
    {
        #region Variable
        static List<List<double>> Matrix_A = new List<List<double>>()
            {
                new List<double>(){1,2,3,4,5},
                new List<double>(){1,2,3,4,5},
                new List<double>(){1,2,3,4,5},
                new List<double>(){1,2,3,4,5},
                new List<double>(){1,2,3,4,5},
            };

        static List<List<double>> Matrix_A_With_Vector_B = new List<List<double>>()
            {
                new List<double>(){1,2,3,4,5,6},
                new List<double>(){1,2,3,4,5,6},
                new List<double>(){1,2,3,4,5,6},
                new List<double>(){1,2,3,4,5,6},
                new List<double>(){1,2,3,4,5,6},
            };

        static Data data = new Data();

        static List<double> results = new List<double>() { 1, 2, 3, 4, 5 };

        delegate void Variant();

        #endregion

        #region Main
        static void Main(string[] args)
        {
            Variant Part1_MatrixCal, Part2_Matrix_A_With_B, Part3_Simple_Iterations_Method, Part4_Zeidel_Method;

            Part1_MatrixCal = Matrix_A_Calculation;
            //Part1_MatrixCal += View_Matrix;

            Part2_Matrix_A_With_B = View_Matrix_With_Vector_B;
            Part2_Matrix_A_With_B += Matrix_A_Sum_Vector_B;

            Part3_Simple_Iterations_Method = Simple_Iters_Method;
            Part3_Simple_Iterations_Method += View_Roots;

            Part4_Zeidel_Method = Zeidel_Method;
            Part4_Zeidel_Method += View_Roots;
            //Simple Iterations

       
            GreenColorConsole("Исходная матрица А:");
            Part1_MatrixCal();

           // Matrix_A[0][0] = 0;
        



            View_Matrix();

            
            GreenColorConsole("Расширенная матрица А:");
            Part2_Matrix_A_With_B();
           

            Console.WriteLine($" Ранг исходной матрицы: {Rank(Matrix_A)}");
            Console.WriteLine($" Ранг расширенной матрицы: {Rank(Matrix_A_With_Vector_B)}\n");

            if (Rank(Matrix_A) != Rank(Matrix_A_With_Vector_B))
            {
                Console.WriteLine(" Ранг исходной матрицы не равен рангу расширенной матрицы.\n СЛАУ не имеет решений.");
   
            }
            else if (Rank(Matrix_A) < Matrix_A.Count)
            { 
                Console.WriteLine(" Ранги матриц равны и меньше числа неизвестных системы.\n СЛАУ имеет бесконечное множество решений.");            
            }
            else
            {

                

                GreenColorConsole("Метод простых итераций:");
                Part3_Simple_Iterations_Method();

                GreenColorConsole("Метод Зейделя:");
                Part4_Zeidel_Method();
            }
        }
        #endregion

        #region CreenColor
        static public void GreenColorConsole(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{str}");
            Console.ResetColor();
        }
        #endregion

        #region Matrix_A_Calculation
        static public void Matrix_A_Calculation()
        {
            for (int i = 0; i < Matrix_A.Count; i++)
            {
                for (int j = 0; j < Matrix_A.Count; j++)
                {
                    Matrix_A[i][j] = (data.K * data.Matrix_C[i][j] + data.Matrix_D[i][j]);
                }
            }
        }
        #endregion

        #region View_Matrix/VectorB
        static public void View_Matrix()
        {

            string str = "";

            for (int i = 0; i < Matrix_A.Count; i++)
            {
                for (int j = 0; j < Matrix_A.Count; j++)
                {
                    str += string.Format("{0:F5}", Matrix_A[i][j]);
                    Console.Write($"{str} ");
                    str = "";
                }
                Console.Write("\n");
            }
            Console.WriteLine();
        }


        static public void View_Matrix_With_Vector_B()
        {
            string str = "";

            for (int i = 0; i < Matrix_A.Count; i++)
            {
                for (int j = 0; j < Matrix_A.Count; j++)
                {
                    str += string.Format("{0:F5}", Matrix_A[i][j]);
                    Console.Write($"{str} ");
                    str = "";
                }

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                str += string.Format("{0:F5}", data.Vector_B[i]);
                Console.Write($"|{str}\n");
                str = "";
                Console.ResetColor();

            }
            Console.WriteLine();
        }
        #endregion

        #region Matrix_A + Vector_B
        static public void Matrix_A_Sum_Vector_B()
        {
            Matrix_A_With_Vector_B = Matrix_A;

            Matrix_A_With_Vector_B[0].Add(data.Vector_B[0]);
            Matrix_A_With_Vector_B[1].Add(data.Vector_B[1]);
            Matrix_A_With_Vector_B[2].Add(data.Vector_B[2]);
            Matrix_A_With_Vector_B[3].Add(data.Vector_B[3]);
            Matrix_A_With_Vector_B[4].Add(data.Vector_B[4]);
        }
        #endregion

        #region Zeidel/SimpleIter_Method
        static public void Zeidel_Method()
        {
            data.iterations = 0;

            int size = Matrix_A.Count;

            // Неизвестные на предыдущей итерации
            List<double> previousVariable = new List<double> {0,0,0,0,0};
           
            // Пока не будет достигнута точность
            bool stop = false;
            while (!stop)
            {
                data.iterations++;

                // Неизвестные на текущей итерации  
                List<double> currentVariable = new List<double> { 0, 0, 0, 0, 0 };

                for (int i = 0; i < size; i++)
                {
                    // x_i = b_i
                    currentVariable[i] = Matrix_A_With_Vector_B[i][size];

                    // Вычитаем сумму по всем отличным от i-ой неизвестным
                    for (int j = 0; j < size; j++)
                    {
                        // С этой итерации
                        if (j < i)
                        {
                            currentVariable[i] -= Matrix_A_With_Vector_B[i][j] * currentVariable[j];
                        }

                        // С прошой итерации
                        if (j > i)
                        {
                            currentVariable[i] -= Matrix_A_With_Vector_B[i][j] * previousVariable[j];
                        }
                    }

                    // x_i /= b_i
                    currentVariable[i] /= Matrix_A_With_Vector_B[i][i];
                }

                // Максимальная погрешность
                double max_error = 0.0;
                for (int i = 0; i < size; i++)
                {
                    double new_max_error = Math.Abs(currentVariable[i] - previousVariable[i]);
                    max_error = new_max_error > max_error ? new_max_error : max_error;
                }

                // Дотигнута ли точность
                if (max_error < data.eps)
                {
                    stop = true;
                }

                // Переход к следующей итерации
                previousVariable = currentVariable;
            }
            results = previousVariable;
        }
        static public void Simple_Iters_Method()
        {
            data.iterations = 0;

            int size = Matrix_A.Count;

            // Неизвестные на предыдущей итерации
            List<double> previousVariable = new List<double> { 0, 0, 0, 0, 0 };

            // Пока не будет достигнута точность
            bool stop = false;
            while (!stop)
            {
                data.iterations++;

                // Неизвестные на текущей итерации  
                List<double> currentVariable = new List<double> { 0, 0, 0, 0, 0 };

                for (int i = 0; i < size; i++)
                {
                    // x_i = b_i
                    currentVariable[i] = Matrix_A_With_Vector_B[i][size];

                    // Вычитаем сумму по всем отличным от i-ой неизвестным
                    for (int j = 0; j < size; j++)
                    {
                        // С прошой итерации
                        if (j != i)
                            currentVariable[i] -= Matrix_A_With_Vector_B[i][j] * previousVariable[j];
                    }

                    // x_i /= b_i
                    currentVariable[i] /= Matrix_A_With_Vector_B[i][i];
                }

                // Максимальная погрешность
                double max_error = 0.0;
                for (int i = 0; i < size; i++)
                {
                    double new_max_error = Math.Abs(currentVariable[i] - previousVariable[i]);
                    max_error = new_max_error > max_error ? new_max_error : max_error;
                }

                // Дотигнута ли точность
                if (max_error < data.eps)
                {
                    stop = true;
                }

                // Переход к следующей итерации
                previousVariable = currentVariable;
            }
            results = previousVariable;
        }
        static public void View_Roots()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Кол-во итераций метода:{data.iterations}");

            string str = "";
            foreach (var item in results)
            {
                str += string.Format("{0:F5}", item);
                Console.Write($"{str}\n");
                str = "";
               //Console.WriteLine(item);
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        #endregion

        #region Rank
        static public int Rank(List<List<double>> Matrix)
        {
            const double EPS = 1E-9;

            int n = Matrix.Count;
            int m = Matrix[0].Count;

            int rank = Math.Max(n, m);


            List<bool> line_used = new List<bool>(n) { false, false, false, false, false, false };

            for (int i = 0; i < m; ++i)
            {
                int j;
                for (j = 0; j < n; ++j)
                {
                    if (!line_used[j] && Math.Abs(Matrix[j][i]) > EPS)
                    {
                        break;
                    }
                }

                if (j == n)
                {
                    --rank;
                }
                else
                {
                    line_used[j] = true;
                    for (int p = i + 1; p < m; ++p)
                        Matrix[j][p] /= Matrix[j][i];

                    for (int k = 0; k < n; ++k)
                        if (k != j && Math.Abs(Matrix[k][i]) > EPS)
                            for (int p = i + 1; p < m; ++p)
                                Matrix[k][p] -= Matrix[j][p] * Matrix[k][i];
                }
            }
            return rank;
        }

    }
    #endregion

    #region Class Data
    class Data
    {
        public List<List<double>> Matrix_C { get; set; }

        public List<List<double>> Matrix_D { get; set; }

        public List<double> Vector_B { get; set; }

        public double K { get; set; }

        public double eps { get; set; }

        public double iterations { get; set; }

        public Data()
        {
            Matrix_C = new List<List<double>>()
            {
                new List<double>() {0.01, 0, -0.02, 0, 0    },
                new List<double>() {0.01, 0.01, -0.02, 0, 0 },
                new List<double>() {0, 0.01, 0.01, 0, -0.02 },
                new List<double>() {0, 0, 0.01, 0.01, 0     },
                new List<double>() {0, 0, 0, 0.01, 0.01     },
            };

            Matrix_D = new List<List<double>>()
            {

                new List<double>() {1.3300, 0.2100, 0.1700, 0.1200, -0.1300 },
                new List<double>() {-0.1300, -1.3300, 0.1100, 0.1700, 0.1200},
                new List<double>() {0.1200, -0.1300, -1.3300, 0.1100, 0.1700},
                new List<double>() {0.1700, 0.1200, -0.1300, -1.3300, 0.1100},
                new List<double>() { 0.1100, 0.6700, 0.1200, -0.1300, -1.3300}
            };

            Vector_B = new List<double> { 1.2, 2.2, 4.0, 0, -1.2 };

            K = 10;
            eps = 0.0001;
            iterations = 0;
        }
        public void OriginalStateOfVetor_B()
        {
            Vector_B[0] = 1.2;
            Vector_B[1] = 2.2;
            Vector_B[2] = 4.0;
            Vector_B[3] = 0;
            Vector_B[4] = -1.2;
        }
    }
    #endregion
}

