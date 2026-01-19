using System.Collections.Generic;
using Game.Tiles;
using Game.Utils;
using UnityEngine;
using VContainer;
using Grid = Game.GridSystem.Grid;

//визуальное отображение доски
namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private TileConfig _tileConfig;
        private readonly List<Tile> _tilesToRefill = new List<Tile>();//тут храним плитки которые хотим заполнить 
        
        private Grid _grid;
        private TilePool _tilePool;
        private SetupCamera _setupCamera;

        private void Start()
        {
            _grid.SetupGrid(10, 10);
            CreateBoard();
            _setupCamera.SetCamera(_grid.Width, _grid.Height, false);
        }

        public void CreateBoard()
        {
            FillBoard();
        }

        private void FillBoard()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if(_grid.GetValue(x, y)) continue; //если в сетке что-то есть, то идем дальше
                    var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), transform);
                    _grid.SetValue(x, y, tile);
                    tile.gameObject.SetActive(true);
                    _tilesToRefill.Add(tile);
                }
            }
        }


        [Inject]
        private void Construct(Grid grid, SetupCamera setupCamera, TilePool pool) //аналог конструктора, с помощью инджект даем понять, что мы принимает аргурменты из контейнера 
        {
            _grid = grid;
            _setupCamera = setupCamera;
            _tilePool = pool;
        }
    }
}