/// <summary>
/// ��ҹ������
/// �͹��﹥������Ψһ�����ǹ�������Դ��ͬ
/// </summary>
public class PlayerAttackDetect : BaseAttackDetect
{
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// �������������Ϣ��
    /// </summary>
    /// <param name="dir"></param>
    protected override void ShootSingle(int dir)
    {
        damage = PlayerInfo.GetInstance().OffenseAbility;
        base.ShootSingle(dir);
    }

    /// <summary>
    /// ��ҹ�������
    /// </summary>
    /// <param name="eventName"></param>
    protected override void AttackFinish(string eventName)
    {
        base.AttackFinish(eventName);
    }
}
