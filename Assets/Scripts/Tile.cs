namespace com.paintpuzzle
{
    using UnityEngine;

    [ExecuteInEditMode]
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileColor _currentColor;

        [SerializeField]
        private Material _material;

        void Update()
        {
            _material.color = TileColorUtility.GetColor(_currentColor);
        }

    }

}