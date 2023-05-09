using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ϣ�ĵ���
/// ������ҵ�������������������
/// ���������ļ�
/// </summary>

public class PlayerInfo : Singleton<PlayerInfo>
{
    #region ����ģ��
    public int MaxHp { get; set; } // �������ֵ
    private int currentHp;// ��ǰ����ֵ
    public int CurrentHp { get { return currentHp; } set {
            currentHp = value;
            if (currentHp > MaxHp) currentHp = MaxHp;
            if (currentHp < 0) currentHp = 0;
            EventCenter.GetInstance().PublishEvent<int>(StaticNameInfo.PlayerHealthChange, currentHp); // �����¼���UI֪��������Ѫ��
        } }
    #endregion
    #region ����ģ��
    public float MaxEnergy { get; set; } // �����ֵ
    private float currentEnergy; // ��ǰ����ֵ
    public float CurrentEnergy { get { return currentEnergy; } set {
            currentEnergy = value;
            if (currentEnergy > MaxEnergy) currentEnergy = MaxEnergy;
            if (currentEnergy < 0) currentEnergy = 0;
            EventCenter.GetInstance().PublishEvent<float>(StaticNameInfo.PlayerEnergyChange, currentEnergy); // �����¼���UI֪������������
        } }
    public float RetrieveSpeed { get; set; } // �����ָ��ٶ�
    public bool CanRetrieveEnergy { get; set; } // �Ƿ���Իָ�����
    #endregion
    #region ս��ģ��
    public int OffenseAbility { get; set; } // ������
    public int DefenseAbility { get; set; } // ������
    public float AttackEnergy { get; set; } // ������Ҫ�ľ���
    #endregion
    #region �ƶ�ģ��
    public float BaseMoveSpeed { get; set; } // �ƶ��ٶȻ���
    public float CurrentMoveSpeedDelta { get; set; } // ��ǰ�ƶ��ٶȱ���
    public float MaxMoveSpeedDelta { get; set; } // ����ƶ��ٶȱ���
    public float Accurate { get; set; }// ���ٶ�
    public float RunEnergyPerFrame { get; set; } // ÿ֡���ĵľ���
    public Transform playerTransform; // ��ҵ�transform
    #endregion

    /// <summary>
    /// ��ʼ�������Ϣ��Ӧ�����������ļ�����д����
    /// </summary>
    public void Init()
    {
        MaxHp = 100;
        CurrentHp = MaxHp;
        MaxEnergy = 100;
        CurrentEnergy = MaxEnergy;
        RetrieveSpeed = 0.5f;
        OffenseAbility = 5;
        DefenseAbility = 5;
        BaseMoveSpeed = 3f;
        CurrentMoveSpeedDelta = 1f;
        MaxMoveSpeedDelta = 1.5f;
        Accurate = 0.5f;
        RunEnergyPerFrame = 0.2f;
        CanRetrieveEnergy = true;
        MonoManager.GetInstance().Subscribe(EnergyRetrieve); // ÿ֡�ָ�����
        AttackEnergy = 20f;
    }
    /// <summary>
    /// �Զ��ָ�������ÿ֡���ã�ί�и�MonoManager
    /// </summary>
    public void EnergyRetrieve()
    {
        if (CanRetrieveEnergy && CurrentEnergy < MaxEnergy)
        {
            CurrentEnergy += RetrieveSpeed;
            if (CurrentEnergy > MaxEnergy) CurrentEnergy = MaxEnergy;
        }
    }
}
