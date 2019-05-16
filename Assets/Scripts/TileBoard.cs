using UnityEngine;
using System.Collections;
using System;

namespace com.paintpuzzle 
{
    public class TileBoard : MonoBehaviour
    {
        
        [SerializeField]
        private Tile _tilePrefab;

        private Vector3 _tilePrefabPivot;

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
            _tilePrefabPivot = new Vector3(x,y,0);
            transform.Translate(_tilePrefabPivot);   
            StartCoroutine(Init()); 
            MouseInputHandler.OnDragPreview += OnDragPreview;
            MouseInputHandler.OnDragFinished += OnDragFinished;
        }

        private void OnDragFinished(Vector3 dragStart, Vector3 dragEnd)
        {
            Vector2Int startIndex = new Vector2Int(Mathf.FloorToInt(dragStart.x-_tilePrefabPivot.x),Mathf.FloorToInt(dragStart.y-_tilePrefabPivot.y));
            Vector2Int endIndex = new Vector2Int(Mathf.FloorToInt(dragEnd.x-_tilePrefabPivot.x), Mathf.FloorToInt(dragEnd.y-_tilePrefabPivot.y));
            Paint(TileColor.Blue,startIndex,endIndex);
        }

        private void OnDragPreview(Vector3 dragStart, Vector3 dragEnd)
        {
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

        public void Paint(TileColor selectedColor, Vector2Int startIndex, Vector2Int endIndex)
        {
            for (int i = startIndex.x; i != endIndex.x; i+=Math.Sign(endIndex.x-startIndex.x))
            {
                for (int j = startIndex.y; j != endIndex.y; j+=Math.Sign(endIndex.y-startIndex.y))
                {
                    _boardMatrix[i,j].Mix(selectedColor);
                }
            }
        }
        
    }
}