using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������model�ϣ���ΪҪ�ö����¼�
/// </summary>
public class EnemyAttackDetect : BaseAttackDetect
{
    private EnemyInfo enemyInfo; // ������Ϣ��������ɶ�Ĵ����������������丸������
    protected override void Start()
    {
        enemyInfo = transform.parent.GetComponent<EnemyInfo>(); // �ҵ�EnemyInfo
        base.Start();
    }

    protected override void ShootSingle(int dir)
    {
        damage = enemyInfo.offenseAbility; // �����˺�
        base.ShootSingle(dir);
    }

    protected override void AttackFinish(string eventName)
    {
        base.AttackFinish(eventName);
    }
}
