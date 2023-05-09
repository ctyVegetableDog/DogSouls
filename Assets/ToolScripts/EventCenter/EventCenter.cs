using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 事件中心，统一管理事件的订阅和发布
/// 改成带泛型的了，避免拆装箱，但是感觉增加了耦合
/// </summary>
public class EventCenter : Singleton<EventCenter>
{
    #region 将事件包裹起来，用里氏替换原则使其支持泛型
    // 事件列表里装父类，用的时候里氏替换
    private interface IEventInfo { }
    // 不带参数的泛型
    private class EventInfo : IEventInfo
    {
        public UnityAction actions;
        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }
    // 一个参数的泛型
    private class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;
        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }
    // 两个参数的泛型
    private class EventInfo<T, TT> : IEventInfo
    {
        public UnityAction<T, TT > actions;
        public EventInfo(UnityAction<T, TT> action)
        {
            actions += action;
        }
    }
    #endregion

    // 事件列表，先用字符串吧还是，得换
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
    #region 发布事件，分为有0，1，2个参数的事件
    /// <summary>
    /// 发布事件，该事件不需要参数
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="publisher">作为返回值传给事件的订阅者</param>
    public void PublishEvent(string eventName)
    {
        if (eventName is null || eventName == "") return;
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo e = eventDic[eventName] as EventInfo;
            if (e != null) e.actions();
            else throw new System.Exception("该事件名和参数类型不匹配");
        }
    }
    /// <summary>
    /// 发布事件，该事件需要发布者给其提供一个参数
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="publisher">作为返回值传给事件的订阅者</param>
    public void PublishEvent<T>(string eventName, T arg1)
    {
        if (eventName is null || eventName == "") return;
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T> e = eventDic[eventName] as EventInfo<T>;
            if (e != null) e.actions(arg1);
            else throw new System.Exception("该事件名和参数类型不匹配");
        }
    }
    /// <summary>
    /// 发布事件，该事件需要发布者给其提供两个参数
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="publisher">作为返回值传给事件的订阅者</param>
    public void PublishEvent<T, TT>(string eventName, T arg1, TT arg2)
    {
        if (eventName is null || eventName == "") return;
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T, TT> e = eventDic[eventName] as EventInfo<T, TT>;
            if (e != null) e.actions(arg1, arg2);
            else throw new System.Exception("该事件名和参数类型不匹配");
        }
    }
    #endregion
    #region 订阅事件，有0，1，2个参数的事件
    /// <summary>
    /// 订阅事件，不带参数
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="action">事件被发布时，该订阅者的回应</param>
    public void SubscribeEvent(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName)) 
        {
            EventInfo e = eventDic[eventName] as EventInfo;
            if (e != null) e.actions += action;
            else throw new System.Exception("该事件名已有其他泛型使用");
        }
        else eventDic.Add(eventName, new EventInfo(action));
    }
    /// <summary>
    /// 订阅事件，带一个参数
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="action">事件被发布时，该订阅者的回应</param>
    public void SubscribeEvent<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T> e = eventDic[eventName] as EventInfo<T>;
            if (e != null) e.actions += action;
            else throw new System.Exception("该事件名已有其他泛型使用");
        }
        else eventDic.Add(eventName, new EventInfo<T>(action));
    }
    /// <summary>
    /// 订阅事件，带两个参数
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="action">事件被发布时，该订阅者的回应</param>
    public void SubscribeEvent<T, TT>(string eventName, UnityAction<T, TT> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T, TT> e = eventDic[eventName] as EventInfo<T, TT>;
            if (e != null) e.actions += action;
            else throw new System.Exception("该事件名已有其他泛型使用");
        }
        else eventDic.Add(eventName, new EventInfo<T, TT>(action));
    }
    #endregion
    #region 取消订阅
    /// <summary>
    /// 取消订阅不带参数的事件
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="action">想要取消的已经订阅的函数名</param>
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
    /// 取消订阅带一个参数的事件
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="action">想要取消的已经订阅的函数名</param>
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
    /// 取消订阅带两个参数的事件
    /// </summary>
    /// <param name="eventName">事件名，使用需要修改string类，后面看看能不能改改，比如在用一个Unorderedset<string>来查询事件名是否存在</param>
    /// <param name="action">想要取消的已经订阅的函数名</param>
    public void UnsubscribeEvent<T, TT>(string eventName, UnityAction<T, TT> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            EventInfo<T, TT> e = eventDic[eventName] as EventInfo<T, TT>;
            if (e != null) e.actions -= action;
            if (e.actions == null) eventDic.Remove(eventName);
        }
    }
    // 清空事件列表
    public void Clear()
    {
        eventDic.Clear();
    }
    #endregion
}
