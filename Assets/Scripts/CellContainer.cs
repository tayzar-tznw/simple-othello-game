using System;
using UnityEngine;

public class CellContainer : MonoBehaviour
{
    Cell[,] Cells = new Cell[9, 9];
    Cell.KOMA CurrentTurn = Cell.KOMA.Black;
    public Action<Cell> OnCellClicked { get; set; }

    private void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Debug.Log("TEST");
                var gameObject = Utils.InstantiatePrefab("Prefabs/Cell", transform);
                Cell cell = gameObject.GetComponent<Cell>();
                cell.SetIndex(i, j);
                Cells[i, j] = cell;
            }
        }
        // Cell[] InitialCells = {Cells[5, 5], Cells[4, 5], Cells[4, 6], Cells[5, 6]};

        // 最初の4つを設定
    }

    public Cell[,] GetCells()
    {
        return Cells;
    }

    Cell.KOMA GetOpponent(Cell.KOMA currentKoma)
    {
        return ~Cell.KOMA.All ^ ~currentKoma;
    }
}