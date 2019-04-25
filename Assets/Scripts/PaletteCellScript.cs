using UnityEngine;

public class PaletteCellScript : MonoBehaviour {

    private GameManagerScript gameManager;

    public void SetGameManager(GameManagerScript manager)
    {
        gameManager = manager;
    }

    void OnMouseDown()
    {
        gameManager.SelectedColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
}
