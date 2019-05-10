using UnityEngine;

namespace com.paintpuzzle
{
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private TileColor _selectedColor;

        [SerializeField]
        private TileBoard _tileBoard;

        private void OnMouseDown() {
            var selectedPosition = Input.mousePosition;
            selectedPosition.z = 10f;
            selectedPosition = Camera.main.ScreenToWorldPoint(selectedPosition);
            _tileBoard.Paint(_selectedColor,selectedPosition);
        }
    }

}