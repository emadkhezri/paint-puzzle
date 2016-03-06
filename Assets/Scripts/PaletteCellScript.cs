using UnityEngine;
using System.Collections;

public class PaletteCellScript : MonoBehaviour {

    private GameManagerScript gameManager;

    public void SetGameManager(GameManagerScript manager)
    {
        gameManager = manager;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        gameManager.SelectedColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
}
