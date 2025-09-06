using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileServiceLibrary
{
    class FileServiceFromLib<T> : IFileService<T> where T : class
    {
        #region IFileService<T>
        public IEnumerable<T> ReadFile(string fileName)
        {
            string js = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<T>>(js) ?? new List<T>();
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                JsonSerializer.SerializeAsync(fs, data, options);
                Console.WriteLine("JSON_FILE READY!");
            }
        }
        #endregion
    }
}
