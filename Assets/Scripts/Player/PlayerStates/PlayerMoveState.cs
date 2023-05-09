using UnityEngine;

/// <summary>
/// 该状态表示玩家正在移动
/// </summary>
public class PlayerMoveState : PlayerState
{
    /// <summary>
    /// 走路动画
    /// </summary>
    /// <param name="playerAI"></param>
    public override void StateEnter(PlayerAI playerAI)
    {
        playerAI.animator.SetBool("IsMoving", true);
    }

    /// <summary>
    /// 无输入闲置
    /// 轻攻击，则轻攻击
    /// 重攻击先不写吧
    /// </summary>
    /// <param name="playerAI"></param>
    public override void StateUpdate(PlayerAI playerAI)
    {
        // 进入闲置状态
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            PlayerInfo.GetInstance().CurrentMoveSpeedDelta = 1f; // 还原当前速度倍率
            playerAI.animator.SetFloat("Speed", 1.0f); // 设置动画
            playerAI.ChangeStateTo(PlayerState.playerIdleState); // 进入闲置
        }
        // 进入攻击状态
        else if (Input.GetAxisRaw("SlightAttack") > 0 && PlayerInfo.GetInstance().CurrentEnergy >= 20)
        {
            playerAI.ChangeStateTo(PlayerState.playerSlightAttackState); // 进入攻击
        }
        // 依然在移动
        else
        {
            ChangeSpeed(); // 计算新的速度倍率
            playerAI.animator.SetFloat("Speed", PlayerInfo.GetInstance().CurrentMoveSpeedDelta); // 设置新速度，切换走路和跑步
            float x = Input.GetAxis("Horizontal"), z = Input.GetAxis("Vertical"); // 获取输入
            playerAI.playerMotor.Rotate(x, z); // 旋转
            playerAI.playerMotor.MoveForward(PlayerInfo.GetInstance().CurrentMoveSpeedDelta); // 向前移动
                                                                                              // 精力改变，如果跑步需要消耗精力
            if (PlayerInfo.GetInstance().CurrentMoveSpeedDelta > 1f)
            {
                PlayerInfo.GetInstance().CanRetrieveEnergy = false;
                PlayerInfo.GetInstance().CurrentEnergy -= PlayerInfo.GetInstance().RunEnergyPerFrame;
            }
            else PlayerInfo.GetInstance().CanRetrieveEnergy = true;
        }
    }

    public override void StateExit(PlayerAI playerAI)
    {
        PlayerInfo.GetInstance().CanRetrieveEnergy = true; // 可以开始精力恢复
        playerAI.animator.SetBool("IsMoving", false); // 修改动画状态
    }

    /// <summary>
    /// 修改速度，切换走路和跑步
    /// </summary>
    private void ChangeSpeed()
    {
        if (PlayerInfo.GetInstance().CurrentEnergy <= 0) PlayerInfo.GetInstance().CurrentMoveSpeedDelta = 1f; // 没精力了不能跑步
        if (Input.GetAxisRaw("Run") > 0)
        {
            if (PlayerInfo.GetInstance().CurrentMoveSpeedDelta < PlayerInfo.GetInstance().MaxMoveSpeedDelta) PlayerInfo.GetInstance().CurrentMoveSpeedDelta += PlayerInfo.GetInstance().Accurate * Time.deltaTime;
            if (PlayerInfo.GetInstance().CurrentMoveSpeedDelta > PlayerInfo.GetInstance().MaxMoveSpeedDelta) PlayerInfo.GetInstance().CurrentMoveSpeedDelta = PlayerInfo.GetInstance().MaxMoveSpeedDelta;
        }
        else
        {
            if (PlayerInfo.GetInstance().CurrentMoveSpeedDelta > 1f) PlayerInfo.GetInstance().CurrentMoveSpeedDelta = 1f; // 直接刹车成1f
        }
    }
}
