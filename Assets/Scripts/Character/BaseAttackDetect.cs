using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����˸ýű������������Խ��н�ս������⣬��Ҫ������Animator����Ϸ�����ϣ�model�ϣ�
/// �丸������Ҫ��һ����AttackPoint�Ŀ������壬�Ӹ������Ϸ�������
/// 
/// �������Ļ�����Ҿʹ������ļ���������ʹ�CharacterInfo����
/// </summary>

public abstract class BaseAttackDetect:MonoBehaviour
{
    [SerializeField]
    private float detectDistance; // ���ľ���
    [SerializeField]
    private string[] detectMasks; // ��Ҫ���Ĳ�
    private LayerMask layerMask; // ͨ��DetectMasks�����������Ҫ�Ĳ�
    private HashSet<int> attackedObject; // ��¼�ܵ���������Ϸ���󣬷�ֹһ�ι����б����ж�μ��
    private Transform attackPoint; // �����㣬�Ӹô��������߼�⣬���丸�������õĽ���AttackPoint�ĵ�����
    //private EnemyInfo attackInfo; // �������ȣ�����֮�󹥻�������֮�����쳣�������ٸĳɽṹ�ɣ��ô�һ��CharacterInfo����
    protected int damage; // �����˺��������ó�����

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
    /// ��AttackPoint���ڵ�λ�ó�ָ������������
    /// </summary>
    /// <param name="direction">�Ƕ�Ϊ45��  1.��ǰ�� 2.��ǰ�� 3.��ǰ�� 4.��ǰ�� 5.��ǰ�� 6.���Ϸ� 7.���Ϸ� 8.���·� 9.���·� ����.��ǰ�� </param>
    protected virtual void ShootSingle(int dir)
    {
        //damage = ...
        Vector3 direction = CalDirection(dir);
        RaycastHit[] singleHit = Physics.RaycastAll(origin: attackPoint.position, direction: direction, maxDistance: detectDistance, layerMask: layerMask);
        foreach (RaycastHit hit in singleHit)
        {
            int insID = hit.collider.gameObject.GetInstanceID();
            if (!attackedObject.Contains(insID)) // û�б�����
            {
                attackedObject.Add(insID);
                // ͨ��collsion�ҵ��ܻ����󣬵������ܻ�����������HealthController���
                Attackable target = hit.collider.gameObject.GetComponent<Attackable>();
                if (target != null) target.Attacked(damage); // �ܻ�
            }
        }
    }
    /// <summary>
    /// ���㷽λ���Ƕ�Ϊ45��
    /// 1.��ǰ�� 2.��ǰ�� 3.��ǰ�� 4.��ǰ�� 5.��ǰ�� 6.���Ϸ� 7.���Ϸ� 8.���·� 9.���·� ����.��ǰ�� 
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private Vector3 CalDirection(int direction)
    {
        switch (direction)
        {
            case 1:
                return attackPoint.forward + attackPoint.right; // ��ǰ��
            case 2:
                return attackPoint.forward - attackPoint.right; // ��ǰ��
            case 3:
                return attackPoint.forward + attackPoint.up; // ��ǰ��
            case 4:
                return attackPoint.forward - attackPoint.up; // ��ǰ��
            case 5:
                return attackPoint.forward; // ��ǰ��
            case 6:
                return attackPoint.forward + attackPoint.right + attackPoint.up; // ���Ϸ�
            case 7:
                return attackPoint.forward - attackPoint.right + attackPoint.up; // ���Ϸ�
            case 8:
                return attackPoint.forward + attackPoint.right - attackPoint.up; // ���·�
            case 9:
                return attackPoint.forward - attackPoint.right - attackPoint.up; // ���·�
            default:
                return attackPoint.forward; // ��ǰ��
        }
    }

    /// <summary>
    /// �����������󶨵����������Ķ����¼���
    /// </summary>
    /// <param name="eventName">�����Ļص��¼�����������״̬ת��</param>
    protected virtual void AttackFinish(string eventName)
    {
        EventCenter.GetInstance().PublishEvent(eventName);
        attackedObject.Clear();
    }
}
