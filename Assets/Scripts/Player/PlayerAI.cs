using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制，负责玩家的行为管理，包括移动，攻击和受击等
/// </summary>
[RequireComponent(typeof(PlayerMotor))]
public class PlayerAI : MonoBehaviour
{
    public PlayerMotor playerMotor; // 移动旋转托管给playerMotor
    public Animator animator; // 获取动画状态机
    public PlayerState currentState; // 角色的实际行为托管给状态模式
    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
        animator = GetComponentInChildren<Animator>();
        currentState = PlayerState.playerIdleState;
    }
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newState">进入的新状态</param>
    public void ChangeStateTo(PlayerState newState)
    {
        currentState.StateExit(this); // 退出當前狀態
        currentState = newState;
        currentState.StateEnter(this); // 進入新狀態
    }

    private void Update()
    {
        currentState.StateUpdate(this);
    }
}
