using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SplitScreen : MonoBehaviour
{
    public int repeatTimes = 2; //分屏次数

    public Shader nRepeat; //shader实体

    private int halfTexWidth; //半屏大小 因为我们屏幕的大小是原分辨率的两倍 所以计算一半

    private static Texture2D rawTexture = null; // 原图

    private static Material m_Material = null;

    protected Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(nRepeat);
                m_Material.hideFlags = HideFlags.DontSave;
            }
            return m_Material;
        }
    }


    //启动的设置：
    void Awake()
    {
        halfTexWidth = Mathf.CeilToInt(Screen.width / 2f);
        material.mainTextureScale = new Vector2(repeatTimes, 1);
    }

    void OnDisable() //清理texture 不然会leak
    {
        if (Application.isEditor)
        {
            DestroyImmediate(rawTexture);
        }
        else if (Application.isPlaying)
        {
            Destroy(rawTexture);
        }

        rawTexture = null;
    }


    //接下来就是处理图片了：
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (repeatTimes < 2) repeatTimes = 2; //分屏2-6 可根据需要自己改
        if (repeatTimes > 6) repeatTimes = 6;

        //这里计算每一个屏幕的宽度
        int repeatedWidth = Mathf.CeilToInt((Screen.width + 0f) / repeatTimes);

        //因为原图的宽度是屏幕的一半 所以需要计算我们需要显示的分屏画面对原图的偏移
        float offset = (halfTexWidth - repeatedWidth) / 2f;

        //接下来就是声明texture 然后从source里面读像素
        rawTexture = new Texture2D(repeatedWidth, Screen.height, TextureFormat.ARGB32, false);
        rawTexture.ReadPixels(new Rect(Mathf.RoundToInt(Screen.width / 4f + offset), 0, rawTexture.width, rawTexture.height), 0, 0);
        rawTexture.Apply();

        //将平铺值赋给shader
        material.mainTextureScale = new Vector2(repeatTimes, 1);

        //输出
        Graphics.Blit(rawTexture, destination, material);

        //清理texture 不然会溢出
        if (Application.isEditor) RenderTexture.DestroyImmediate(rawTexture);
        else if (Application.isPlaying) RenderTexture.DestroyObject(rawTexture);
    }
}


