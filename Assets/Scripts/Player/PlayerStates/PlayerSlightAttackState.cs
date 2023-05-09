using UnityEngine;

public class PlayerSlightAttackState : PlayerState
{
    private bool attackFinish; // 是否攻击结束
    public override void StateEnter(PlayerAI playerAI)
    {
        attackFinish = false;
        PlayerInfo.GetInstance().CurrentEnergy -= PlayerInfo.GetInstance().AttackEnergy; // 攻击消耗精力
        PlayerInfo.GetInstance().CanRetrieveEnergy = false;
        playerAI.animator.SetTrigger("SlightAttack");
        EventCenter.GetInstance().SubscribeEvent(StaticNameInfo.PlayerAttackFinish, AttackFinish); // 订阅攻击结束事件，绑定了动画事件的脚本在攻击结束时触发该事件
                                                                                      //PlayerInfo.currentWeapon.GetComponent<Collider>().enabled = true;
    }

    public override void StateExit(PlayerAI playerAI)
    {
        PlayerInfo.GetInstance().CanRetrieveEnergy = true;
        playerAI.animator.ResetTrigger("SlightAttack");
        EventCenter.GetInstance().UnsubscribeEvent(StaticNameInfo.PlayerAttackFinish, AttackFinish);
        //PlayerInfo.currentWeapon.GetComponent<Collider>().enabled = false;
    }
    /// <summary>
    /// 这里当动画事件到攻击的最后一帧触发
    /// </summary>
    private void AttackFinish()
    {
        attackFinish = true;
    }

    /// <summary>
    /// 判断攻击有没有结束，具体的攻击判定在攻击的动画事件里
    /// </summary>
    /// <param name="playerAI"></param>
    public override void StateUpdate(PlayerAI playerAI)
    {
        if (attackFinish)
        {
            playerAI.ChangeStateTo(PlayerState.playerIdleState);
        }
    }
}