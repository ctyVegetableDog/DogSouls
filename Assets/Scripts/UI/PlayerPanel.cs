using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : BasePanel
{
    private Slider HpSlider; // ������
    private Slider EnergySlider; // ������
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

        EventCenter.GetInstance().SubscribeEvent<int>(StaticNameInfo.PlayerHealthChange, UpdateHpSlider); // ���������ı��¼�
        EventCenter.GetInstance().SubscribeEvent<float>(StaticNameInfo.PlayerEnergyChange, UpdateEnergySlider); // ���ľ����ı��¼�

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
