using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 表示挂载了该脚本的对象是一个角色，拥有角色的基本行为逻辑
/// 生命，受伤和死亡
/// 攻击力和防御力
/// 
/// 玩家角色不使用
/// </summary>
public class EnemyInfo : MonoBehaviour
{
    #region 生命模块
    public int maxHp; // 最大生命值
    public int currentHp; // 當前生命值
    [SerializeField]
    public string deadEventName; // 死亡事件名
    public string damagedEventName; // 受伤事件名
    #endregion
    #region 战斗模块
    public int offenseAbility; // 攻击力
    public int defenseAbility; // 防禦力
    #endregion
    #region 移动模块
    public float moveSpeed; //移动速度
    #endregion
    private void Awake()
    {
        currentHp = maxHp;
    }
}