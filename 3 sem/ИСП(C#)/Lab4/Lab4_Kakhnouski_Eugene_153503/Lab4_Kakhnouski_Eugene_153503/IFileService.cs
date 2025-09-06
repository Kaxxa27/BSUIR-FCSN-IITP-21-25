using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Kakhnouski_Eugene_153503
{
    interface IFileService
    {
        IEnumerable<Product> ReadFile(string filename);
        void SaveData(IEnumerable<Product> data, string filename);
    }
}
