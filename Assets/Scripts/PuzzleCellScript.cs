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
            gameManager.StartCellSelection(PositionInBoard);
        }

        void OnMouseEnter()
        {
            gameManager.UpdateCellSelectionRect(PositionInBoard);
        }

        void OnMouseUp()
        {
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
            actualColor = MixColor(oldColor, color);
            gameObject.GetComponent<SpriteRenderer>().color = ActualColor;
        }

        public void AddHighlight(Color color)
        {
            if (isHighlighted)
                return;

            Color newColor = MixColor(
                gameObject.GetComponent<SpriteRenderer>().color,
                color);
            gameObject.GetComponent<SpriteRenderer>().color = newColor;

            isHighlighted = true;
        }

        public void RemoveHighlight()
        {
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