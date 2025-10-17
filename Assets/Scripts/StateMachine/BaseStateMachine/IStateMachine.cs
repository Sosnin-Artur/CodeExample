public interface IStateMachine<T> where T : IState
{
    void ChangeState(T state);
}