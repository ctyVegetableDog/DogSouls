using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载了该脚本子类的物体可以进行近战攻击检测，需要绑定在有Animator的游戏对象上（model上）
/// 其父物体需要绑定一个叫AttackPoint的空子物体，从该物体上发射射线
/// 
/// 攻击力的话，玩家就从配置文件来，怪物就从CharacterInfo里来
/// </summary>

public abstract class BaseAttackDetect:MonoBehaviour
{
    [SerializeField]
    private float detectDistance; // 检测的距离
    [SerializeField]
    private string[] detectMasks; // 想要检测的层
    private LayerMask layerMask; // 通过DetectMasks计算出真正想要的层
    private HashSet<int> attackedObject; // 记录受到攻击的游戏对象，防止一次攻击中被击中多次检测
    private Transform attackPoint; // 攻击点，从该处发射射线检测，从其父物体配置的叫做AttackPoint的点上拿
    //private EnemyInfo attackInfo; // 攻击力等，现在之后攻击力，等之后有异常属性了再改成结构吧，得从一个CharacterInfo里拿
    protected int damage; // 攻击伤害，单独拿出来吧

    protected virtual void Start()
    {
        attackedObject = new HashSet<int>();
        attackPoint = this.transform.parent.Find("AttackPoint");
        //attackInfo = this.transform.parent.GetComponent<EnemyInfo>();
        foreach (string maskName in detectMasks)
        {
            layerMask |= LayerMask.NameToLayer(maskName);
        }
        layerMask = ~layerMask;
        
    }


    /// <summary>
    /// 从AttackPoint所在的位置朝指定方向发射射线
    /// </summary>
    /// <param name="direction">角度为45度  1.右前方 2.左前方 3.上前方 4.下前方 5.正前方 6.右上方 7.左上方 8.右下方 9.左下方 其他.正前方 </param>
    protected virtual void ShootSingle(int dir)
    {
        //damage = ...
        Vector3 direction = CalDirection(dir);
        RaycastHit[] singleHit = Physics.RaycastAll(origin: attackPoint.position, direction: direction, maxDistance: detectDistance, layerMask: layerMask);
        foreach (RaycastHit hit in singleHit)
        {
            int insID = hit.collider.gameObject.GetInstanceID();
            if (!attackedObject.Contains(insID)) // 没有被检测过
            {
                attackedObject.Add(insID);
                // 通过collsion找到受击对象，调用其受击方法，就在HealthController里吧
                Attackable target = hit.collider.gameObject.GetComponent<Attackable>();
                if (target != null) target.Attacked(damage); // 受击
            }
        }
    }
    /// <summary>
    /// 计算方位，角度为45度
    /// 1.右前方 2.左前方 3.上前方 4.下前方 5.正前方 6.右上方 7.左上方 8.右下方 9.左下方 其他.正前方 
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private Vector3 CalDirection(int direction)
    {
        switch (direction)
        {
            case 1:
                return attackPoint.forward + attackPoint.right; // 右前方
            case 2:
                return attackPoint.forward - attackPoint.right; // 左前方
            case 3:
                return attackPoint.forward + attackPoint.up; // 上前方
            case 4:
                return attackPoint.forward - attackPoint.up; // 下前方
            case 5:
                return attackPoint.forward; // 正前方
            case 6:
                return attackPoint.forward + attackPoint.right + attackPoint.up; // 右上方
            case 7:
                return attackPoint.forward - attackPoint.right + attackPoint.up; // 左上方
            case 8:
                return attackPoint.forward + attackPoint.right - attackPoint.up; // 右下方
            case 9:
                return attackPoint.forward - attackPoint.right - attackPoint.up; // 左下方
            default:
                return attackPoint.forward; // 正前方
        }
    }

    /// <summary>
    /// 攻击结束，绑定到攻击结束的动画事件上
    /// </summary>
    /// <param name="eventName">触发的回调事件，比如主角状态转换</param>
    protected virtual void AttackFinish(string eventName)
    {
        EventCenter.GetInstance().PublishEvent(eventName);
        attackedObject.Clear();
    }
}
