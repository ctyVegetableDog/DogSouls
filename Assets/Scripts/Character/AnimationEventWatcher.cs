using UnityEngine;

/// <summary>
/// 绑定在带animator的组件上，负责监听动画事件
/// </summary>
public class AnimationEventWatcher : MonoBehaviour
{
    /// <summary>
    /// 监听不带参数的动画事件
    /// </summary>
    /// <param name="eventName">触发的事件名</param>
    private void AnimationEvent(string eventName)
    {
        EventCenter.GetInstance().PublishEvent(eventName); // 触发事件
    }

    /// <summary>
    /// 带当前脚本挂载的物体的动画事件
    /// 这里回调传回去的是挂载了该脚本的父节点，因为该脚本和animator一起挂在模型上，而模型作为根节点的直系子节点
    /// </summary>
    /// <param name="eventName"></param>
    private void AnimationEventWithObject(string eventName)
    {
        EventCenter.GetInstance().PublishEvent(eventName, this.transform.parent.gameObject);
    }
}
