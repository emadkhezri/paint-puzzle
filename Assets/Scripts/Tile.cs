
namespace com.paintpuzzle
{
    using UnityEngine;

    public class Tile : MonoBehaviour
    {
        [SerializeField]
        public TileColor CurrentColor;
        [SerializeField]
        public TileBoard TileBoard;
        private Material _material;
        public BoxCollider Collider;

        private void Awake() {
            _material = GetComponent<MeshRenderer>().material;
            Collider = GetComponent<BoxCollider>();
            Collider.isTrigger = true;
            Mix(CurrentColor);
        }

        public void Mix(TileColor color)
        {
            CurrentColor = TileColorUtility.MixColor(CurrentColor,color);
            _material.color = TileColorUtility.GetColor(CurrentColor);
        }

        private void OnTriggerEnter(Collider other) {
                if (other.gameObject.layer==8)
                {
                    Mix(TileBoard.SelectedColor);   
                }
        }
    }
}