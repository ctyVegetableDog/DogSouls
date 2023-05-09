using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʾ�����˸ýű��Ķ�����һ����ɫ��ӵ�н�ɫ�Ļ�����Ϊ�߼�
/// ���������˺�����
/// �������ͷ�����
/// 
/// ��ҽ�ɫ��ʹ��
/// </summary>
public class EnemyInfo : MonoBehaviour
{
    #region ����ģ��
    public int maxHp; // �������ֵ
    public int currentHp; // ��ǰ����ֵ
    [SerializeField]
    public string deadEventName; // �����¼���
    public string damagedEventName; // �����¼���
    #endregion
    #region ս��ģ��
    public int offenseAbility; // ������
    public int defenseAbility; // ���R��
    #endregion
    #region �ƶ�ģ��
    public float moveSpeed; //�ƶ��ٶ�
    #endregion
    private void Awake()
    {
        currentHp = maxHp;
    }
}