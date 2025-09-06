using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    [Serializable]
    public class Circle : IShape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public bool Filled { get; set; } = false;
        public char Fill { get; set; } = '*';

        public Circle() { }

        public Circle(int cx, int cy, int radius,bool filled, char fill)
        {
            Filled = filled;
            X = cx;
            Y = cy;
            Radius = radius;
            Fill = fill;
        }

        
        public void Draw(char[,] canvas/*, bool filled, char fill*/)
        {/*
            Filled = filled;
            Fill = fill;*/
            double aspectRatio = 2.0;
            int adjustedY = Y / 2;
            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    int dx = j - X;
                    int dy = (int)((i - adjustedY) * aspectRatio); 

                    int distanceSquared = dx * dx + dy * dy;

                    if (distanceSquared <= Radius * Radius && distanceSquared >= (Radius - 1) * (Radius - 1)|| Filled && distanceSquared < Radius * Radius)
                    {
                        canvas[i, j] = Fill;
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
            return new Circle(X, Y, Radius,Filled,Fill); 
        }
    }
}
