using UnityEngine;
using System;

namespace com.paintpuzzle
{
    public class PuzzleCellScript : MonoBehaviour
    {
        #region Private Fields
        private GameManagerScript gameManager;

        private bool isHighlighted = false;
        private Color actualColor = Color.white;

        private bool isMoving = false;
        private float moveStartTime;
        private float moveDuration;
        private Vector3 moveStartPostion;
        private Vector3 moveDestinationPosition;
        #endregion

        #region Public Properties
        public Color ActualColor
        {
            get { return actualColor; }

            set
            {
                actualColor = value;
                gameObject.GetComponent<SpriteRenderer>().color = actualColor;

            }
        }

        public Vector2 PositionInBoard
        {
            private get;
            set;
        }
        #endregion

        #region Private Methods
        private Color MixColor(Color backColor, Color foreColor)
        {
            if (backColor == Color.white)
                return foreColor;

            return new Color(
                   Math.Max(backColor.r, foreColor.r),
                   Math.Max(backColor.g, foreColor.g),
                   Math.Max(backColor.b, foreColor.b));
        }

        private Color TakeColor(Color backColor, Color foreColor)
        {
            if (backColor == foreColor)
                return Color.white;

            return new Color(
                   backColor.r - foreColor.r,
                   backColor.g - foreColor.g,
                   backColor.b - foreColor.b);
        }
        #endregion

        #region MonoBehaviour Methods
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isMoving)
            {
                float progress = (Time.time - moveStartTime) / moveDuration;
                gameObject.transform.position = Vector3.Lerp(moveStartPostion, moveDestinationPosition, progress);
                if (progress > 1)
                {
                    isMoving = false;
                }
            }
        }

        void OnMouseDown()
        {
            //print(string.Format("Cell[{0}, {1}] received mouse down.", Position.x, Position.y));
            gameManager.StartCellSelection(PositionInBoard);
        }

        void OnMouseEnter()
        {
            //print(string.Format("Cell[{0}, {1}] received mouse enter.", Position.x, Position.y));
            gameManager.UpdateCellSelectionRect(PositionInBoard);
        }

        void OnMouseUp()
        {
            //print(string.Format("[{0}, {1}]: mouse up.", Position.x, Position.y));
            gameManager.EndCellSelection();
        }
        #endregion

        #region Public Methods
        public void SetGameManager(GameManagerScript manager)
        {
            gameManager = manager;
        }

        public void AddColor(Color color)
        {
            Color oldColor = gameObject.GetComponent<SpriteRenderer>().color;

            //print(string.Format("[{0}, {1}]: Adding color. oldColor= {0}, color= {1}.", oldColor, color));
            actualColor = MixColor(oldColor, color);
            gameObject.GetComponent<SpriteRenderer>().color = ActualColor;
        }

        public void AddHighlight(Color color)
        {
            if (isHighlighted)
                return;

            //print(string.Format("[{0}, {1}]: Adding hightlight. color= {2}", Position.x, Position.y, color));
            Color newColor = MixColor(
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

        public void StartMoveToDestination(Vector3 destination, float duration)
        {
            moveStartPostion = gameObject.transform.position;
            moveDestinationPosition = destination;
            moveDuration = duration;
            moveStartTime = Time.time;
            isMoving = true;
        }
        #endregion

    }

}