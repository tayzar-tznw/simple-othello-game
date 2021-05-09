using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    private CommandInvoker _commandInvoker;
    [SerializeField] private Button Undo;
    [SerializeField] private CellContainer cellContainer;

    private void Awake()
    {
    }

    void Start()
    {
        cellContainer.OnCellClicked = OnCellClicked;
        // _commandInvoker = new CommandInvoker();

        // Undo.onClick.AddListener(OnUndoClicked);
    }

    private void OnCellClicked(Cell cell)
    {
        Debug.Log("test");
        // BUGGY
        // ICommand command = new PlaceKomaCommand(cell, _cellContainer.GetCells(), Cell.KOMA.Black);
        // _commandInvoker.AddCommand(command);
        // this.Execute();
    }

    private void OnUndoClicked()
    {
        Debug.Log("Undo clicked");
        // _commandInvoker.UndoCommand();
        // Execute();
    }


    private void Execute()
    {
        // _cellContainer.ResetCells();
        _commandInvoker.Execute();
    }
}