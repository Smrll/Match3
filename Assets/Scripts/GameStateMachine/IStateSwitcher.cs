namespace GameStateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState; //можем передавать любой стейт, тк он дженерик
    }
}