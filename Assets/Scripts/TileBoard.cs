using UnityEngine;
using System.Collections;
using System;

namespace com.paintpuzzle 
{
    public class TileBoard : MonoBehaviour
    {
        
        [SerializeField]
        private Tile _tilePrefab;

        [SerializeField]
        private int _width=2;
        [SerializeField]
        private int _height=2;

        private Tile[,] _boardMatrix;

        private void Awake()
        {
            _boardMatrix = new Tile[_width,_height];
            float x = -1 * _width  / 2f;
            float y = -1 * _height / 2f;
            transform.Translate(x, y, 0);   
            StartCoroutine(Init()); 
        }

        private IEnumerator Init() 
        {
            for (int i = _width-1; i  >= 0 ; i--)
            {
                for(int j=_height-1; j >= 0; j--)
                {
                    Vector3 position = new Vector3(i,j,0);
                    Tile tileObject = GameObject.Instantiate<Tile>(_tilePrefab, Vector3.zero, Quaternion.identity);
                    tileObject.name = $"Tile [{i}][{j}]";
                    tileObject.transform.SetParent(transform);
                    tileObject.transform.localPosition = position;
                    _boardMatrix[i,j] = tileObject;
                    yield return null;
                }
            }
        }

        public void Paint(TileColor selectedColor, Vector3 selectedPosition)
        {

        }
        
    }
}