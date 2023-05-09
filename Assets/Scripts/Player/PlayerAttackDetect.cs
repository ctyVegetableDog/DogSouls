/// <summary>
/// 玩家攻击检测
/// 和怪物攻击检测的唯一区别是攻击力来源不同
/// </summary>
public class PlayerAttackDetect : BaseAttackDetect
{
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 攻击力从玩家信息拿
    /// </summary>
    /// <param name="dir"></param>
    protected override void ShootSingle(int dir)
    {
        damage = PlayerInfo.GetInstance().OffenseAbility;
        base.ShootSingle(dir);
    }

    /// <summary>
    /// 玩家攻击结束
    /// </summary>
    /// <param name="eventName"></param>
    protected override void AttackFinish(string eventName)
    {
        base.AttackFinish(eventName);
    }
}
