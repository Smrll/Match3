using Game.Board;
using UnityEngine;

namespace GameStateMachine.States
{
    public class PrepareState : IState

    {
        private readonly IStateSwitcher _stateSwitcher;
        private GameBoard _gameBoard;

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard)//зачем нужен конструктор? + вопросы в тг
        {
            _gameBoard = gameBoard;
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            _gameBoard.CreateBoard(); //вызываем здесь чтобы посмотреть что все работает
            
        }

        public void Exit()
        {
            Debug.Log("Game was started");
        }
    }
}