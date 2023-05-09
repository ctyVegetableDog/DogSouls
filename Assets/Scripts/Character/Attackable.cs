using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 绑定了该脚本子类的对象是可以被攻击的
/// 如果是障碍物啥的则可以触发玩家的弹刀动画
/// 如果是怪物则进入怪物的受击逻辑，和MonsterController交互
/// 如果是玩家则进入玩家的受击逻辑，和PlayerInfo，PlayerAI交互
/// </summary>

[RequireComponent(typeof(Collider))] // 需要碰撞盒检测碰撞
public abstract class Attackable : MonoBehaviour
{
    /// <summary>
    /// 被攻击时调用
    /// </summary>
    public virtual void Attacked(int damage) { }
    /// <summary>
    /// 死亡时调用，如果是场景，则也有被摧毁的时候，无法被摧毁就不写就行
    /// </summary>
    /// <param name="deadEventName"></param>
    public virtual void Dead(GameObject gameObject)
    {
        
    }
}
