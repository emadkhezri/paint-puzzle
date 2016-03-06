using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    private GameObject[,] puzzleBoard;
    private const float cellWidth = 0.65f;
    private Rect selectionRectTopLeftCornerCoords;
    private bool isCellSelectionActive = false;

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
    }

    private void createPuzzleBoard()
    {
        puzzleBoard = new GameObject[PuzzleWidth, PuzzleHeight];
        for (int x = 0; x < PuzzleWidth; ++x)
            for (int y = 0; y < PuzzleHeight; ++y)
            {
                puzzleBoard[x, y] = Instantiate<GameObject>(PuzzleCellPrefab);
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().Position = new Vector2(x, y);
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().SetGameManager(gameObject.GetComponent<GameManagerScript>());
                puzzleBoard[x, y].GetComponent<Transform>().position = new Vector3(
                    PuzzleTopLeftCornerCoords.x + x * cellWidth,
                    PuzzleTopLeftCornerCoords.y - y * cellWidth,
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

    }
    
    public void StartCellSelection(Vector2 position)
    {
        isCellSelectionActive = true;

        selectionRectTopLeftCornerCoords.xMin = position.x;
        selectionRectTopLeftCornerCoords.yMin = position.y;
        selectionRectTopLeftCornerCoords.xMax = position.x;
        selectionRectTopLeftCornerCoords.yMax = position.y;

        puzzleBoard[(int)position.x, (int)position.y].GetComponent<PuzzleCellScript>().AddHighlight(SelectedColor);
    }

    public void UpdateCellSelectionRect(Vector2 position)
    {
        if (!isCellSelectionActive)
        {
            print("Cell Selection is not active, no need to update rect.");
            return;
        }

        //remove old rect highlight
        for (int x = (int)selectionRectTopLeftCornerCoords.xMin; x <= selectionRectTopLeftCornerCoords.xMax; ++x)
            for (int y = (int)selectionRectTopLeftCornerCoords.yMin; y <= selectionRectTopLeftCornerCoords.yMax; ++y)
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().RemoveHighlight(SelectedColor);

        //update rect coords
        if (selectionRectTopLeftCornerCoords.xMin <= position.x)
            selectionRectTopLeftCornerCoords.xMax = position.x;
        else
        {
            selectionRectTopLeftCornerCoords.xMax = selectionRectTopLeftCornerCoords.xMin;
            selectionRectTopLeftCornerCoords.xMin = position.x;
        }
        if (selectionRectTopLeftCornerCoords.yMin <= position.y)
            selectionRectTopLeftCornerCoords.yMax = position.y;
        else
        {
            selectionRectTopLeftCornerCoords.yMax = selectionRectTopLeftCornerCoords.yMin;
            selectionRectTopLeftCornerCoords.yMin = position.y;
        }

        // add highlight to updated rect
        for(int x=(int)selectionRectTopLeftCornerCoords.xMin; x<=selectionRectTopLeftCornerCoords.xMax; ++x)
            for (int y = (int)selectionRectTopLeftCornerCoords.yMin; y <= selectionRectTopLeftCornerCoords.yMax; ++y)
                puzzleBoard[x, y].GetComponent<PuzzleCellScript>().AddHighlight(SelectedColor);
    }

    public void EndCellSelection()
    {
        isCellSelectionActive = false;

        for (int col = (int)selectionRectTopLeftCornerCoords.xMin; col <= selectionRectTopLeftCornerCoords.xMax; ++col)
            for (int row = (int)selectionRectTopLeftCornerCoords.yMin; row <= selectionRectTopLeftCornerCoords.yMax; ++row)
            {
                puzzleBoard[col, row].GetComponent<PuzzleCellScript>().RemoveHighlight(SelectedColor);
                puzzleBoard[col, row].GetComponent<PuzzleCellScript>().AddColor(SelectedColor);
            }
    }
}
