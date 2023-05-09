using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void StateEnter(PlayerAI playerAI)
    {

    }

    public override void StateExit(PlayerAI playerAI)
    {

    }

    public override void StateUpdate(PlayerAI playerAI)
    {
        // 转到移动状态
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            playerAI.ChangeStateTo(PlayerState.playerWalkState);
        }
        //轻攻击
        else if (Input.GetAxisRaw("SlightAttack") > 0 && PlayerInfo.GetInstance().CurrentEnergy >= 20)
        {
            playerAI.ChangeStateTo(PlayerState.playerSlightAttackState);
        }
        //重攻击
    }
}
