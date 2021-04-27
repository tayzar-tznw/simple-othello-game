using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class CellContainer : MonoBehaviour
{
    //Default start turn

    Cell.KOMA CurrentTurn = Cell.KOMA.Black;

    // private List<Cell> Cells = new List<Cell>();

    Cell[,] Cells = new Cell[9, 9];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                var gameObject = Utils.InstantiatePrefab("Prefabs/Cell", this.transform);
                Cell cell = gameObject.GetComponent<Cell>();
                cell.SetIndex(i, j);
                Cells[i, j] = cell;
            }
        }

        Cells[5, 5].SetKoma(Cell.KOMA.Black);
        Cells[4, 5].SetKoma(Cell.KOMA.White);
        Cells[4, 6].SetKoma(Cell.KOMA.Black);
        Cells[5, 6].SetKoma(Cell.KOMA.White);
        FindValidCells();
    }

    void ResetBlueKoma()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (Cells[i, j].GetColor() == Color.blue && Cells[i, j].IsOccupied())
                {
                    Cells[i, j].SetKoma(Cell.KOMA.None);
                }
            }
        }
    }

    public void OnCellClicked(Cell cell)
    {
        if (cell.GetColor() != Color.blue)
        {
            return;
        }

        cell.SetKoma(this.CurrentTurn);
        this.ResetBlueKoma();
        
        
        foreach (Neighbours.Direction Value in Enum.GetValues(typeof(Neighbours.Direction)))
        {
           this.FlipCell(cell,Value);
        }
        
        this.CurrentTurn = ~Cell.KOMA.All ^ ~this.CurrentTurn;
        FindValidCells();
    }

    void FindValidCells()
    {
        Color currentColor = (CurrentTurn == Cell.KOMA.Black) ? Color.black : Color.white;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (Cells[i, j].GetColor() == currentColor && Cells[i, j].IsOccupied())
                {
                    Debug.Log($"{i},{j}");
                    foreach (Neighbours.Direction Value in Enum.GetValues(typeof(Neighbours.Direction)))
                    {
                        CheckCell(Cells[i, j], Value);
                    }
                }
            }
        }
    }

    void FlipCell(Cell cell, Neighbours.Direction direction)
    {
        int x = cell.GetNeighbour(direction).Item1;
        int y = cell.GetNeighbour(direction).Item2;
        Color currentColor = cell.GetColor();
        Boolean check = false;
        List<Cell> Temp = new List<Cell>();

        while (x > 0 && y > 0 && x < 9 && y < 9)
        {
            Cell nextCell = Cells[x, y];
            Debug.Log($"direction : {direction} index : {cell.GetIndex()} next : ({x},{y})");
            Color nextCellColor = nextCell.GetColor();
            if (nextCell.IsOccupied())
            {
                check = (nextCellColor != currentColor && nextCellColor != Color.blue);

                if (check)
                {
                    Temp.Add(nextCell);
                }
            }

            if (nextCell.IsOccupied() && nextCellColor == currentColor)
            {
                foreach (var item in Temp)
                {
                    if (currentColor == Color.black)
                    {
                        item.SetKoma(Cell.KOMA.Black);
                    }
                    else
                    {
                        item.SetKoma(Cell.KOMA.White);
                    }
                }
                break;
            }

            cell = nextCell;
            x = cell.GetNeighbour(direction).Item1;
            y = cell.GetNeighbour(direction).Item2;
        }
    }

    void CheckCell(Cell cell, Neighbours.Direction direction)
    {
        int x = cell.GetNeighbour(direction).Item1;
        int y = cell.GetNeighbour(direction).Item2;
        Color currentColor = cell.GetColor();
        Boolean check = false;

        while (x > 0 && y > 0 && x < 9 && y < 9)
        {
            Cell nextCell = Cells[x, y];
            Color nextCellColor = nextCell.GetColor();
            if (nextCell.IsOccupied())
            {
                check = (nextCellColor != currentColor && nextCellColor != Color.blue);
            }

            if (!nextCell.IsOccupied())
            {
                if (check)
                {
                    nextCell.SetKoma(Cell.KOMA.Blue);
                }
                break;
            }

            cell = nextCell;
            x = cell.GetNeighbour(direction).Item1;
            y = cell.GetNeighbour(direction).Item2;
        }
    }
}