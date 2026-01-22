namespace GameStateMachine
{
    public interface IState
    {
        void Enter();//войти и выйти из состояния
        void Exit();
    }
}