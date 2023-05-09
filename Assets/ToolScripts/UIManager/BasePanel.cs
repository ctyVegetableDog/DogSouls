using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Paneld���࣬������󶨵�Panel��
/// �ṩ�Ը�Panel��UI����Ĳ�ѯ�͸�Panel����ʾ����
/// </summary>
public abstract class BasePanel : MonoBehaviour
{
    // �����������UI�ؼ�����Ϊһ��GameObject�Ͽ��԰󶨶��UI�����������List�����Ƕ�������
    private Dictionary<string, List<UIBehaviour>> controllDic = new Dictionary<string, List<UIBehaviour>>();
    protected virtual void Awake()
    {
        FindAllChildren<Button>();
        FindAllChildren<Text>();
        FindAllChildren<Image>();
        FindAllChildren<Slider>();
    }
    /// <summary>
    /// ʹ������Ѱ�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    protected T GetChildrenByName<T>(string name) where T : UIBehaviour
    {
        if (controllDic.ContainsKey(name))
        {
            foreach (UIBehaviour each in controllDic[name])
            {
                if (each is T) return each as T;
            }
        }
        return null;
    }

    /// <summary>
    /// �ҵ���Panel��ĳ�����͵�ȫ��Ԫ��
    /// </summary>
    /// <typeparam name="T">Ԫ������</typeparam>
    private void FindAllChildren<T>() where T : UIBehaviour
    {
        // �ҵ�ȫ��Ԫ��
        T[] allChildren = this.GetComponentsInChildren<T>();
        string objName;
        foreach (T each in allChildren)
        {
            objName = each.gameObject.name;
            if (controllDic.ContainsKey(objName))
            {
                controllDic[objName].Add(each);
            }
            else
            {
                controllDic.Add(objName, new List<UIBehaviour>() { each });
            }
        }
    }
    // ��ʾ��panel
    public virtual void ShowSelf() { }
    // ���ظ�panel
    public virtual void HielSelf() { }

}