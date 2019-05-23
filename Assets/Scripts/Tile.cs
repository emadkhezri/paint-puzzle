
namespace com.paintpuzzle
{
    using UnityEngine;

    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileColor _currentColor;
        private Material _material;
        public BoxCollider Collider;

        private void Awake() {
            _material = GetComponent<MeshRenderer>().material;
            Collider = GetComponent<BoxCollider>();
            Collider.isTrigger = true;
        }

        public void Mix(TileColor color)
        {
            _currentColor = TileColorUtility.MixColor(_currentColor,color);
            _material.color = TileColorUtility.GetColor(_currentColor);
        }

        private void OnTriggerEnter(Collider other) {
                if (other.gameObject.layer==8)
                {
                    Mix(TileColor.Blue);   
                }
        }
    }
}