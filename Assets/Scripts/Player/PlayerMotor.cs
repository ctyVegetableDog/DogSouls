using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 主角的移动模块，包含相机的旋转
/// 在这里公开玩家的transform并成为static的
/// </summary>
public class PlayerMotor : MonoBehaviour
{
    private Transform mainCam; //相机位置
    private Vector3 forwardDirection; // 正方向为玩家-相机在Y轴为法线平面上的投影

    private Transform trans; // 在此处公开玩家的transform
    private void Awake()
    {
        mainCam = Camera.main.transform;
        trans = this.transform;
        PlayerInfo.GetInstance().playerTransform = trans; // 将玩家的transform委托给静态玩家信息
    }

    /// <summary>
    /// 前进
    /// </summary>
    /// <param name="v">速度倍率</param>
    public void MoveForward(float v)
    {
        trans.Translate(0, 0, v * PlayerInfo.GetInstance().BaseMoveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 旋转，需要重新计算正方向，正方向为玩家-相机在Y轴为法线平面上的投影
    /// </summary>
    /// <param name="x">从Input获取</param>
    /// <param name="z">从Input获取</param>
    public void Rotate(float x, float z)
    {
        UpdateForward();
        trans.rotation = Quaternion.LookRotation(forwardDirection) * Quaternion.LookRotation(new Vector3(x, 0, z));
        //trans.rotation = Quaternion.LookRotation(new Vector3(x, 0, z));
    }

    /// <summary>
    /// 计算正方向
    /// </summary>
    private void UpdateForward()
    {
        forwardDirection = Vector3.ProjectOnPlane(trans.position - mainCam.position, Vector3.up);
    }
    
}
