public class StateMachine
{
    public BaseState currentState;

    public void Set(BaseState newState, bool forceReset = false)
    {
        if (currentState != newState || forceReset)
        {
            currentState?.StateExit();

            currentState = newState;

            currentState.Initialise();
            currentState.StateEnter();
        }
    }
}