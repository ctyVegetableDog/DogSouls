using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    private void Start()
    {
        PlayerInfo.GetInstance().Init(); // ��ʼ�������Ϣ

        #region ��ʼ��UI
        UIMananger.GetInstance().ShowPanel<PlayerPanel>("ui", "PlayerInfoPanel");
        #endregion

        #region ��ʼ������
        GameObject field = AssetbundleManager.GetInstance().LoadRes<GameObject>("field", "MainGameField");
        field.transform.Translate(0, -0.5f, 0);
        #endregion

        #region ��ʼ������
        GameObject enemy = Pool.GetInstance().Get("enemy", "Slime");
        #endregion
    }
}
