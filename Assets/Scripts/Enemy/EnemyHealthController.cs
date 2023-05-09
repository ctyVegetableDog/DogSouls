using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 怪物信息脚本，绑定了该脚本的敌人可以受击，受伤和死亡
/// 碰撞盒在Attackable里挂了
/// 需要有个BaseHealthBar子节点
/// </summary>
[RequireComponent(typeof(EnemyInfo))]
public class EnemyHealthController : Attackable
{
    [SerializeField]
    private bool backToPool; // 是否回到缓存池，默认都是回去
    private EnemyInfo characterInfo;
    private Animator animator; // 从子物体拿
    private Slider healthSlider; // 血条
    private void Start()
    {
        healthSlider = GetComponentInChildren<Slider>(true); // 找到血条
        backToPool = true;
        characterInfo = GetComponent<EnemyInfo>();
        animator = GetComponentInChildren<Animator>();
        healthSlider.maxValue = characterInfo.maxHp; // 设置血条最大值
        healthSlider.minValue = 0; // 设置血条最小值
        healthSlider.value = characterInfo.currentHp; // 设置当前血条值
        animator.SetInteger("HealthPoint", characterInfo.currentHp); // 设置生命
        EventCenter.GetInstance().SubscribeEvent<GameObject>(StaticNameInfo.EnemyDead, Dead); // 订阅死亡事件，在死亡动画里发布
    }

    /// <summary>
    /// 受到攻击，受伤
    /// </summary>
    /// <param name="damage">伤害</param>
    public override void Attacked(int damage)
    {
        animator.ResetTrigger("GetHit");
        characterInfo.currentHp -= (damage - characterInfo.defenseAbility); // 受伤
        animator.SetInteger("HealthPoint", characterInfo.currentHp); // 设置生命
        healthSlider.value = characterInfo.currentHp; // 设置血条
        if (!healthSlider.gameObject.activeSelf) healthSlider.gameObject.SetActive(true); // 设置血条可用
        // 这里不用管死亡，死亡在HealthPoint为0时播放死亡动画，可以在死亡动画事件里做死亡
        // EventCenter.GetInstance().PublishEvent(characterInfo.damagedEventName); // 发布受击事件，这个先不用吧，目前不需要
        // 受击先不用事件，播放受击动画
        animator.SetTrigger("GetHit");

    }

    /// <summary>
    /// 怪物死了就回缓存池
    /// </summary>
    /// <param name="deadEventName">死亡事件名</param>
    public override void Dead(GameObject gameObject)
    {
        base.Dead(gameObject);
        if (backToPool) Pool.GetInstance().Push(name, gameObject); // 回到缓存池
        else Destroy(gameObject);
    }
}
