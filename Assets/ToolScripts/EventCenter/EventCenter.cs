using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �¼����ģ�ͳһ�����¼��Ķ��ĺͷ���
/// �ĳɴ����͵��ˣ������װ�䣬���Ǹо����������
/// </summary>
public class EventCenter : Singleton<EventCenter>
{
    #region ���¼������������������滻ԭ��ʹ��֧�ַ���
    // �¼��б���װ���࣬�õ�ʱ�������滻
    private interface IEventInfo { }
    // ���������ķ���
    private class EventInfo : IEventInfo
    {
        public UnityAction actions;
        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }
    // һ�������ķ���
    private class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;
        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }
    // ���������ķ���
    private class EventInfo<T, TT> : IEventInfo
    {
        public UnityAction<T, TT > actions;
        public EventInfo(UnityAction<T, TT> action)
        {
            actions += action;
        }
    }
    #endregion

    // �¼��б������ַ����ɻ��ǣ��û�
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
    #region �����¼�����Ϊ��0��1��2���������¼�
    /// <summary>
    /// �����¼������¼�����Ҫ����
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="publisher">��Ϊ����ֵ�����¼��Ķ�����</param>
    public void PublishEvent(string eventName)
    {
        if (eventName is null || eventName == "") return;
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo e = eventDic[eventName] as EventInfo;
            if (e != null) e.actions();
            else throw new System.Exception("���¼����Ͳ������Ͳ�ƥ��");
        }
    }
    /// <summary>
    /// �����¼������¼���Ҫ�����߸����ṩһ������
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="publisher">��Ϊ����ֵ�����¼��Ķ�����</param>
    public void PublishEvent<T>(string eventName, T arg1)
    {
        if (eventName is null || eventName == "") return;
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T> e = eventDic[eventName] as EventInfo<T>;
            if (e != null) e.actions(arg1);
            else throw new System.Exception("���¼����Ͳ������Ͳ�ƥ��");
        }
    }
    /// <summary>
    /// �����¼������¼���Ҫ�����߸����ṩ��������
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="publisher">��Ϊ����ֵ�����¼��Ķ�����</param>
    public void PublishEvent<T, TT>(string eventName, T arg1, TT arg2)
    {
        if (eventName is null || eventName == "") return;
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T, TT> e = eventDic[eventName] as EventInfo<T, TT>;
            if (e != null) e.actions(arg1, arg2);
            else throw new System.Exception("���¼����Ͳ������Ͳ�ƥ��");
        }
    }
    #endregion
    #region �����¼�����0��1��2���������¼�
    /// <summary>
    /// �����¼�����������
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="action">�¼�������ʱ���ö����ߵĻ�Ӧ</param>
    public void SubscribeEvent(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName)) 
        {
            EventInfo e = eventDic[eventName] as EventInfo;
            if (e != null) e.actions += action;
            else throw new System.Exception("���¼���������������ʹ��");
        }
        else eventDic.Add(eventName, new EventInfo(action));
    }
    /// <summary>
    /// �����¼�����һ������
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="action">�¼�������ʱ���ö����ߵĻ�Ӧ</param>
    public void SubscribeEvent<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T> e = eventDic[eventName] as EventInfo<T>;
            if (e != null) e.actions += action;
            else throw new System.Exception("���¼���������������ʹ��");
        }
        else eventDic.Add(eventName, new EventInfo<T>(action));
    }
    /// <summary>
    /// �����¼�������������
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="action">�¼�������ʱ���ö����ߵĻ�Ӧ</param>
    public void SubscribeEvent<T, TT>(string eventName, UnityAction<T, TT> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T, TT> e = eventDic[eventName] as EventInfo<T, TT>;
            if (e != null) e.actions += action;
            else throw new System.Exception("���¼���������������ʹ��");
        }
        else eventDic.Add(eventName, new EventInfo<T, TT>(action));
    }
    #endregion
    #region ȡ������
    /// <summary>
    /// ȡ�����Ĳ����������¼�
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="action">��Ҫȡ�����Ѿ����ĵĺ�����</param>
    public void UnsubscribeEvent(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo e = eventDic[eventName] as EventInfo;
            if (e != null) e.actions -= action;
            if (e.actions == null) eventDic.Remove(eventName);
        }
    }
    /// <summary>
    /// ȡ�����Ĵ�һ���������¼�
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="action">��Ҫȡ�����Ѿ����ĵĺ�����</param>
    public void UnsubscribeEvent<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T> e = eventDic[eventName] as EventInfo<T>;
            if (e != null) e.actions -= action;
            if (e.actions == null) eventDic.Remove(eventName);
        }
    }
    /// <summary>
    /// ȡ�����Ĵ������������¼�
    /// </summary>
    /// <param name="eventName">�¼�����ʹ����Ҫ�޸�string�࣬���濴���ܲ��ܸĸģ���������һ��Unorderedset<string>����ѯ�¼����Ƿ����</param>
    /// <param name="action">��Ҫȡ�����Ѿ����ĵĺ�����</param>
    public void UnsubscribeEvent<T, TT>(string eventName, UnityAction<T, TT> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T, TT> e = eventDic[eventName] as EventInfo<T, TT>;
            if (e != null) e.actions -= action;
            if (e.actions == null) eventDic.Remove(eventName);
        }
    }
    // ����¼��б�
    public void Clear()
    {
        eventDic.Clear();
    }
    #endregion
}
