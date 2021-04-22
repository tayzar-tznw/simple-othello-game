using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class Cell : MonoBehaviour
{
    [Flags]
    public enum KOMA
    {
        None,
        Black,
        White,
        Blue,
        All = Black | White,
    }

    [SerializeField] GameObject Koma;

    private Neighbours n;

    private int _x;
    private int _y;

    // Start is called before the first frame update
    void Start()
    {
        Koma.SetActive(false);
    }

    public void SetIndex(int x, int y)
    {
        _x = x;
        _y = y;
        n.SetNeighbours(x, y);
    }

   
    
    public Tuple<int, int> GetIndex()
    {
        return Tuple.Create(_x, _y);
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log("OnCellClicked");
        this.GetComponentInParent<CellContainer>().OnCellClicked(this);
    }
}