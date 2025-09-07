using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    [Serializable]
    public class Rectangle : IShape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Filled { get; set; } = false;
        public char Fill { get; set; } = '*';

        public Rectangle() { }
        public Rectangle(int x, int y, int width, int height,bool filled, char fill)
        {
            Filled = filled;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Fill = fill;
        }

        /*public void Draw(char[,] canvas)
        {
            int adjustedY = Y / 2;
            int adjustedHeight = Height / 2;
            for (int i = 0; i < adjustedHeight; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (adjustedY + i < canvas.GetLength(0) && X + j < canvas.GetLength(1))
                    {
                        if (Filled || i == 0 || i == adjustedHeight - 1 || j == 0 || j == Width - 1)
                            canvas[adjustedY + i, X + j] = Fill;
                    }
                }
            }
        }*/
        public void Draw(char[,] canvas)
        {
            int adjustedY = Y / 2;
            int adjustedHeight = Height / 2;

            for (int i = 0; i < adjustedHeight; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int canvasY = adjustedY + i;
                    int canvasX = X + j;

                    if (canvasY >= 0 && canvasY < canvas.GetLength(0) &&
                        canvasX >= 0 && canvasX < canvas.GetLength(1))
                    {
                        if (Filled || i == 0 || i == adjustedHeight - 1 || j == 0 || j == Width - 1)
                        {
                            canvas[canvasY, canvasX] = Fill;
                        }
                    }
                }
            }
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
        public IShape Clone()
        {
            return new Rectangle(X, Y, Width, Height,Filled,Fill);
        }
    }


}
