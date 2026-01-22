using System.Collections.Generic;
using System.Linq;
using Game.Board;
using GameStateMachine.States;

namespace GameStateMachine
{
    public class StateMachine: IStateSwitcher
    {
        private List<IState> _states;//храним список состояний
        private IState _currentState;//текущее состояние
        private GameBoard _gameBoard;

        public StateMachine(GameBoard  gameBoard)
        {
            _gameBoard = gameBoard;
            _states = new List<IState>()
            {
                new PrepareState(this, _gameBoard)

            };
            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            var state = _states.FirstOrDefault(state => state is T);//какой передадим такой и будет искать
            _currentState.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}