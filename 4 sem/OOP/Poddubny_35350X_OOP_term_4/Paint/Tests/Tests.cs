using Paint;
using System.Drawing;
namespace Tests
{
    [TestClass]
    public class Tests
    {
        private const int Width = 180;
        private const int Height = 30;

        [TestMethod]
        public void TestDrawRectangle()
        {
            int x = 1;
            int y = 1;
            bool filled = false;
            char fill = '*';

            Paint.Rectangle shape = new Paint.Rectangle(x, y, Width, Height, filled, fill);
            char[,] canvas = new char[Height, Width];
            shape.Draw(canvas);

            y /= 2;
            int height = Height / 2;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (y + i < canvas.GetLength(0) && x + j < canvas.GetLength(1))
                    {
                        if (filled || i == 0 || i == height - 1 || j == 0 || j == Width - 1)
                            Assert.AreEqual(fill, canvas[y + i, x + j]);

                    }
                }
            }
        }
        [TestMethod]
        public void TestDrawFilledCircle()
        {
            int x = 20;
            int y = 20;
            int radius = 10;
            bool filled = true;
            char fill = '0';

            var circle = new Circle(x, y, radius, filled, fill);
            char[,] canvas = new char[Height, Width];

            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    canvas[i, j] = ' ';
                }
            }

            circle.Draw(canvas);

            double aspectRatio = 2.0; 
            int adjustedY = y / 2; 

            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    int dx = j - x;
                    int dy = (int)((i - adjustedY) * aspectRatio); 
                    int distanceSquared = dx * dx + dy * dy;

                    if (distanceSquared <= radius * radius)
                    {
                        Assert.AreEqual(fill, canvas[i, j], $"Pixel at ({i}, {j}) should be '{fill}'.");
                    }
                    else
                    {
                        Assert.AreEqual(' ', canvas[i, j], $"Pixel at ({i}, {j}) should be ' '.");
                    }
                }
            }
        }
        [TestMethod]
        public void TestDrawFilledTriangle()
        {
            int x = 20; 
            int y = 4; 
            int height = 10; 
            int baseLength = 12; 
            bool filled = true;
            char fill = 'X'; 

            var triangle = new Triangle(x, y, height, baseLength, filled, fill);
            char[,] canvas = new char[Height, Width]; 

            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    canvas[i, j] = ' ';
                }
            }
            triangle.Draw(canvas);

            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    int adjustedY = y / 2;
                    int x1 = x;
                    int y1 = adjustedY;
                    int x2 = x - baseLength;
                    int y2 = adjustedY + height / 2;
                    int x3 = x + baseLength;
                    int y3 = adjustedY + height / 2;

                    if (Triangle.IsPointInTriangle(j, i, x1, y1, x2, y2, x3, y3))
                    {
                        Assert.AreEqual(fill, canvas[i, j], $"Pixel at ({i}, {j}) should be '{fill}'.");
                    }
                }
            }
        }
        //undo 
        //redo


        [TestMethod]
        public void TestMoveRectangle()
        {
            var rect = new Paint.Rectangle();
            rect.X = 0;
            rect.Y = 0;

            rect.Move(20, 23);
            Assert.AreEqual(20, rect.X);
            Assert.AreEqual(23, rect.Y);

            rect.Move(-9, -5);
            Assert.AreEqual(11, rect.X);
            Assert.AreEqual(18, rect.Y);
        }

    }
    [TestClass]
    public class TestConsolePaint
    {
        [TestMethod]
        public void TestUndo()
        {
            var app = new ConsolePaint();
            app.shapes.Add(new Circle(1,1,20,true,'6'));
            app.Undo();
            Assert.AreEqual(0, app.shapes.Count);
        }
        [TestMethod]
        public void TestRedo()
        {
            var app = new ConsolePaint();
            app.shapes.Add(new Circle(1, 1, 20, true, '6'));
            app.SaveState();
            app.Undo();

            app.SaveState();
            app.Redo();
            Assert.AreEqual(1, app.shapes.Count);
        }

    }
}
