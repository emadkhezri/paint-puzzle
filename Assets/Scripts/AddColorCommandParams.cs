using UnityEngine;

namespace com.paintpuzzle
{
    class AddColorCommandParams
    {
        public GameObject[,] PuzzleBoard { get; set; }
        public Rect SelectionRect { get; set; }
        public Color SelectedColor { get; set; }
    }

}
