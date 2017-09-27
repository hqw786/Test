using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Tools
{
    /// <summary>
    /// 根据预置体名称实例化一个物体
    /// </summary>
    /// <param name="prefabName">预置体名称</param>
    /// <param name="position">位置</param>
    /// <param name="scale">缩放</param>
    /// <param name="rotation">旋转</param>
    /// <returns></returns>
    public static GameObject InstantiateGameObject(string prefabName, Vector3 position, Vector3 scale, Quaternion rotation)
    {
        GameObject g = Pool.Instance.GetObject(prefabName);
        g.SetActive(true);
        g.transform.position = position;
        g.transform.localScale = scale;
        g.transform.rotation = rotation;
        return g;
    }
}
