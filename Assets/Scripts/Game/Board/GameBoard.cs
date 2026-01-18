using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Game.GridSystem;
using Grid = Game.GridSystem.Grid;
using Game.Tiles;

//визуальное отображение доски
namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private TileConfig _tileConfig;
        private readonly List<Tile> _tilesToRefill = new List<Tile>();//тут храним плитки которые хотим заполнить 
        
        private Grid _grid;

        private void Start()
        {
            _grid.SetupGrid(10, 10);
            CreateBoard();
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
                    var tile = Instantiate(_tilePrefab, transform);
                    tile.transform.position = _grid.GridToWorld(x, y);
                    var tileComponent = tile.GetComponent<Tile>();
                    tileComponent.SetTileConfig(_tileConfig);
                    _grid.SetValue(x, y, tileComponent);
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