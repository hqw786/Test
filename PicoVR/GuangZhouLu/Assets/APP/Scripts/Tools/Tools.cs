using System;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public static Vector3 WorldToUI(Vector3 point, Vector3 BoundaryPoint, Vector3 origin, float rate)
    {
        //计算距左下角点的距离
        float x = Mathf.Abs(point.x - BoundaryPoint.x) * rate;
        float y = Mathf.Abs(point.z - BoundaryPoint.z) * rate;

        Vector3 pos = new Vector3(origin.x + x, origin.y + y, 0);
        return pos;
    }
}