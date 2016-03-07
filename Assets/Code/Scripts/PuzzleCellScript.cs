using UnityEngine;
using System.Collections;
using System;

public class PuzzleCellScript : MonoBehaviour
{
    private GameManagerScript gameManager;
    private bool isHighlighted = false;
    private Color actualColor = Color.white;

    public Color ActualColor
    {
        get { return actualColor; }

        set
        {
            actualColor = value;
            gameObject.GetComponent<SpriteRenderer>().color = actualColor;

        }
    }

    public Vector2 Position
    {
        private get;
        set;
    }

    private Color mixColor(Color backColor, Color foreColor)
    {
        if (backColor == Color.white)
            return foreColor;

        return new Color(
               Math.Max(backColor.r, foreColor.r),
               Math.Max(backColor.g, foreColor.g),
               Math.Max(backColor.b, foreColor.b));
    }

    private Color takeColor(Color backColor, Color foreColor)
    {
        if (backColor == foreColor)
            return Color.white;

        return new Color(
               backColor.r - foreColor.r,
               backColor.g - foreColor.g,
               backColor.b - foreColor.b);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        //print(string.Format("Cell[{0}, {1}] received mouse down.", Position.x, Position.y));
        gameManager.StartCellSelection(Position);
    }

    void OnMouseEnter()
    {
        //print(string.Format("Cell[{0}, {1}] received mouse enter.", Position.x, Position.y));
        gameManager.UpdateCellSelectionRect(Position);
    }

    void OnMouseUp()
    {
        //print(string.Format("[{0}, {1}]: mouse up.", Position.x, Position.y));
        gameManager.EndCellSelection();
    }

    public void SetGameManager(GameManagerScript manager)
    {
        gameManager = manager;
    }

    public void AddColor(Color color)
    {
        Color oldColor = gameObject.GetComponent<SpriteRenderer>().color;

        //print(string.Format("[{0}, {1}]: Adding color. oldColor= {0}, color= {1}.", oldColor, color));
        actualColor = mixColor(oldColor, color);
        gameObject.GetComponent<SpriteRenderer>().color = ActualColor;
    }

    public void AddHighlight(Color color)
    {
        if (isHighlighted)
            return;

        //print(string.Format("[{0}, {1}]: Adding hightlight. color= {2}", Position.x, Position.y, color));
        Color newColor = mixColor(
            gameObject.GetComponent<SpriteRenderer>().color,
            color);
        gameObject.GetComponent<SpriteRenderer>().color = newColor;

        isHighlighted = true;
    }

    public void RemoveHighlight()
    {
        //print(string.Format("[{0}, {1}]: Removing hightlight. color= {2}", Position.x, Position.y, color));
        //print(string.Format("[{0}, {1}]: {2} - {3} = {4}", Position.x, Position.y,
        //    color, gameObject.GetComponent<SpriteRenderer>().color, actualColor));
        gameObject.GetComponent<SpriteRenderer>().color = ActualColor;

        isHighlighted = false;
    }
}
