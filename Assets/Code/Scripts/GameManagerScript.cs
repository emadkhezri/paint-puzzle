using UnityEngine;
using System.Collections;
using Assets.Code.Classes.Command;

public class GameManagerScript : MonoBehaviour
{
    private GameObject[,] puzzleBoard;
    private const float cellWidth = 0.65f;
    private Vector2 selectionStartCoords;
    private Vector2 selectionEndCoords;
    private Rect selectionRect;
    private bool isCellSelectionActive = false;
    private CommandHistory commandHistory = new CommandHistory();

    public Color SelectedColor;

    public GameObject PaletteCellPrefab;
    public GameObject PuzzleCellPrefab;

    public Vector3 PaletteTopLeftCornerCoords;
    public Vector3 PuzzleTopLeftCornerCoords;
    public int PuzzleWidth;
    public int PuzzleHeight;

    // Use this for initialization
    void Start()
    {
        print("Game Manager started.");

        SelectedColor = Color.red;

        GameObject[] palette = createPalette();        
        palette[0].GetComponent<SpriteRenderer>().color = Color.red;
        palette[1].GetComponent<SpriteRenderer>().color = Color.green;
        palette[2].GetComponent<SpriteRenderer>().color = Color.blue;

        createPuzzleBoard();
        movePuzzleBoardIntoView();
    }

    private void movePuzzleBoardIntoView()
    {
        for (int x = 0; x < PuzzleWidth; ++x)
            for (int y = 0; y < PuzzleHeight; ++y)
            {
                Vector3 position = puzzleBoard[x, y].transform.position;
                position.y -= 10;
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().StartMoveToDestination(
                    position, 1f - Random.Range(0, 2) * 0.4f);
            }
    }

    private void createPuzzleBoard()
    {
        puzzleBoard = new GameObject[PuzzleWidth, PuzzleHeight];
        for (int x = 0; x < PuzzleWidth; ++x)
            for (int y = 0; y < PuzzleHeight; ++y)
            {
                puzzleBoard[x, y] = Instantiate<GameObject>(PuzzleCellPrefab);
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().PositionInBoard = new Vector2(x, y);
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().SetGameManager(gameObject.GetComponent<GameManagerScript>());
                puzzleBoard[x, y].GetComponent<Transform>().position = new Vector3(
                    PuzzleTopLeftCornerCoords.x + x * cellWidth,
                    PuzzleTopLeftCornerCoords.y + 10 - y * cellWidth,
                    0);
            }
    }

    private GameObject[] createPalette()
    {
        GameObject[] palette = new GameObject[3];

        for (int i = 0; i < 3; ++i)
        {
            palette[i] = Instantiate<GameObject>(PaletteCellPrefab);
            palette[i].GetComponent<PaletteCellScript>().SetGameManager(gameObject.GetComponent<GameManagerScript>());
            palette[i].GetComponent<Transform>().position = new Vector3(
                PaletteTopLeftCornerCoords.x,
                PaletteTopLeftCornerCoords.y - i * cellWidth,
                0);
        }
        return palette;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            commandHistory.Undo();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            commandHistory.Redo();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            puzzleBoard[0, 0].AddComponent<ShakeObjectScript>();
        }
    }
    
    public void StartCellSelection(Vector2 position)
    {
        isCellSelectionActive = true;

        selectionStartCoords = position;
        selectionEndCoords = position;

        selectionRect.xMin = position.x;
        selectionRect.yMin = position.y;
        selectionRect.xMax = position.x;
        selectionRect.yMax = position.y;

        puzzleBoard[(int)position.x, (int)position.y].GetComponent<PuzzleCellScript>().AddHighlight(SelectedColor);
    }

    public void UpdateCellSelectionRect(Vector2 position)
    {
        if (!isCellSelectionActive)
        {
            //print("Cell Selection is not active, no need to update rect.");
            return;
        }
        if (selectionEndCoords == position)
        {
            //print("Selection did not change no need to updated.");
            return;
        }

        //remove old rect highlight
        for (int x = (int)selectionRect.xMin; x <= selectionRect.xMax; ++x)
            for (int y = (int)selectionRect.yMin; y <= selectionRect.yMax; ++y)
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().RemoveHighlight();

        //update rect coords
        selectionEndCoords = position;
        selectionRect.xMin = Mathf.Min(selectionStartCoords.x, selectionEndCoords.x);
        selectionRect.yMin = Mathf.Min(selectionStartCoords.y, selectionEndCoords.y);
        selectionRect.xMax = Mathf.Max(selectionStartCoords.x, selectionEndCoords.x);
        selectionRect.yMax = Mathf.Max(selectionStartCoords.y, selectionEndCoords.y);

        // add highlight to updated rect
        for(int x=(int)selectionRect.xMin; x<=selectionRect.xMax; ++x)
            for (int y = (int)selectionRect.yMin; y <= selectionRect.yMax; ++y)
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().AddHighlight(SelectedColor);
    }

    public void EndCellSelection()
    {
        isCellSelectionActive = false;

        for (int col = (int)selectionRect.xMin; col <= selectionRect.xMax; ++col)
            for (int row = (int)selectionRect.yMin; row <= selectionRect.yMax; ++row)
                puzzleBoard[col, row].GetComponent<PuzzleCellScript>().RemoveHighlight();

        AddColorCommandParams commandParameters = new AddColorCommandParams
        {
            PuzzleBoard = puzzleBoard,
            SelectedColor = SelectedColor,
            SelectionRect = selectionRect
        };
        AddColorCommand command = new AddColorCommand(commandParameters);
        command.Execute();
        commandHistory.Add(command);
    }
}
