using System;

public class Neighbours
{
    int North { get; set; }
    int East { get; set; }
    int West { get; set; }
    int South { get; set; }

    private Neighbours()
    {
    }

    public void SetNeighbours(int x, int y)
    {
        if (x - 1 > 0)
        {
            West = (x - 1);
        }

        if (y - 1 > 0)
        {
            North = (y - 1);
        }

        if (x  < 8)
        {
            East = (x + 1);
        }

        if (y < 8)
        {
            South = (y + 1);
        }
    }
}