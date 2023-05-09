using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载在model上，因为要用动画事件
/// </summary>
public class EnemyAttackDetect : BaseAttackDetect
{
    private EnemyInfo enemyInfo; // 敌人信息，攻击力啥的从这里来，挂载在其父物体上
    protected override void Start()
    {
        enemyInfo = transform.parent.GetComponent<EnemyInfo>(); // 找到EnemyInfo
        base.Start();
    }

    protected override void ShootSingle(int dir)
    {
        damage = enemyInfo.offenseAbility; // 设置伤害
        base.ShootSingle(dir);
    }

    protected override void AttackFinish(string eventName)
    {
        base.AttackFinish(eventName);
    }
}
