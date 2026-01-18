using Game.Board;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Grid = Game.GridSystem.Grid;

namespace DI
{
    public class GameScope: LifetimeScope

    {
        [SerializeField] private GameBoard _gameBoard;
        protected override void Configure(IContainerBuilder builder)
        {
            if (_gameBoard == null)
            {
                Debug.LogError("GameBoard is not assigned in GameScope!");
                return;
            }
            builder.Register<Grid>(Lifetime.Singleton);
            builder.RegisterInstance(_gameBoard);
        }
    }
}