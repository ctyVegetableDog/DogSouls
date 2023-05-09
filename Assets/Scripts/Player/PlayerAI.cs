using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҿ��ƣ�������ҵ���Ϊ���������ƶ����������ܻ���
/// </summary>
[RequireComponent(typeof(PlayerMotor))]
public class PlayerAI : MonoBehaviour
{
    public PlayerMotor playerMotor; // �ƶ���ת�йܸ�playerMotor
    public Animator animator; // ��ȡ����״̬��
    public PlayerState currentState; // ��ɫ��ʵ����Ϊ�йܸ�״̬ģʽ
    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
        animator = GetComponentInChildren<Animator>();
        currentState = PlayerState.playerIdleState;
    }
    /// <summary>
    /// �л�״̬
    /// </summary>
    /// <param name="newState">�������״̬</param>
    public void ChangeStateTo(PlayerState newState)
    {
        currentState.StateExit(this); // �˳���ǰ��B
        currentState = newState;
        currentState.StateEnter(this); // �M��� �B
    }

    private void Update()
    {
        currentState.StateUpdate(this);
    }
}
