
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

        private void Update() {
            _material.color = TileColorUtility.GetColor(_currentColor);
        }
    }
}