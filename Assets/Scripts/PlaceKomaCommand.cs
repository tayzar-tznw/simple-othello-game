using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaceKomaCommand : ICommand
{
    private Cell cell;
    private Cell[,] Cells;
    private Cell.KOMA CurrentTurn;

    public PlaceKomaCommand(Cell cell, Cell[,] Cells, Cell.KOMA CurrentTurn)
    {
        this.cell = cell;
        this.CurrentTurn = CurrentTurn;
        this.Cells = Cells;
    }

    public void Execute()
    {
        cell.SetKoma(CurrentTurn);
        foreach (Neighbours.Direction value in Enum.GetValues(typeof(Neighbours.Direction)))
        {
            FlipCell(cell, value);
        }
    }

    Cell.KOMA GetOpponent(Cell.KOMA currentKoma)
    {
        return ~Cell.KOMA.All ^ ~currentKoma;
    }

    void FlipCell(Cell cell, Neighbours.Direction direction)
    {
        var (x, y) = cell.GetNeighbour(direction);
        Boolean found = false;
        List<Cell> FlipList = new List<Cell>();

        while (x > 0 && y > 0 && x < 9 && y < 9)
        {
            Cell nextCell = Cells[x, y];
            var nextCellColor = nextCell.GetKomaType();


            if (!nextCell.IsOccupied())
            {
                return;
            }
            else if (nextCellColor == GetOpponent(CurrentTurn))
            {
                FlipList.Add(nextCell);
                found = true;
            }
            else if (found && nextCellColor == CurrentTurn)
            {
                foreach (var item in FlipList)
                {
                    item.SetKoma(CurrentTurn);
                }

                return;
            }

            cell = nextCell;
            (x, y) = cell.GetNeighbour(direction);
        }
    }
}