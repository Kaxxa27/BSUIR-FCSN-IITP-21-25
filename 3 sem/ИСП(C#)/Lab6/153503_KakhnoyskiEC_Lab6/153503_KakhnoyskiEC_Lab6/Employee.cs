namespace _153503_KakhnoyskiEC_Lab6
{
    class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool isWorking { get; set; }

        public Employee(string name = "Tomas", int age = 777, bool isWorking = false)
        {
            Name = name;
            Age = age;
            this.isWorking = isWorking;
        }
    }
}

