using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缓存池避免游戏对象的频繁创建
/// </summary>
public class Pool : Singleton<Pool>
{
    #region 单一元素池
    /// <summary>
    /// 给Pool使用，单一元素池，同名且同路径的GameObject被管理到一个池里
    /// 不要直接使用
    /// </summary>
    private class ObjectDrawer
    {
        // 所有单一种类游戏对象的父节点
        private GameObject parent;
        // 单一对象池
        private Queue<GameObject> objectQueue;
        // 计数
        public int Count { get { return objectQueue.Count; } }

        /// <summary>
        /// 初始化单一元素池
        /// </summary>
        /// <param name="root">整个缓存池的根节点，将单一元素池的根节点交给其</param>
        /// <param name="rootName">该单一元素池名</param>
        public ObjectDrawer(GameObject root, string rootName)
        {
            // 初始化该单一池的根节点，并指定其根节点为缓存池的父节点
            parent = new GameObject();
            parent.name = rootName;
            parent.transform.SetParent(root.transform);

            // 初始化该类元素池
            objectQueue = new Queue<GameObject>(50);
        }

        /// <summary>
        /// 物体入池
        /// </summary>
        /// <param name="obj">入池的物体</param>
        public void Push(GameObject obj)
        {
            // 将该物体禁用（可以考虑是否移动到远处）
            obj.SetActive(false);
            obj.transform.SetParent(parent.transform);
            objectQueue.Enqueue(obj);
        }

        /// <summary>
        /// 从池中获取物体
        /// </summary>
        /// <returns>获取的物体</returns>
        public GameObject Get()
        {
            GameObject obj = objectQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
    }

    #endregion


    // 缓存池
    private Dictionary<string, ObjectDrawer> objectDic = new Dictionary<string, ObjectDrawer>();
    // 给所有使用缓存池的元素添加根节点，方便管理
    private GameObject root = null;
    /// <summary>
    /// 元素入池，只有用Get方法拿出来的元素才可以入池，不然名字不对，入池元素的名字得是AB包路径名/元素名
    /// </summary>
    /// <param name="name">元素在AB包下的路径/元素类型名，其实从元素池中拿出来的元素的名字自己就叫这个了，所以直接传name就行</param>
    /// <param name="obj">入池元素</param>
    public void Push(string name, GameObject obj)
    {
        // 若没有根节点需要先初始化根节点
        if (root == null)
        {
            root = new GameObject("Pool_root");
        }
        // 若没有该单一元素池，则初始化
        if (!objectDic.ContainsKey(name))
        {
            objectDic[name] = new ObjectDrawer(root, name);
        }
        // 元素入池
        objectDic[name].Push(obj);
    }
    /// <summary>
    /// 从缓存池中取出元素，如果缓存池中不存在该元素，则改为从AB里加载
    /// </summary>
    /// <param name="path">元素所在的AB包名</param>>
    /// <param name="name">元素类型名称，元素路径格式为path/name</param>
    /// <returns>取出的元素</returns>
    public GameObject Get(string path, string name)
    {
        // 有个烂主意可以避免这下GC，使用二级字典[path][name]定位到一个元素
        string assetUrl = string.Format("{0}/{1}", path, name);
        GameObject obj = null;
        // 如果该池存在且还有元素，说明可以从该池中取出元素
        if (objectDic.ContainsKey(assetUrl) && objectDic[assetUrl].Count > 0)
        {
            obj = objectDic[assetUrl].Get();
        }
        // 否则，新创建一个元素
        else
        {
            // 从AB包加载资源
            obj = AssetbundleManager.GetInstance().LoadRes<GameObject>(path, name); // 这里就会创建了
            obj.name = assetUrl;
        }
        return obj;
    }
    /// <summary>
    /// 清空缓存池
    /// </summary>
    public void Clear()
    {
        objectDic.Clear(); // 清空列表
        GameObject.Destroy(root); //删除缓存池管理的所有元素
        root = null; //清空根节点
    }
}
