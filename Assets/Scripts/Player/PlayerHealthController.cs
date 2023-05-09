using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������ƣ��ܻ�������������
/// �����������ﲥ����״̬�ﲥ
/// </summary>
[RequireComponent(typeof(PlayerAI))]
public class PlayerHealthController : Attackable
{
    private PlayerAI playerAI; // ���AI��״̬��������
    private void Start()
    {
        playerAI = GetComponent<PlayerAI>();
        playerAI.animator.SetInteger("HealthPoint", PlayerInfo.GetInstance().CurrentHp);
        EventCenter.GetInstance().SubscribeEvent<int>(StaticNameInfo.PlayerHealthChange, UpdateHealth); // ���Ķ�����Ѫ�������¼�
    }

    public override void Attacked(int damage)
    {
        base.Attacked(damage);
        playerAI.ChangeStateTo(PlayerState.playerGetHitState);
        PlayerInfo.GetInstance().CurrentHp -= (damage - PlayerInfo.GetInstance().DefenseAbility); // ����
    }
    /// <summary>
    /// ����״̬�����������
    /// </summary>
    /// <param name="hp"></param>
    private void UpdateHealth(int hp)
    {
        playerAI.animator.SetInteger("HealthPoint", hp);
    }
}
