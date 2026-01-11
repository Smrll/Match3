using Game.Tiles;
using UnityEngine;

namespace Game.GridSystem
{
    public class Grid
    {
       public Tile[,] GameGrid {get; private set;}
        public int Width {get; private set;}
        public int Height {get; private set;}
        
        public Vector2Int CurrentPosition {get; private set;}
        public Vector2Int TargetPosition {get; private set;}
        
        public Grid(int width, int height) //сетка готова
        {
            Width = width;
            Height = height;
            GameGrid = new Tile[width, height];
        }
        //сетой надо как-то управлять
        public Vector2Int SetCurrentPosition(Vector2Int value) => CurrentPosition = value;
        public Vector2Int SetTargetPosition(Vector2Int value) => CurrentPosition = value;
        
        //координаты будем преобразовывать
        public Vector3 GridtoWorld(int x, int y) => new Vector3(x, y, z: 0);
        
        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            var x = Mathf.FloorToInt(worldPosition.x);
            var y = Mathf.FloorToInt(worldPosition.y);
            return new Vector2Int(x, y);
        }
        //теперь можем получать и назначать плитки
        public void SetValue(int x, int y, Tile tile)
        {
            if (IsValidPosition(x, y))
                GameGrid[x, y] = tile;
        }
        public void SetValue(Vector3 worldPosition, int y, Tile tile)
        {
            var worldToGrid = WorldToGrid(worldPosition);
            SetValue(worldToGrid.x, worldToGrid.y, tile);
        }
        
        //получение плитки
        public Tile GetValue(int x, int y) => IsValidPosition(x, y) ? GameGrid[x, y] : null;

        public Tile GetValue(Vector3 worldPosition)
        {
            var worldToGrid = WorldToGrid(worldPosition);
            return GetValue(worldToGrid.x, worldToGrid.y);
        }
        
        //добавим проверку, так как плитки можем перемещать, а нам важно чтобы они не выходили за границы
        public bool IsValidPosition(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
    }
}