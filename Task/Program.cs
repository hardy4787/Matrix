using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pracrik1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matrix = new double[3, 3] { { 1, -1, 1 }, { 2, 0, 1 }, { 1, -1, 1 } };
            QMatrix matrix1 = new QMatrix(matrix);
            Console.WriteLine(matrix1.PrintMatrix());

            matrix = new double[3, 3] { { 1, -1, 1 }, { 2, 0, 1 }, { 1, -1, 1 } };
            QMatrix matrix2 = new QMatrix(matrix);

            if (matrix1 == matrix2)
                Console.WriteLine("Матрицы равны");
            else
                Console.WriteLine("Матрицы не равны");
            Console.WriteLine("\nПеремножение матриц");
            Console.WriteLine((matrix1 * matrix2).PrintMatrix());
            Console.WriteLine("Сложение матриц");
            Console.WriteLine((matrix1 + matrix2).PrintMatrix());
            Console.WriteLine("Вычетание матриц");
            Console.WriteLine((matrix1 - matrix2).PrintMatrix());

            Console.ReadLine();
        }
    }
}
