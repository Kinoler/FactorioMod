
using System.Dynamic;

namespace FactorioMod.Factorio.Models
{
    public class Pipe
    {
        public Point Location { get; set; }
        public Orientation Orientation { get; set; }
        public Direction Direction { get; set; }
        public Rotation Rotation { get; set; }
        public Pipe NextPipe { get; set; }

        public Pipe(Point location, Orientation orientation, Direction direction)
        {
            Orientation = orientation;
            Direction = direction;
            Location = location;
        }

    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x = default, int y = default)
        {
            X = x;
            Y = y;
        }

        public Point Offset(int x, int y)
        {
            return new Point(X + x, Y + y);
        }
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public enum Direction
    {
        RightOrUp,
        LeftOrDown
    }

    public enum Rotation
    {
        None,
        Left,
        Right
    }

    public enum DirectionOrientation
    {
        Up,
        Down,
        Right,
        Left
    }

}