namespace A_3
{
    public class Tile
    {
        public int Id { get; set; }
        public Directions Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Direction)}: {Direction}, {nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }
}