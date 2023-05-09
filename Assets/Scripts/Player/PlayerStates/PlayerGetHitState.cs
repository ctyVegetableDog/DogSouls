using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家受击
/// 进入状态时触发动画，状态更新时不能响应游戏中的按键
/// 受击结束时恢复Idle
/// 判断受击结束在动画事件，也就是说还需要给模型绑个脚本专门来监视受击动画是否结束
/// </summary>
public class PlayerGetHitState : PlayerState
{
    private bool isOver; // 受击结束
    public override void StateEnter(PlayerAI playerAI)
    {
        isOver = false;
        playerAI.animator.SetTrigger("GetHit"); // 开始播放受击动画
        EventCenter.GetInstance().SubscribeEvent(StaticNameInfo.PlayerGetHitFinish, HitFinish); // 订阅攻击结束事件，绑定了动画事件的脚本在攻击结束时触发该事件
    }

    public override void StateExit(PlayerAI playerAI)
    {
        playerAI.animator.ResetTrigger("GetHit");
        EventCenter.GetInstance().UnsubscribeEvent(StaticNameInfo.PlayerGetHitFinish, HitFinish);
    }

    public override void StateUpdate(PlayerAI playerAI)
    {
        if (isOver)
        {
            playerAI.ChangeStateTo(PlayerState.playerIdleState);
        }
    }
    /// <summary>
    /// 受击结束
    /// </summary>
    private void HitFinish()
    {
        isOver = true;
    }
}