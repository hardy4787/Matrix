using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class QMatrix : ICloneable
    {
        private delegate double PlusOrMinus(double a, double b);
        private int columns;
        private int rows;
        public double[,] MatrixValue { get; set; }

        public QMatrix(double[,] value)
        {
            this.columns = value.GetLength(1);
            rows = value.GetLength(0);
            MatrixValue = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    MatrixValue[i, j] = value[i, j];
                }
            }
        }

        public QMatrix(int rows, int columns)
        {
            if (columns < 0 || rows < 0)
                throw new MatrixException("Matrix must have at least one line or column");
            this.columns = columns;
            this.rows = rows;
            MatrixValue = new double[rows, columns];
        }

        public double this[int columns, int rows]
        {
            get
            {
                if (columns < 0 || rows < 0 || this.columns < columns || this.rows < rows)
                    throw new MatrixException("Out of range matrix");
                return MatrixValue[columns, rows];
            }
            set { MatrixValue[columns, rows] = value; }
        }
        private static QMatrix PlusOrMinusMatrix(QMatrix a, QMatrix b, PlusOrMinus Calculation)
        {
            if ((object)a == null || (object)b == null)
                throw new NullReferenceException("The object is null. Initialize the matrix");
            if (a.columns != b.columns || a.rows != b.rows)
                throw new MatrixException("Matrices must have the same number of columns and rows");
            QMatrix matrix = new QMatrix(a.rows, a.columns);
            for (int i = 0; i < matrix.rows; i++)
                for (int j = 0; j < matrix.columns; j++)
                    matrix[i, j] = Calculation(a[i,j],b[i,j]);
            return matrix;
        }
        public static QMatrix operator +(QMatrix left, QMatrix right)
        {
            return PlusOrMinusMatrix(left, right, (x, y) => x + y);
        }
        public static QMatrix operator -(QMatrix left, QMatrix right)
        {
            return PlusOrMinusMatrix(left, right, (x, y) => x - y);
        }
        public string PrintMatrix()
        {
            string result = "\n";
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    result += $"{this.MatrixValue[i, j]} ";
                }
                result += "\n";
            }
            return result;
        }
        public static QMatrix operator *(QMatrix a, QMatrix b)
        {
            if (a == (object)null || b == (object)null)
                throw new ArgumentNullException("The object is null. Initialize the matrix");
            if (a.columns != b.rows)
                throw new MatrixException("Matrices must have the same number of columns and rows");
            QMatrix multy = new QMatrix(a.rows, b.columns);
            for (int i = 0; i < a.rows; i++)
                for (int j = 0; j < b.columns; j++)
                    for (int k = 0; k < b.rows; k++)
                        multy[i, j] += a[i, k] * b[k, j];
            return multy;
        }

        public static bool operator ==(QMatrix a, QMatrix b)
        {
            if (a == (object)null || b == (object)null)
                throw new ArgumentNullException("The object is null. Initialize the matrix");
            if ((a.rows != b.rows) || (a.columns != b.columns))
                throw new MatrixException("Matrices have different sizes");
            for (int i = 0; i < a.rows; i++)
                for (int j = 0; j < b.columns; j++)
                    if (a[i, j] != b[i, j])
                        return false;
            return true;
        }

        public static bool operator !=(QMatrix a, QMatrix b)
        {
            return !(a == b);
        }

        public object Clone()
        {
            return new QMatrix(this.MatrixValue);
        }
        public override bool Equals(object obj)
        {
            QMatrix ComparableMatrix = obj as QMatrix;
            if (ComparableMatrix == (object)null)
                throw new ArgumentNullException("The object is null. Initialize the matrix");
            return this == ComparableMatrix ? true : false;
        }
        public override int GetHashCode()
        {
            var hashCode = 192619276;
            hashCode = hashCode * -1521134295 + EqualityComparer<double[,]>.Default.GetHashCode(MatrixValue);
            return hashCode;
        }
    }
}
