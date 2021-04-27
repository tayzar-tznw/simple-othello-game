using System;

public class Neighbours
{
    private (int, int) North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest;

    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    public Neighbours(int x, int y)
    {
        West = (x, (y - 1));
        North = ((x - 1), y);
        East = (x, (y + 1));
        South = ((x + 1), y);
        NorthEast = ((x - 1), (y + 1));
        NorthWest = ((x - 1), (y - 1));
        SouthEast = ((x + 1), (y + 1));
        SouthWest = ((x + 1), (y - 1));
    }

    public (int, int) GetNeighbour(Direction direction)
    {
        return direction switch
        {
            Direction.North => North,
            Direction.East => East,
            Direction.West => West,
            Direction.South => South,
            Direction.NorthEast => NorthEast,
            Direction.NorthWest => NorthWest,
            Direction.SouthEast => SouthEast,
            Direction.SouthWest => SouthWest,
        };
    }
}