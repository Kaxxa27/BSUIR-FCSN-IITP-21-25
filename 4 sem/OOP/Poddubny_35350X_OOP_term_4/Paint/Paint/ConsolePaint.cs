using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.Tests")]

namespace Paint
{
    public class ConsolePaint
    {
        private char[,] canvas;
        private const int Width = 180;
        private const int Height = 30;
        public List<IShape> shapes = new();
        public Stack<List<IShape>> undoStack = new();
        public Stack<List<IShape>> redoStack = new();

        public ConsolePaint()
        {
            canvas = new char[Height, Width];
            ClearCanvas();
            SaveState();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ClearCanvas();
                foreach (var shape in shapes)
                    shape.Draw(canvas);
                RenderCanvas();
                ShowMenu();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nConsole Paint - Choose an action:");
            Console.WriteLine("1. Draw Rectangle");
            Console.WriteLine("2. Draw Circle");
            Console.WriteLine("3.  Draw triangle");
            Console.WriteLine("4. Move last shape");
            Console.WriteLine("5. Undo");
            Console.WriteLine("6. Redo");
            Console.WriteLine("7. Save Shapes to JSON");
            Console.WriteLine("8. Load Shapes from JSON");
            Console.WriteLine("9. Delete shape");
            Console.WriteLine("0. quit");
            Console.Write("Enter your choice: ");

            switch (Console.ReadLine())
            {
                case "1":
                    DrawRectangle();
                    break;
                case "2":
                    DrawCircle();
                    break;
                case "3":
                    DrawTriangle();
                    break;
                case "4":
                    MoveLastShape();
                    break;
                case "5":
                    Undo();
                    break;
                case "6":
                    Redo();
                    break;
                case "7":
                    Console.WriteLine("Enter name of file (to this string app will add .json");
                    string path = Console.ReadLine();
                    SaveShapesToJson(path + ".json");
                    break;
                case "8":
                    Console.WriteLine("Enter name of file (to this string app will add .json");
                    string path1 = Console.ReadLine();
                    LoadShapesFromJson(path1 + ".json");
                    break;
                case "9":
                    if (shapes.Count > 0)
                    {
                        DeleteShape();
                    }
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }

        private void DrawRectangle()
        {
            SaveState();
            try
            {
                Console.Write("Enter X position: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Enter Y position: ");
                int y = int.Parse(Console.ReadLine());
                Console.Write("Enter width: ");
                int width = int.Parse(Console.ReadLine());
                Console.Write("Enter height: ");
                int height = int.Parse(Console.ReadLine());
                Console.Write("Enter fill (true or false): ");
                bool filled = bool.Parse(Console.ReadLine());
                char fill = '*';
                if (filled)
                {
                    Console.Write("Enter fill symbol: ");
                    fill = char.Parse(Console.ReadLine());
                }
                shapes.Add(new Rectangle(x, y, width, height, filled,fill));
            }catch (Exception e) { }
        }

        public void DrawCircle()
        {
            SaveState();
            try
            {
                
                Console.Write("Enter X position: ");
                int cx = int.Parse(Console.ReadLine());
                Console.Write("Enter Y position: ");
                int cy = int.Parse(Console.ReadLine());
                Console.Write("Enter radius: ");
                int radius = int.Parse(Console.ReadLine());
                Console.Write("Enter fill (true or false): ");
                bool filled = bool.Parse(Console.ReadLine());
                char fill = '*';
                if (filled)
                {
                    Console.Write("Enter fill symbol ");
                    fill = char.Parse(Console.ReadLine());
                }
                shapes.Add(new Circle(cx, cy, radius,filled,fill));
            }
            catch (Exception e) { }
        }
        private void DrawTriangle()
        {
            SaveState();
            try
            {
                Console.Write("Enter X (top vertex): ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Enter Y (top vertex): ");
                int y = int.Parse(Console.ReadLine());

                Console.Write("Enter height: ");
                int height = int.Parse(Console.ReadLine());
                Console.Write("Enter base length: ");
                int baseLength = int.Parse(Console.ReadLine());
                Console.Write("Enter fill (true or false): ");
                bool filled = bool.Parse(Console.ReadLine());
                char fill = '*';
                if(filled)
                {
                    Console.Write("Enter fill symbol: ");
                    fill = char.Parse(Console.ReadLine());
                }
                shapes.Add(new Triangle(x, y, height, baseLength,filled,fill));
            }
            catch (Exception e) { }
        }

        private void DeleteShape()
        {
            if (shapes.Count == 0)
            {
                Console.WriteLine("No shapes to delete.");
                return;
            }

            SaveState();
            Console.WriteLine("List of shapes:");
            for (int i = 0; i < shapes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {shapes[i].GetType().Name} at ({shapes[i].X}, {shapes[i].Y})");
            }

            Console.Write("Enter the number of the shape to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= shapes.Count)
            {
                shapes.RemoveAt(index - 1);
                Console.WriteLine("Shape deleted.");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        private void MoveLastShape()
        {
            SaveState();
            try
            {
                if (shapes.Count > 0)
                {
                    Console.Write("Enter X position: ");
                    int dx = int.Parse(Console.ReadLine());
                    Console.Write("Enter Y position: ");
                    int dy = int.Parse(Console.ReadLine());
                    shapes[^1].Move(dx, dy);
                }
            }
            catch (Exception e) { }
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                var currentState = shapes.Select(shape => shape.Clone()).ToList();
                redoStack.Push(currentState);

                shapes = undoStack.Pop().Select(shape => shape.Clone()).ToList();
            }
            else
            {
                Console.WriteLine("Nothing to undo.");
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                var currentState = shapes.Select(shape => shape.Clone()).ToList();
                undoStack.Push(currentState);

                shapes = redoStack.Pop().Select(shape => shape.Clone()).ToList();
            }
            else
            {
                Console.WriteLine("Nothing to redo.");
            }
        }

        private void SaveShapesToJson(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new ShapeConverter() } 
            };

            string json = JsonSerializer.Serialize(shapes, options);
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Shapes saved to {filePath}.");
        }

        private void LoadShapesFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { new ShapeConverter() } 
                };

                string json = File.ReadAllText(filePath);
                shapes = JsonSerializer.Deserialize<List<IShape>>(json, options);
                Console.WriteLine($"Shapes loaded from {filePath}.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        public void SaveState()
        {
            var copiedShapes = shapes.Select(shape => shape.Clone()).ToList();
            undoStack.Push(copiedShapes);
            redoStack.Clear();
        }

        private void ClearCanvas()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    canvas[i, j] = ' ';
        }

        private void RenderCanvas()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Console.Write(canvas[i, j]);
                Console.WriteLine();
            }
        }
    }
}
