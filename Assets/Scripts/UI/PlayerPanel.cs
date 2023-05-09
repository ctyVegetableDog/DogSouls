using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : BasePanel
{
    private Slider HpSlider; // 生命条
    private Slider EnergySlider; // 精力条
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        HpSlider = GetChildrenByName<Slider>("PlayerHealthSlider");
        EnergySlider = GetChildrenByName<Slider>("PlayerEnergySlider");
        HpSlider.maxValue = PlayerInfo.GetInstance().MaxHp;
        HpSlider.minValue = 0;
        HpSlider.value = HpSlider.maxValue;
        EnergySlider.maxValue = PlayerInfo.GetInstance().MaxEnergy;
        EnergySlider.minValue = 0;
        EnergySlider.value = EnergySlider.maxValue;

        EventCenter.GetInstance().SubscribeEvent<int>(StaticNameInfo.PlayerHealthChange, UpdateHpSlider); // 订阅生命改变事件
        EventCenter.GetInstance().SubscribeEvent<float>(StaticNameInfo.PlayerEnergyChange, UpdateEnergySlider); // 订阅精力改变事件

    }
    private void UpdateHpSlider(int hp)
    {
        HpSlider.value = hp;
    }
    private void UpdateEnergySlider(float energy)
    {
        EnergySlider.value = energy;
    }
}
