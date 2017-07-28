﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class OBJFadeEffect_Base : MonoBehaviour   // where T : Material
{
    protected bool isShow;//渐显
    protected bool isHide;//渐隐

    protected bool isBig;//变大
    protected bool isSmall;//变小

    protected Vector3 minScale = Vector3.zero;
    protected Vector3 maxScale = Vector3.zero;

    protected Color color;//颜色（主要控制透明通道）
    protected Vector3 scale;
    protected GameObject obj;
    protected Material mat;

    protected Shader shaderT;
    protected Shader shaderN;

    float timerColor;
    float timerScale;

    protected void Awake()
    {

    }
    protected void Start()
    {
        
    }
    protected void Update()
    {
        if (mat == null) return;
        if(isShow)
        {
            ColorTransition(1f, ref isShow, true);
        }
        if(isHide)
        {
            ColorTransition(0f, ref isHide, false, true);
        }
        if(isBig)
        {
            ScaleTransition(maxScale, ref isBig, true);
        }
        if(isSmall)
        {
            ScaleTransition(minScale, ref isSmall, false);
        }
    }
    /// <summary>
    /// 颜色过渡渐变
    /// </summary>
    /// <param name="alpha">最终值</param>
    /// <param name="value">要改变的变量</param>
    /// <param name="show">是否显示</param>
    /// <param name="hide">GameObject是否隐藏</param>
    protected void ColorTransition(float alpha, ref bool value, bool show, bool hide = false)
    {
        timerColor += Time.deltaTime;
        color.a = Mathf.Lerp(color.a, alpha, timerColor / ConfigData.fadeTime);
        if (show)
        {
            if (color.a >= 0.95f)
            {
                timerColor = 0f;
                value = false;
                color.a = alpha;
                mat.color = color;
                mat.shader = shaderN;
            }
        }
        else
        {
            if (color.a <= 0.05f)
            {
                timerColor = 0f;
                value  = false;
                color.a = alpha;
                mat.color = color;
                //material.shader = shaderN;
                if(hide)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
    /// <summary>
    /// 绽放过渡渐变
    /// </summary>
    /// <param name="scale">最终值</param>
    /// <param name="value">要改变的变量</param>
    /// <param name="big">是否放大</param>
    protected void ScaleTransition(Vector3 scale, ref bool value, bool big)
    {
        timerScale += Time.deltaTime;
        obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, scale, timerScale / ConfigData.scaleTime);
        if(big)
        {
            if(obj.transform.localScale.x >= scale.x)
            {
                obj.transform.localScale = scale;
                value = false;
                timerScale = 0f;
            }
        }
        else
        {
            if(obj.transform.localScale.x  <= scale.x)
            {
                obj.transform.localScale = scale;
                value = false;
                timerScale = 0f;
            }
        }
    }

    #region 基础功能
    protected void SetObject()
    {
        shaderN = SYSManager.Instance.shaderN;
        shaderT = SYSManager.Instance.shaderT;

        obj = this.gameObject;
        mat = obj.GetComponent<MeshRenderer>().material;
        mat.shader = shaderT;
        if (mat == null) Debug.LogError("材质为空");
        color = mat.color;
        scale = obj.transform.localScale;
    }
    protected void SetObject(GameObject g)
    {
        shaderN = SYSManager.Instance.shaderN;
        shaderT = SYSManager.Instance.shaderT;

        obj = g;
        mat = obj.GetComponent<MeshRenderer>().material;
        mat.shader = shaderT;
        if (mat == null) Debug.LogError("材质为空");
        color = mat.color;
        scale = obj.transform.localScale;
    }
    public void SetShow()
    {
        isShow = true;
        isHide = false;
        mat.shader = shaderT;
    }
    public void SetHide()
    {
        isHide = true;
        isShow = false;
        mat.shader = shaderT;
    }
    protected void SetDefaultShow()
    {
        color.a = 1f;
        mat.color = color;
    }
    protected void SetDefaultShow(Color c)
    {
        color = c;
        color.a = 1f;
        mat.color = color;
    }
    protected void SetDefaultHide()
    {
        color.a = 0f;
        mat.color = color;
    }
    protected void SetDefaultHide(Color c)
    {
        color = c;
        color.a = 0f;
        mat.color = color;
    }

    public void SetBig()
    {
        isBig = true;
        isSmall = false;
    }
    public void SetSmall()
    {
        isBig = false;
        isSmall = true;
    }
    protected void SetDefaultScale()
    {
        obj.transform.localScale = scale;
    }
    protected void SetDefaultBig()
    {
        obj.transform.localScale = maxScale;
    }
    protected void SetDefaultBig(Vector3 max)
    {
        obj.transform.localScale = max;
    }
    protected void SetDefaultSmall()
    {
        obj.transform.localScale = minScale;
    }
    protected void SetDefaultSmall(Vector3 min)
    {
        obj.transform.localScale = min;
    }
    protected void SetMinAndMaxScale(Vector3 min, Vector3 max)
    {
        minScale = min;
        maxScale = max;
    }
    #endregion
}
