namespace Matter.Controller.Server
{
    public class MatterNodeModel
    {
        public string Id { get; set; }

        public Position Position { get; set; }

        public Data Data { get; set; }
    }

    public class Position
    {
        public int X { get; set; }

        public int Y { get; set; }
    }

    public class Data
    {
        public string Label { get; set; }
    }
}
