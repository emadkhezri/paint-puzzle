using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.paintpuzzle
{
    public class ColorChooser : MonoBehaviour
    {
        [SerializeField]
        private Tile _tile;
        [SerializeField]
        private TileBoard _tileBoard;

        private void OnMouseEnter() {
            _tileBoard.SelectedColor = _tile.CurrentColor;
        }
    }    
}
