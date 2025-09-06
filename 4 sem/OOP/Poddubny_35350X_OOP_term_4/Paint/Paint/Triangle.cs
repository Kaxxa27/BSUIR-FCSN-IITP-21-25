using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public class Triangle : IShape
    {
        public bool Filled { get; set; } = false;
        public int X { get; set; }
        public int Y { get; set; } 
        public int Height { get; set; } 
        public int Base { get; set; }
        public char Fill { get; set; } = '*';

        public Triangle() { } 

        public Triangle(int x, int y, int height, int baseLength, bool filled, char fill)
        {
            Filled = filled;
            X = x;
            Y = y;
            Height = height;
            Base = baseLength;
            Fill = fill;
        }

        /*public void Draw(char[,] canvas*//*, bool filled, char fill*//*)
        {
*//*
            Filled = filled;
            Fill = fill;*//*
            int x1 = X - Base;
            int y1 = (Y/2 + Height/2);   
            int x2 = X + Base;
            int y2 = (Y/2 + Height/2);  

            DrawLine(canvas, X, Y/2, x1, y1);
            DrawLine(canvas, x1, y1, x2, y2);
            DrawLine(canvas, x2, y2, X, Y/2);

            if (Filled)
            {
                FillTriangle(canvas, X, Y/2, x1, y1, x2, y2);
            }
        }*/

        private void DrawLine(char[,] canvas, int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                if (x1 >= 0 && x1 < canvas.GetLength(1) && y1 >= 0 && y1 < canvas.GetLength(0))
                {
                    canvas[y1, x1] = Fill;
                }

                if (x1 == x2 && y1 == y2) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        /*private void FillTriangle(char[,] canvas, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            int minX = Math.Min(x1, Math.Min(x2, x3));
            int maxX = Math.Max(x1, Math.Max(x2, x3));
            int minY = Math.Min(y1, Math.Min(y2, y3));
            int maxY = Math.Max(y1, Math.Max(y2, y3));

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (IsPointInTriangle(x, y, x1, y1, x2, y2, x3, y3))
                    {
                        if (x >= 0 && x < canvas.GetLength(1) && y >= 0 && y < canvas.GetLength(0))
                        {
                            canvas[y, x] = Fill;
                        }
                    }
                }
            }
        }*/
        public void Draw(char[,] canvas)
        {
            int x1 = X - Base;
            int y1 = (Y / 2 + Height / 2);
            int x2 = X + Base;
            int y2 = (Y / 2 + Height / 2);

            x1 = Math.Clamp(x1, 0, canvas.GetLength(1) - 1);
            y1 = Math.Clamp(y1, 0, canvas.GetLength(0) - 1);
            x2 = Math.Clamp(x2, 0, canvas.GetLength(1) - 1);
            y2 = Math.Clamp(y2, 0, canvas.GetLength(0) - 1);

            DrawLine(canvas, X, Y / 2, x1, y1);
            DrawLine(canvas, x1, y1, x2, y2);
            DrawLine(canvas, x2, y2, X, Y / 2);

            if (Filled)
            {
                FillTriangle(canvas, X, Y / 2, x1, y1, x2, y2);
            }
        }

        private void FillTriangle(char[,] canvas, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            int minX = Math.Min(x1, Math.Min(x2, x3));
            int maxX = Math.Max(x1, Math.Max(x2, x3));
            int minY = Math.Min(y1, Math.Min(y2, y3));
            int maxY = Math.Max(y1, Math.Max(y2, y3));

            minX = Math.Clamp(minX, 0, canvas.GetLength(1) - 1);
            maxX = Math.Clamp(maxX, 0, canvas.GetLength(1) - 1);
            minY = Math.Clamp(minY, 0, canvas.GetLength(0) - 1);
            maxY = Math.Clamp(maxY, 0, canvas.GetLength(0) - 1);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (IsPointInTriangle(x, y, x1, y1, x2, y2, x3, y3))
                    {
                        canvas[y, x] = Fill;
                    }
                }
            }
        }

        static public bool IsPointInTriangle(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            double d1 = Sign(x, y, x1, y1, x2, y2);
            double d2 = Sign(x, y, x2, y2, x3, y3);
            double d3 = Sign(x, y, x3, y3, x1, y1);

            bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(hasNeg && hasPos);
        }

        static public double Sign(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            return (x1 - x3) * (y2 - y3) - (x2 - x3) * (y1 - y3);
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public IShape Clone()
        {
            return new Triangle(X, Y, Height, Base,Filled,Fill);
        }
    }
}
