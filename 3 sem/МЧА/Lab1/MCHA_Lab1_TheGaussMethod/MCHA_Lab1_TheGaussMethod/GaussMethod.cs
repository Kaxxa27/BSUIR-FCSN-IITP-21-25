using System;
using System.Collections.Generic;

namespace MCHA_Lab1_TheGaussMethod
{
    class GaussMethod
    {
        #region Variable
        static List<List<double>> Matrix_A = new List<List<double>>()
            {
                new List<double>(){1,2,3,4,5 },
                new List<double>(){1,2,3,4,5 },
                new List<double>(){1,2,3,4,5 },
                new List<double>(){1,2,3,4,5 },
                new List<double>(){1,2,3,4,5 },
            };

        static Data data = new Data();

        static List<double> results = new List<double>() { 1, 2, 3, 4, 5 };

        delegate void Variant();

        #endregion

        #region Main
        static void Main(string[] args)
        {
            Variant Part1MatrixCal, Part2TransformMatrix, Part3Roots;

            Part1MatrixCal = Matrix_A_Calculation;
            Part1MatrixCal += View_Matrix;

            Part2TransformMatrix = Transform_Matrix;
            Part2TransformMatrix += View_Matrix_With_Vector_B;

            Part3Roots = Find_Solutions;
            Part3Roots += View_Roots;

            //VARIANT №1
            GreenColorConsole("Variant №1\nThe original matrix:");
            Part1MatrixCal();

            GreenColorConsole("The transformed matrix:");
            Part2TransformMatrix();

            GreenColorConsole("Roots of the solution:");
            Part3Roots();

            //VARIANT №2 Main elemnt in column

            GreenColorConsole("\nVariant №2 Main elemnt in column\nThe original matrix:");
            Part1MatrixCal();

            data.OriginalStateOfVetor_B();
            FindMainElementInColumn();
            SwapRows();

            GreenColorConsole("The transformed matrix:");
            Part2TransformMatrix();

            GreenColorConsole("Roots of the solution:");
            Part3Roots();

            //VARIANT №3 Main element in all Matrix

            GreenColorConsole("\nVariant №3 Main elemnt in All Matrix\nThe original matrix:");  
            Part1MatrixCal();

            data.OriginalStateOfVetor_B();
            FindMainElementInAllMatrix();
            SwapRows();

            GreenColorConsole("The transformed matrix:");
            Part2TransformMatrix();

            GreenColorConsole("Roots of the solution:");
            Part3Roots();

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
                    str += string.Format("{0:F15}", Matrix_A[i][j]);
                    Console.Write($"{str} ");
                    str = "";
                }
                Console.WriteLine("\n");
            }
        }
      

        static public void View_Matrix_With_Vector_B()
        {

            string str = "";

            for (int i = 0; i < Matrix_A.Count; i++)
            {
                for (int j = 0; j < Matrix_A.Count; j++)
                {
                    str += string.Format("{0:F15}", Matrix_A[i][j]);
                    Console.Write($"{str} ");
                    str = "";
                }

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"|{data.Vector_B[i]}\n");
                Console.ResetColor();
            }
        }
        #endregion

        #region Transform_Matrix
        static public void Transform_Matrix()
        {
            for (int i = 0; i < Matrix_A.Count; i++)
            {
                for (int j = i + 1; j < Matrix_A.Count; j++)
                {
                    data.q = Matrix_A[j][i] / Matrix_A[i][i];
                    data.Vector_B[j] -= data.q * data.Vector_B[i];
                    Matrix_A[j][i] = 0;
                    for (int l = i + 1; l < Matrix_A.Count; l++)
                    {
                        Matrix_A[j][l] -= data.q * Matrix_A[i][l];
                    }
                }
            }
        }
        #endregion

        #region Roots
        static public void Find_Solutions()
        {
            for (int i = Matrix_A.Count - 1; i >= 0; i--)
            {
                for (int j = i; j < Matrix_A.Count; j++)
                {
                    if (j == Matrix_A.Count - 1) break;
                    else data.Vector_B[i] -= Matrix_A[i][j + 1] * results[j + 1];
                }
                results[i] = (data.Vector_B[i] / Matrix_A[i][i]);
            }
        }

        static public void View_Roots()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach(var item in results)
            {
                Console.WriteLine(item);
            }
            Console.ResetColor();
        }
        #endregion

        #region Variant2
        static public void FindMainElementInColumn()
        {
            for (int i = 0; i < Matrix_A.Count; i++)
            {
                int max_index = i;
                double max = Matrix_A[i][i];
                for (int j = i + 1; j < Matrix_A.Count; j++)
                {
                    if (Math.Abs(max) < Math.Abs(Matrix_A[j][i]))
                    {
                        max_index = j;
                        max = Matrix_A[j][i];
                    }
                }
            }
        }

        static public void SwapRows()
        {
            for (int i = 0; i < Matrix_A.Count; i++)
            {
                int max_index = i;
                if (i != max_index)
                {
                    double temp_root = data.Vector_B[i];
                    data.Vector_B[i] = data.Vector_B[max_index];
                    data.Vector_B[max_index] = temp_root;
                    for (int j = i; j < Matrix_A.Count; j++)
                    {
                        double temp = Matrix_A[i][j];
                        Matrix_A[i][j] = Matrix_A[max_index][j];
                        Matrix_A[max_index][j] = temp;
                    }
                }
            }
        }
        #endregion

        #region Variant3
        static public void FindMainElementInAllMatrix()
        {
            for (int i = 0; i < Matrix_A.Count; i++)
            {
                int max_index = i;
                double max = Matrix_A[i][i];
                for (int j = i; j < Matrix_A.Count; j++)
                {
                    for (int k = i; k < Matrix_A.Count; k++)
                    {
                        if (Math.Abs(max) < Math.Abs(Matrix_A[j][k]))
                        {
                            max_index = j;
                            max = Matrix_A[j][k];
                        }
                    }
                }
            }
        }
        #endregion
    }

    #region Class Data
    class Data
    {
        public List<List<double>> Matrix_C { get; set; }

        public List<List<double>> Matrix_D { get; set; }

        public List<double> Vector_B { get; set; }

        public double K { get; set; }

        public double q { get; set; }

        public Data()
        {
            Matrix_C = new List<List<double>>()
        {
            new List<double>() {0.2 * 0.0000000000000001, 0.0, 0.2, 0.0, 0.2},
            new List<double>() {0.0, 0.2, 0.0, 0.2, 0.0},
            new List<double>() {0.2, 0.0, 0.2, 0.0, 0.2},
            new List<double>() {0.0, 0.2, 0.0, 0.2, 0.0},
            new List<double>() {0.0, 0.0, 0.2, 0.0, 0.2},
        };

            Matrix_D = new List<List<double>>()
        {
           
            new List<double>() {2.33, 0.81, 0.67, 0.92, -0.53},
            new List<double>() {-0.53, 2.33, 0.81, 0.67, 0.92},
            new List<double>() {0.92, -0.53, 2.33, 0.81, 0.67},
            new List<double>() {0.67, 0.92, -0.53, 2.33, 0.81},
            new List<double>() {0.81, 0.67, 0.92, -0.53, 2.33}
        };

            Vector_B = new List<double> { 4.2, 4.2, 4.2, 4.2, 4.2 };

            K = 10;
            q = 0.0;
        }

        public void OriginalStateOfVetor_B()
        {
            for (int i = 0; i < Vector_B.Count; i++) Vector_B[i] = 4.2;
        }
            

    }
    #endregion
}