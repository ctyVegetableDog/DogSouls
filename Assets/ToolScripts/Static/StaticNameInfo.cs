using UnityEngine;

public static class StaticNameInfo
{
    #region AB包
    // AB包路径名
    public static string AssetbundlePathUrl = Application.streamingAssetsPath + "/";
    // AB包主包名
    public static string AssetbundleMainBundleName = "Windows";
    #endregion

    #region 事件
    // 主角受击结束事件名
    public static string PlayerGetHitFinish = "PlayerGetHitFinish";
    // 主角攻击结束事件名
    public static string PlayerAttackFinish = "PlayerAttackFinish";
    // 敌人死亡
    public static string EnemyDead = "EnemyDead";
    // 主角生命改变
    public static string PlayerHealthChange = "PlayerHealthChange";
    // 主角精力改变
    public static string PlayerEnergyChange = "PlayerEnergyChange";
    #endregion
}