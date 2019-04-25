using UnityEngine;

namespace com.paintpuzzle
{
    public class PaletteCellScript : MonoBehaviour
    {

        private GameManager gameManager;

        public void SetGameManager(GameManager manager)
        {
            gameManager = manager;
        }

        void OnMouseDown()
        {
            gameManager.SelectedColor = gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

}