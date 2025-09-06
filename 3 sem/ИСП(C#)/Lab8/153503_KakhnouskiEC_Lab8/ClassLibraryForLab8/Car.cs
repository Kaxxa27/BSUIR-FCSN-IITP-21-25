using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForLab8
{
    public class Car
    {
        public int CarID { get; set; }
        public string? CarName { get; set; }

        public bool EngineValueThanTwoLitrs { get; set; }


        public Car(int CarID, string? CarName, bool EngineValueThanTwoLitrs = false)
        {
            this.CarID = CarID;
            this.CarName = CarName;
            this.EngineValueThanTwoLitrs = EngineValueThanTwoLitrs;

        }
    }
}
