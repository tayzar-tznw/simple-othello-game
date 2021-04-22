using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CellContainer : MonoBehaviour
{
    //Default start turn
    Cell.KOMA CurrentTurn = Cell.KOMA.Black;

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
            }
        }
    }

    private Tuple<int, int>[] AvailableCells;

    // Update is called once per frame
    void Update()
    {
    }

    public void OnCellClicked(Cell cell)
    {
        Debug.Log(cell.GetIndex());
        if (cell.IsOccupied()) return;

        cell.SetKoma(this.CurrentTurn);
        this.CurrentTurn = ~Cell.KOMA.All ^ ~this.CurrentTurn;
        
        for (int i = 0; i < AvailableCells.Length; i++)
        {
            Debug.Log(AvailableCells[i]);
        }
    }

    void FindValidCell()
    {
        
    }
}