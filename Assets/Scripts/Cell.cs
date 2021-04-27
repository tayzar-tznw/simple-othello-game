using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class Cell : MonoBehaviour
{
    
    public enum KOMA
    {
        None,
        Black,
        White,
        Blue,
        All = Black | White,
    }

    [SerializeField] GameObject Koma;

    private Neighbours _neighbours;

    private int _x;
    private int _y;
    
    public void SetIndex(int x, int y)
    {
        _x = x;
        _y = y;
        _neighbours = new Neighbours(x, y);
    }

    public (int, int) GetIndex()
    {
        return (_x, _y);
    }

    public (int, int) GetNeighbour(Neighbours.Direction direction)
    {
        return _neighbours.GetNeighbour(direction);
    }

    public Color GetColor()
    {
        return Koma.GetComponent<Image>().color;
    }

    public Boolean IsOccupied()
    {
        return Koma.activeSelf;
    }

    public void SetKoma(KOMA koma)
    {
        switch (koma)
        {
            case KOMA.None:
                Koma.SetActive(false);
                break;
            case KOMA.Black:
                Koma.SetActive(true);
                Koma.GetComponent<Image>().color = Color.black;
                break;
            case KOMA.White:
                Koma.SetActive(true);
                Koma.GetComponent<Image>().color = Color.white;
                break;
            case KOMA.Blue:
                Koma.SetActive(true);
                Koma.GetComponent<Image>().color = Color.blue;
                break;
        }
    }

    public void OnCellClicked()
    {
        this.GetComponentInParent<CellContainer>().OnCellClicked(this);
    }
}