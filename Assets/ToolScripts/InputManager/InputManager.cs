using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("�Ȳ��������о�������������ó�����ģ�鷴���鷳", true)]
public class InputManager : Singleton<InputManager>
{
    // ����ǰ���������������ⰴ��
    private List<string> virtualAxis = new List<string>();
    public bool isStart = true; // �����⿪��
    public InputManager()
    {
        // ��������
        MonoManager.GetInstance().Subscribe(update);
    }
    // ����������ⰴ��
    public void CheckAll()
    {
        foreach (string axis in virtualAxis)
        {
            CheckAxis(axis);
        }
    }
    // ��ⵥ�����ⰴ��
    private void CheckAxis(string axis)
    {
        float axisValue = Input.GetAxis(axis);
        if (axisValue != 0)
        {
            EventCenter.GetInstance().PublishEvent(axis, axisValue);
        }
    }
    /// <summary>
    /// update������ÿ֡����
    /// </summary>
    private void update()
    {
        if (isStart)
        {
            CheckAll();
        }
    }
}
