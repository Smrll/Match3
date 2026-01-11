using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using VContainer;
using Game.GridSystem;

//визуальное отображение доски
namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        private readonly List<Tiles.Tile> _tilesToRefill = new List<Tiles.Tile>();//тут храним плитки которые хотим заполнить ????????????tile
        
        private Grid _grid;

        public void CreateBoard()
        {
            
        }

        private void FillBoard()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if(_grid.GetValue(x,y)) continue;
                    var tile = Instantiate(_tilePrefab, transform);
                    tile.transform.position = _grid.GridToWorld(x, y);
                    _grid.SetValue(x, y,tile.GetComponent<Tile>());
                }
            }
        }


        [Inject]
        private void Construct(Grid grid) //аналог конструктора, с помощью инджект даем понять, что мы принимает аргурменты из контейнера 
        {
            _grid = grid;
        }
    }
}