using System;
using UnityEngine;

public class CellContainer : MonoBehaviour
{
    Cell[,] Cells = new Cell[9, 9];
    public Action<Cell> OnCellClicked { get; set; }

    private void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                var gameObject = Utils.InstantiatePrefab("Prefabs/Cell", transform);
                Cell cell = gameObject.GetComponent<Cell>();
                cell.SetIndex(i, j);
                Cells[i, j] = cell;
            }
        }

        Cells[5, 5].SetKoma(Cell.KOMA.White);
        Cells[4, 5].SetKoma(Cell.KOMA.Black);
        Cells[4, 6].SetKoma(Cell.KOMA.White);
        Cells[5, 6].SetKoma(Cell.KOMA.Black);
        // 最初の4つを設定
    }

    public Cell[,] GetCells()
    {
        return Cells;
    }
}