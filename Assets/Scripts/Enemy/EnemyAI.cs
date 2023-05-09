using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人AI类
/// 感觉移动旋转啥的都在这里吧
/// 敌人比较简单，移动攻击等逻辑都在这，不然场景里同时激活的Mono数会大幅增加
/// 怪物也用状态模式吧，不然还是太耦合了，而且很乱
/// </summary>
public class EnemyAI : MonoBehaviour
{
    private Animator animator; //敌人动画组件
    private Transform trans; //自身transform
    private EnemyInfo enemyInfo; // 敌人信息
    private bool isMoving = false;
    private void Start()
    {
       
        trans = this.transform;
        enemyInfo = GetComponent<EnemyInfo>(); // 获取敌人的信息组件
        animator = GetComponentInChildren<Animator>(); // 获取敌人动画组件
        if (!isMoving)
        {
            isMoving = true;
            animator.SetBool("IsMoving", true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        animator.SetTrigger("Attack");
    }
    /// <summary>
    /// 前进
    /// </summary>
    private void MoveForward()
    {
        trans.Translate(0, 0, enemyInfo.moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 看向玩家
    /// </summary>
    public void LookAtPlayer()
    {
        trans.LookAt(PlayerInfo.GetInstance().playerTransform);
    }
}
