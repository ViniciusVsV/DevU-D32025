using UnityEngine;

public class BossAttackState : IState
{
    // 0 = baixo, 1 = teclado, 2 = bateria
    private static int nextSoloIndex = 0;
    private int currentSoloIndex;

    private BossController bossController;
    private BossStateMachine bossStateMachine;
    public BossAttackState(BossController bossController, BossStateMachine stateMachine)
    {
        this.bossController = bossController;
        this.bossStateMachine = stateMachine;
    }

    public void Enter()
    {
        currentSoloIndex = nextSoloIndex;
        switch (currentSoloIndex)
        {
            case 0:
                bossController.bassSolo.StartBassSolo();
                break;
            case 1:
                bossController.keyboardSolo.StartKeyboardSolo();
                break;
            case 2:
                bossController.drumSolo.Activate();
                break;
        }

        nextSoloIndex++;
        if (nextSoloIndex > 2)
        {
            nextSoloIndex = 0;
        }
    }

    public void Update()
    {
        switch (currentSoloIndex)
        {
            case 0:
                if (!bossController.bassSolo.isSoloing)
                    bossStateMachine.ChangeState(bossController.idleState);
                break;
            case 1:
                if (!bossController.keyboardSolo.isSoloing)
                    bossStateMachine.ChangeState(bossController.idleState);
                break;
            case 2:
                if (!bossController.drumSolo.isSoloing)
                    bossStateMachine.ChangeState(bossController.idleState);
                break;
        }
    }
    
    public void Exit()
    {
           
    }
}