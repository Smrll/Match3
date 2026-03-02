using System;
using Animations;
using Input;
using UnityEngine;
using Grid = Game.GridSystem.Grid;
using Game.Tiles;

namespace GameStateMachine.States
{
    public class PlayerTurnState: IState, IDisposable //состояние при котором работает инпут, игрок может взаимодействовать с плитками, выбирать их и указывать какие плитки будут меняться и взаимодействовать
    {
        private readonly Vector2Int _emptyPosition = Vector2Int.one * -1;
        private readonly InputReader _inputReader;
        private readonly Grid _grid;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Camera _camera;
        private IAnimation _animation;

        public PlayerTurnState(Grid grid, IStateSwitcher stateSwitcher, IAnimation animation)
        {
            _inputReader = new InputReader();
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _camera = Camera.main;
            _inputReader.Click += OnTileClick;
        }
        
        public void Dispose()
        {
            _inputReader.Click -= OnTileClick;
        }

        public void Enter()
        {
            _inputReader.EnableInputs(true);
            DeselectTile();
        }

        public void Exit()
        {
            _inputReader.EnableInputs(false);
        }
        

        private void OnTileClick()
        {
            var clickPosition = _grid.WorldToGrid(
                _camera.ScreenToWorldPoint(_inputReader.Position()));
            if(IsValidPosition(clickPosition) == false || IsBlankPosition(clickPosition))
                return;
            if (_grid.CurrentPosition == _emptyPosition)
            {
                //play sound
                _grid.SetCurrentPosition(clickPosition);
                _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1.2f);
            }
            else if (_grid.CurrentPosition == clickPosition)
            {
                //play sound
                DeselectTile();
            }
            else if (_grid.CurrentPosition != clickPosition && 
                     IsSwappable(_grid.CurrentPosition, clickPosition))
            {
                _grid.SetTargetPosition(clickPosition);
                _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, 
                    _grid.CurrentPosition.y), 1f);
                _stateSwitcher.SwitchState<SwapTilesState>();
            }

        }

        private void DeselectTile()
        {
            _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
            _grid.SetCurrentPosition(_emptyPosition);
            _grid.SetTargetPosition(_emptyPosition);
        }
        
        private bool IsSwappable(Vector2Int currentTilePos, Vector2Int targetTilePos) => 
            Mathf.Abs(currentTilePos.x - targetTilePos.x) + 
            Mathf.Abs(currentTilePos.y - targetTilePos.y) == 1;
        //5:5  5:6 = 5-5 + 5-6 = 1 так как abs
        //5:5  5:7 = 5-5 + 5-7 = 2 так как abs - не соседний тайл
        
        private bool IsBlankPosition(Vector2Int gridPos) => 
            _grid.GetValue(gridPos.x, gridPos.y).TileConfig.TileKind == TileKind.Blank;

        private bool IsValidPosition(Vector2Int gridPos)
        {
            return gridPos.x >= 0 && gridPos.x < _grid.Width && gridPos.y >= 0 && gridPos.y < _grid.Height;
        }
    }
}