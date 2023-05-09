/// <summary>
/// 玩家状态基类
/// </summary>
public abstract class PlayerState
{
    #region 移动状态
    public static PlayerMoveState playerWalkState;
    #endregion

    #region 攻击状态
    public static PlayerSlightAttackState playerSlightAttackState;
    #endregion

    #region 闲置
    public static PlayerIdleState playerIdleState;
    #endregion

    #region 受击
    public static PlayerGetHitState playerGetHitState; 
    #endregion
    /// <summary>
    /// 进入状态时
    /// </summary>
    /// <param name="playerAI"></param>
    public abstract void StateEnter(PlayerAI playerAI);
    /// <summary>
    /// 状态中
    /// </summary>
    /// <param name="playerAI"></param>
    public abstract void StateUpdate(PlayerAI playerAI);
    /// <summary>
    /// 退出状态
    /// </summary>
    /// <param name="playerAI"></param>
    public abstract void StateExit(PlayerAI playerAI);

    static PlayerState()
    {
        playerIdleState = new PlayerIdleState();
        playerWalkState = new PlayerMoveState();
        playerSlightAttackState = new PlayerSlightAttackState();
        playerGetHitState = new PlayerGetHitState();
    }
}