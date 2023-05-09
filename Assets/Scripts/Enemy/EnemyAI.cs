using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����AI��
/// �о��ƶ���תɶ�Ķ��������
/// ���˱Ƚϼ򵥣��ƶ��������߼������⣬��Ȼ������ͬʱ�����Mono����������
/// ����Ҳ��״̬ģʽ�ɣ���Ȼ����̫����ˣ����Һ���
/// </summary>
public class EnemyAI : MonoBehaviour
{
    private Animator animator; //���˶������
    private Transform trans; //����transform
    private EnemyInfo enemyInfo; // ������Ϣ
    private bool isMoving = false;
    private void Start()
    {
       
        trans = this.transform;
        enemyInfo = GetComponent<EnemyInfo>(); // ��ȡ���˵���Ϣ���
        animator = GetComponentInChildren<Animator>(); // ��ȡ���˶������
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
    /// ǰ��
    /// </summary>
    private void MoveForward()
    {
        trans.Translate(0, 0, enemyInfo.moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// �������
    /// </summary>
    public void LookAtPlayer()
    {
        trans.LookAt(PlayerInfo.GetInstance().playerTransform);
    }
}
