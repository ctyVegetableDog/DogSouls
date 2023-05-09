using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˸ýű�����Ķ����ǿ��Ա�������
/// ������ϰ���ɶ������Դ�����ҵĵ�������
/// ����ǹ�������������ܻ��߼�����MonsterController����
/// ���������������ҵ��ܻ��߼�����PlayerInfo��PlayerAI����
/// </summary>

[RequireComponent(typeof(Collider))] // ��Ҫ��ײ�м����ײ
public abstract class Attackable : MonoBehaviour
{
    /// <summary>
    /// ������ʱ����
    /// </summary>
    public virtual void Attacked(int damage) { }
    /// <summary>
    /// ����ʱ���ã�����ǳ�������Ҳ�б��ݻٵ�ʱ���޷����ݻپͲ�д����
    /// </summary>
    /// <param name="deadEventName"></param>
    public virtual void Dead(GameObject gameObject)
    {
        
    }
}
