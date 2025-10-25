using UnityEngine;

public class BossIdleState : IState
{
    private BossController bossController;
    private BossStateMachine bossStateMachine;
    private float idleTimer;

    public BossIdleState(BossController bossController, BossStateMachine stateMachine)
    {
        this.bossController = bossController;
        this.bossStateMachine = stateMachine;
    }

    public void Enter()
    {
        idleTimer = bossController.attackInterval;
    }

    public void Update()
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0)
        {
            bossStateMachine.ChangeState(bossController.attackState);
        }
    }
    
    public void Exit()
    { }
}