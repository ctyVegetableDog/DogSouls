using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������Ϣ�ű������˸ýű��ĵ��˿����ܻ������˺�����
/// ��ײ����Attackable�����
/// ��Ҫ�и�BaseHealthBar�ӽڵ�
/// </summary>
[RequireComponent(typeof(EnemyInfo))]
public class EnemyHealthController : Attackable
{
    [SerializeField]
    private bool backToPool; // �Ƿ�ص�����أ�Ĭ�϶��ǻ�ȥ
    private EnemyInfo characterInfo;
    private Animator animator; // ����������
    private Slider healthSlider; // Ѫ��
    private void Start()
    {
        healthSlider = GetComponentInChildren<Slider>(true); // �ҵ�Ѫ��
        backToPool = true;
        characterInfo = GetComponent<EnemyInfo>();
        animator = GetComponentInChildren<Animator>();
        healthSlider.maxValue = characterInfo.maxHp; // ����Ѫ�����ֵ
        healthSlider.minValue = 0; // ����Ѫ����Сֵ
        healthSlider.value = characterInfo.currentHp; // ���õ�ǰѪ��ֵ
        animator.SetInteger("HealthPoint", characterInfo.currentHp); // ��������
        EventCenter.GetInstance().SubscribeEvent<GameObject>(StaticNameInfo.EnemyDead, Dead); // ���������¼��������������﷢��
    }

    /// <summary>
    /// �ܵ�����������
    /// </summary>
    /// <param name="damage">�˺�</param>
    public override void Attacked(int damage)
    {
        animator.ResetTrigger("GetHit");
        characterInfo.currentHp -= (damage - characterInfo.defenseAbility); // ����
        animator.SetInteger("HealthPoint", characterInfo.currentHp); // ��������
        healthSlider.value = characterInfo.currentHp; // ����Ѫ��
        if (!healthSlider.gameObject.activeSelf) healthSlider.gameObject.SetActive(true); // ����Ѫ������
        // ���ﲻ�ù�������������HealthPointΪ0ʱ�����������������������������¼���������
        // EventCenter.GetInstance().PublishEvent(characterInfo.damagedEventName); // �����ܻ��¼�������Ȳ��ðɣ�Ŀǰ����Ҫ
        // �ܻ��Ȳ����¼��������ܻ�����
        animator.SetTrigger("GetHit");

    }

    /// <summary>
    /// �������˾ͻػ����
    /// </summary>
    /// <param name="deadEventName">�����¼���</param>
    public override void Dead(GameObject gameObject)
    {
        base.Dead(gameObject);
        if (backToPool) Pool.GetInstance().Push(name, gameObject); // �ص������
        else Destroy(gameObject);
    }
}
