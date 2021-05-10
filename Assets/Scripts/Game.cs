using UnityEngine;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    private readonly CommandInvoker _commandInvoker = new CommandInvoker();
    [SerializeField] private Button Undo;
    [SerializeField] private CellContainer cellContainer;
    Cell.KOMA CurrentTurn = Cell.KOMA.Black;
    private Cell[,] Cells;

    void Start()
    {
        cellContainer.OnCellClicked = OnCellClicked;
        Cells = cellContainer.GetCells();
        Undo.onClick.AddListener(OnUndoClicked);
        FindValidCells();
    }

    private void OnCellClicked(Cell cell)
    {
        if (cell.GetKomaType() != Cell.KOMA.Blue)
        {
            return;
        }

        ICommand command = new PlaceKomaCommand(cell, cellContainer.GetCells(), CurrentTurn);
        _commandInvoker.AddCommand(command);
        Execute();
    }

    Cell.KOMA GetOpponent(Cell.KOMA currentKoma)
    {
        return ~Cell.KOMA.All ^ ~currentKoma;
    }

    private void OnUndoClicked()
    {
        if (_commandInvoker.TryUndoCommand())
        {
            Execute();
        }
    }

    private void Execute()
    {
        foreach (var cell in Cells)
        {
            cell.SetKoma(Cell.KOMA.None);
        }

        Cells[5, 5].SetKoma(Cell.KOMA.White);
        Cells[4, 5].SetKoma(Cell.KOMA.Black);
        Cells[4, 6].SetKoma(Cell.KOMA.White);
        Cells[5, 6].SetKoma(Cell.KOMA.Black);

        _commandInvoker.Execute();
        CurrentTurn = GetOpponent(CurrentTurn);
        FindValidCells();
    }


    void FindValidCells()
    {
        foreach (var cell in Cells)
        {
            if (cell.GetKomaType() == CurrentTurn)
            {
                foreach (Neighbours.Direction Value in Enum.GetValues(typeof(Neighbours.Direction)))
                {
                    CheckCell(cell, Value);
                }
            }
        }
    }

    void CheckCell(Cell cell, Neighbours.Direction direction)
    {
        var (x, y) = cell.GetNeighbour(direction);
        var currentColor = cell.GetKomaType();
        Boolean found = false;

        while (x > 0 && y > 0 && x < 9 && y < 9)
        {
            Cell nextCell = Cells[x, y];
            var nextCellColor = nextCell.GetKomaType();
            if (nextCell.IsOccupied())
            {
                found = (nextCellColor == GetOpponent(currentColor));
            }
            else if (found)
            {
                nextCell.SetKoma(Cell.KOMA.Blue);
                break;
            }

            cell = nextCell;
            x = cell.GetNeighbour(direction).Item1;
            y = cell.GetNeighbour(direction).Item2;
        }
    }
}