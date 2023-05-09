using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("先不用它，感觉输入输出单独拿出来做模块反而麻烦", true)]
public class InputManager : Singleton<InputManager>
{
    // 被当前被监听的所有虚拟按键
    private List<string> virtualAxis = new List<string>();
    public bool isStart = true; // 输入检测开启
    public InputManager()
    {
        // 开启监听
        MonoManager.GetInstance().Subscribe(update);
    }
    // 检测所有虚拟按键
    public void CheckAll()
    {
        foreach (string axis in virtualAxis)
        {
            CheckAxis(axis);
        }
    }
    // 检测单个虚拟按键
    private void CheckAxis(string axis)
    {
        float axisValue = Input.GetAxis(axis);
        if (axisValue != 0)
        {
            EventCenter.GetInstance().PublishEvent(axis, axisValue);
        }
    }
    /// <summary>
    /// update方法，每帧调用
    /// </summary>
    private void update()
    {
        if (isStart)
        {
            CheckAll();
        }
    }
}
