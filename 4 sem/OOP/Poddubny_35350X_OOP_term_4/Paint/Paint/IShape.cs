using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public interface IShape
    {
        bool Filled { get; set; }
        char Fill {  get; set; }
        void Draw(char[,] canvas/*, bool filled, char fill*/);
        void Move(int dx, int dy);
        int X { get; set; }
        int Y { get; set; }
        IShape Clone();
    }
}
