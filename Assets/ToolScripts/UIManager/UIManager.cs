using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI�㼶ö��
/// </summary>
public enum E_UI_Layer
{
    System,
    Top,
    Mid,
    Bot
}

/// <summary>
/// �������Canvas�ļ���
/// </summary>
public class UIMananger : Singleton<UIMananger>
{
    // CanvasԤ�������ڵ�AB����
    public string pathUrl { get { return "ui"; } }
    // CanvasԤ������
    public string canvasName { get { return "MainCanvas"; } }

    public Transform mainCanvas; // ��Canvas
    // ���ϲ㣬����ϵͳ�㼶UI��������Ϸ�˵���
    private Transform systemTrans;
    // �ϲ�
    private Transform topTrans;
    // �в�
    private Transform midTrans;
    // �ײ�
    private Transform botTrans;
    // ������е�Panel����ŵ�Panel��Ҫ�̳�BasePanel
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    public UIMananger()
    {
        // ��AB���м���Canvas��Canvas����system, top, mid��bot4���㼶
        GameObject canvas = AssetbundleManager.GetInstance().LoadRes<GameObject>(pathUrl, canvasName);
        canvas.name = canvasName;
        GameObject.DontDestroyOnLoad(canvas);
        mainCanvas = canvas.transform;
        systemTrans = mainCanvas.Find("system");
        topTrans = mainCanvas.Find("top");
        midTrans = mainCanvas.Find("mid");
        botTrans = mainCanvas.Find("bot");

        GameObject eventSystem = AssetbundleManager.GetInstance().LoadRes<GameObject>(pathUrl, "EventSystem");
        eventSystem.name = "EventSystem";
        GameObject.DontDestroyOnLoad(eventSystem);
    }

    /// <summary>
    /// ��ʾĳ��panel����AB���м��ء�
    /// PanelӦ����AB���е�ĳ��Ԥ���壬��Ԥ������һ��Panel�����������һ������ΪT�Ľű����ýű��̳���BasePanel
    /// </summary>
    /// <param name="bundleName">AB����</param>
    /// <param name="panelName">panel��</param>
    /// <param name="layer">�����ĸ��㼶</param>
    /// <param name="callback">��������ɺ��������£���������Text��������Button��ɫ֮���</param>
    public void ShowPanel<T>(string bundleName, string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callback = null) where T : BasePanel
    {
        // ����ǰ������Ѿ���ʾ�ˣ���ֱ�ӵ���callbackʹ����
        if (panelDic.ContainsKey(panelName))
        {
            if (callback != null)
            {
                callback(panelDic[panelName] as T);
            }
            return;
        }
        // ���򣬴�������ʹ�ã����ﻹ�е����⣬�������첽����û�н���ʱ�������ط����첽�����ˣ���������
        AssetbundleManager.GetInstance().LoadResAsync<GameObject>(bundleName, panelName, (obj) => {
            Transform father = botTrans;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    father = midTrans;
                    break;
                case E_UI_Layer.Top:
                    father = topTrans;
                    break;
                case E_UI_Layer.System:
                    father = systemTrans;
                    break;
            }
            // ����panel�Ĵ�С��λ��
            obj.transform.SetParent(father);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            // ��ȡ���غõ�panelԤ�����ϵ�panel�ű�����Ҫ�̳�BasePanel��
            T panel = obj.GetComponent<T>();

            if (callback != null)
                callback(panel);

            panel.ShowSelf(); // �����䱻��ʾʱ�ķ���
            panelDic.Add(panelName, panel);
            
        });
    }

    /// <summary>
    /// �ر�ĳ��panel
    /// </summary>
    /// <param name="panelName">panel��</param>
    public void HidePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HielSelf(); // �䱻����
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// �����ƻ�ȡPanel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetPanel<T>(string name) where T : BasePanel
    {
        if (panelDic.ContainsKey(name))
        {
            return panelDic[name] as T;
        }
        else return null;

    }
}