using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家生命控制，受击和死亡在这里
/// 动画不在这里播，在状态里播
/// </summary>
[RequireComponent(typeof(PlayerAI))]
public class PlayerHealthController : Attackable
{
    private PlayerAI playerAI; // 玩家AI，状态从这里拿
    private void Start()
    {
        playerAI = GetComponent<PlayerAI>();
        playerAI.animator.SetInteger("HealthPoint", PlayerInfo.GetInstance().CurrentHp);
        EventCenter.GetInstance().SubscribeEvent<int>(StaticNameInfo.PlayerHealthChange, UpdateHealth); // 订阅动画的血条更新事件
    }

    public override void Attacked(int damage)
    {
        base.Attacked(damage);
        playerAI.ChangeStateTo(PlayerState.playerGetHitState);
        PlayerInfo.GetInstance().CurrentHp -= (damage - PlayerInfo.GetInstance().DefenseAbility); // 受伤
    }
    /// <summary>
    /// 设置状态机中玩家生命
    /// </summary>
    /// <param name="hp"></param>
    private void UpdateHealth(int hp)
    {
        playerAI.animator.SetInteger("HealthPoint", hp);
    }
}
