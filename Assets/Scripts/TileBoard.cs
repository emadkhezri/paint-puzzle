using UnityEngine;

namespace com.paintpuzzle 
{
    public class TileBoard : MonoBehaviour
    {
        
        [SerializeField]
        private Tile _tilePrefab;

        [SerializeField]
        private int _width;
        [SerializeField]
        private int _height;

        private Tile[,] _boardMatrix;

        private void Awake()
        {
            _boardMatrix = new Tile[_width,_height];
            Init();
        }

        private void Init() 
        {
            for (int i = 0; i < _boardMatrix.GetLength(0) ; i++)
            {
                for(int j=0; j < _boardMatrix.GetLength(1); j++)
                {
                    Vector3 position = new Vector3Int(i,j,0);
                    Tile tileObject = GameObject.Instantiate<Tile>(_tilePrefab,position, Quaternion.identity);
                    _boardMatrix[i,j] = tileObject;
                }
            }
        }
        
    }
}