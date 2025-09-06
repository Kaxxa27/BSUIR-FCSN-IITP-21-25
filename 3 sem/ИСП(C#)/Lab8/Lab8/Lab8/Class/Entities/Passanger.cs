namespace Class.Entities {
    public class Passanger {
        public int ID { get; init; }
        public string Name { get; set; } = "";
        public bool WithBaggage { get; set; }

        public Passanger(int id, string name, bool withBaggage) {
            ID = id;
            Name = name;
            WithBaggage = withBaggage;
        }
    }
}
