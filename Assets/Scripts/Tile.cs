
namespace com.paintpuzzle
{
    using UnityEngine;

    public class Tile : MonoBehaviour
    {
        [SerializeField]
        public TileColor Color;
        [SerializeField]
        public TileBoard TileBoard;
        private Material _material;
        public BoxCollider Collider;

        private void Awake() {
            _material = GetComponent<MeshRenderer>().material;
            Collider = GetComponent<BoxCollider>();
            Collider.isTrigger = true;
            Mix(Color);
        }

        public void Mix(TileColor color)
        {
            Color = TileColorUtility.MixColor(Color,color);
            _material.color = TileColorUtility.GetColor(Color);
        }

        private void OnTriggerEnter(Collider other) {
                if (other.gameObject.layer==8)
                {
                    Mix(TileBoard.SelectedColor);   
                }
        }
    }
}