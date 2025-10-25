using UnityEngine;

public class BossStateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState == newState)
            return;

        if (currentState != null)
            currentState.Exit();

        Debug.Log($"Current State: {currentState} | New State: {newState}");

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
            currentState.Update();
    }
}