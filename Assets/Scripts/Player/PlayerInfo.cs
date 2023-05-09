using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家信息的单例
/// 保存玩家的生命，精力，攻击等
/// 来自配置文件
/// </summary>

public class PlayerInfo : Singleton<PlayerInfo>
{
    #region 生命模块
    public int MaxHp { get; set; } // 最大生命值
    private int currentHp;// 当前生命值
    public int CurrentHp { get { return currentHp; } set {
            currentHp = value;
            if (currentHp > MaxHp) currentHp = MaxHp;
            if (currentHp < 0) currentHp = 0;
            EventCenter.GetInstance().PublishEvent<int>(StaticNameInfo.PlayerHealthChange, currentHp); // 发布事件给UI知道，更新血条
        } }
    #endregion
    #region 精力模块
    public float MaxEnergy { get; set; } // 最大精力值
    private float currentEnergy; // 当前精力值
    public float CurrentEnergy { get { return currentEnergy; } set {
            currentEnergy = value;
            if (currentEnergy > MaxEnergy) currentEnergy = MaxEnergy;
            if (currentEnergy < 0) currentEnergy = 0;
            EventCenter.GetInstance().PublishEvent<float>(StaticNameInfo.PlayerEnergyChange, currentEnergy); // 发布事件给UI知道，更新绿条
        } }
    public float RetrieveSpeed { get; set; } // 精力恢复速度
    public bool CanRetrieveEnergy { get; set; } // 是否可以恢复精力
    #endregion
    #region 战斗模块
    public int OffenseAbility { get; set; } // 攻击力
    public int DefenseAbility { get; set; } // 防御力
    public float AttackEnergy { get; set; } // 攻击需要的精力
    #endregion
    #region 移动模块
    public float BaseMoveSpeed { get; set; } // 移动速度基数
    public float CurrentMoveSpeedDelta { get; set; } // 当前移动速度倍率
    public float MaxMoveSpeedDelta { get; set; } // 最大移动速度倍率
    public float Accurate { get; set; }// 加速度
    public float RunEnergyPerFrame { get; set; } // 每帧消耗的精力
    public Transform playerTransform; // 玩家的transform
    #endregion

    /// <summary>
    /// 初始化玩家信息，应该来自配置文件，先写死吧
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
        MonoManager.GetInstance().Subscribe(EnergyRetrieve); // 每帧恢复精力
        AttackEnergy = 20f;
    }
    /// <summary>
    /// 自动恢复精力，每帧调用，委托给MonoManager
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
