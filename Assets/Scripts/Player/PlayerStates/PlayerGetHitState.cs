using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ܻ�
/// ����״̬ʱ����������״̬����ʱ������Ӧ��Ϸ�еİ���
/// �ܻ�����ʱ�ָ�Idle
/// �ж��ܻ������ڶ����¼���Ҳ����˵����Ҫ��ģ�Ͱ���ű�ר���������ܻ������Ƿ����
/// </summary>
public class PlayerGetHitState : PlayerState
{
    private bool isOver; // �ܻ�����
    public override void StateEnter(PlayerAI playerAI)
    {
        isOver = false;
        playerAI.animator.SetTrigger("GetHit"); // ��ʼ�����ܻ�����
        EventCenter.GetInstance().SubscribeEvent(StaticNameInfo.PlayerGetHitFinish, HitFinish); // ���Ĺ��������¼������˶����¼��Ľű��ڹ�������ʱ�������¼�
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
    /// �ܻ�����
    /// </summary>
    private void HitFinish()
    {
        isOver = true;
    }
}