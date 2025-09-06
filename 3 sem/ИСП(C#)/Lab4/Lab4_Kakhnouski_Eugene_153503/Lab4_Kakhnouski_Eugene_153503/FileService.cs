using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Lab4_Kakhnouski_Eugene_153503
{
    class FileService : IFileService
    {
        BinaryReader Reader;
        BinaryWriter Writer;
        public IEnumerable<Product> ReadFile(string filename)
        {
            bool Exists = true;
            try
            {
                if (!File.Exists(filename)) throw new Exception("Файл отсутствует!");              
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Exists = false;
            }
            if(Exists)
            {
                using (Reader = new BinaryReader(File.Open(filename, FileMode.Open)))
                {
                    while (Reader.PeekChar() > -1)
                    {
                        Thread.Sleep(300);
                        string Name = Reader.ReadString();
                        int Price = Reader.ReadInt32();
                        bool isBroken = Reader.ReadBoolean();
                        yield return new Product(Name, Price, isBroken);
                    }
                }
            } 
        }

        public void SaveData(IEnumerable<Product> data, string filename)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();      
            }
            using (Writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                foreach (var item in data)
                {
                    Writer.Write(item.Name);
                    Writer.Write(item.Price);
                    Writer.Write(item.isBroken);
                }                            
            }
        }
    }
}
