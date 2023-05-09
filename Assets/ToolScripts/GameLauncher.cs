using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    private void Start()
    {
        PlayerInfo.GetInstance().Init(); // 初始化玩家信息

        #region 初始化UI
        UIMananger.GetInstance().ShowPanel<PlayerPanel>("ui", "PlayerInfoPanel");
        #endregion

        #region 初始化场地
        GameObject field = AssetbundleManager.GetInstance().LoadRes<GameObject>("field", "MainGameField");
        field.transform.Translate(0, -0.5f, 0);
        #endregion

        #region 初始化敌人
        GameObject enemy = Pool.GetInstance().Get("enemy", "Slime");
        #endregion
    }
}
