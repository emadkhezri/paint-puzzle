
namespace com.paintpuzzle
{
    using UnityEngine;

    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileColor _currentColor;

        private Material _material;

        private void Awake() {
            _material = GetComponent<MeshRenderer>().material;
        }

        public void Mix(TileColor color)
        {
            _currentColor = TileColorUtility.MixColor(_currentColor,color);
            _material.color = TileColorUtility.GetColor(_currentColor);
        }

        private void OnMouseDown() {
            Mix(TileColor.Blue);
        }
    }
}