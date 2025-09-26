namespace StateMachine
{
    public class StateMachine
    {
        public BaseState currentState;

        public void SetState(BaseState newState, bool forceReset = false)
        {
            if (currentState != newState || forceReset)
            {
                currentState?.StateExit();

                currentState = newState;

                currentState.StateEnter();
            }
        }
    }
}
