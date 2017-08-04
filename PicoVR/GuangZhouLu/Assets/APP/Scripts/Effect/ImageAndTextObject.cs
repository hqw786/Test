using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ImageAndTextObject<T> : MonoBehaviour where T : Graphic
{
    protected T obj;//是产生效果的UI对象
    public bool isFlicker = true;//是否闪烁（为真，只要显示就闪烁，为假，手动设置是否闪烁）
    
    protected bool isShowAlpha;//渐显,透明度
    protected bool isHideAlpha;//渐隐，透明度
    protected float originAlpha;//原始颜色和透明度（主要控制透明通道）
    protected Color color;
    protected float timerColorAlpha;//透明过渡时长
    protected bool isTwoWayAlpha;//是否双向
    protected float minAlpha;//最小透明值
    protected float maxAlpha;//最大透明值
    protected float twoWayAlphaTimer;//TODO:双向时长（最小到最大的过渡时长）以后再计算个公式出来
    protected float twoWayAlphaShowTimer;
    
    protected bool isShowColor;//渐变颜色
    protected bool isHideColor;//渐变颜色
    protected Color originColor;//原始大小
    protected float timerColor;//颜色过渡时长
    protected bool isTwoWayColor;//是否双向
    protected Color minColor;//最小颜色
    protected Color maxColor;//最大颜色
    protected float twoWayColorTimer;//TODO:双向时长（最小到最大的过渡时长）以后再计算个公式出来

    protected bool isBig;//变大
    protected bool isSmall;//变小
    protected Vector3 scale;//原始大小
    protected float timerScale;//缩放过渡时长
    protected bool isTwoWayScale;//是否双向
    protected Vector3 minScale = Vector3.zero;//设置最小大小
    protected Vector3 maxScale = Vector3.zero;//设置最大大小
    protected float twoWayScaleTimer;//TODO:双向时长（最小到最大的过渡时长）以后再计算个公式出来
       

    protected void Awake()
    {
        //设置对象
        SetObject();
    }
    public void SetObject()
    {
        obj = GetComponent<T>();//获得图片或文本组件
        color = obj.color;//获得原始颜色和透明度
        scale = obj.transform.localScale;//获得原始大小
    }
    protected void Start()
    {
        if(isFlicker)
            SetAlphaTransitionTwoWay(0.2f, 1f, 2f, 0f);
    }
    protected void Update()
    {
        if (obj == null) return;
        if (isShowAlpha)
        {
            ColorTransition(1f, ref isShowAlpha, true);
        }
        if (isHideAlpha)
        {
            ColorTransition(0f, ref isHideAlpha, false);
        }
        if (isBig)
        {
            ScaleTransition(maxScale, ref isBig, true);
        }
        if (isSmall)
        {
            ScaleTransition(minScale, ref isSmall, false);
        }
        if (isFlicker)
        {
            AlphaTransitionTwoWay();
            if(twoWayAlphaShowTimer > 0f)
            {
                Invoke("", twoWayAlphaShowTimer);
            }
        }
        else
        {
            if (isTwoWayAlpha)
            {
                AlphaTransitionTwoWay();
            }
        }
    }
    /// <summary>
    /// 颜色透明度过渡渐变
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
                obj.color = color;
            }
        }
        else
        {
            if (color.a <= 0.05f)
            {
                timerColor = 0f;
                value = false;
                color.a = alpha;
                obj.color = color;
                if (hide)
                {
                    obj.gameObject.SetActive(false);
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
        if (big)
        {
            if (obj.transform.localScale.x >= scale.x)
            {
                obj.transform.localScale = scale;
                value = false;
                timerScale = 0f;
            }
        }
        else
        {
            if (obj.transform.localScale.x <= scale.x)
            {
                obj.transform.localScale = scale;
                value = false;
                timerScale = 0f;
            }
        }
    }
    

    #region 透明
    public void SetAlphaTransitionOneWay()
    {
        
    }


    /// <summary>
    /// 设置双向透明过渡
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="transTime"></param>
    public void SetAlphaTransitionTwoWay(float min, float max, float transTime, float showTime)
    {
        isTwoWayAlpha = true;
        minAlpha = min;
        maxAlpha = max;
        twoWayAlphaTimer = transTime;
        twoWayAlphaShowTimer = showTime;
        color.a = minAlpha;
        obj.color = color;
    }
    void SetDefaultAlpha()
    {
        //将布尔变量设为默认值

        obj.color = color;
    }
    /// <summary>
    /// 双向透明过渡
    /// </summary>
    protected void AlphaTransitionTwoWay()
    {
        float temp = Mathf.PingPong(Time.time, twoWayAlphaTimer) / twoWayAlphaTimer;
        color.a = minAlpha + (maxAlpha - minAlpha) * temp;
        obj.color = color;
    }
    #endregion

    #region 颜色

    #endregion

    #region 缩放

    #endregion

    #region 功能分类

    
    
    public void SetShow()
    {
        if (obj == null)
            SetObject();
        SetDefaultHide();
        isShowAlpha = true;
        isHideAlpha = false;
    }
    public void SetHide()
    {
        if (obj == null)
            SetObject();
        SetDefaultShow();
        timerColor = 0f;
        //color.a = 0.36f;
        isHideAlpha = true;
        isShowAlpha = false;
    }
    protected void SetDefaultShow()
    {
        if (obj == null)
            SetObject();
        color.a = 1f;
        obj.color = color;
        isShowAlpha = false;
        isHideAlpha = false;
    }
    protected void SetDefaultShow(Color c)
    {
        if (obj == null)
            SetObject();
        color = c;
        color.a = 1f;
        obj.color = color;
        isShowAlpha = false;
        isHideAlpha = false;
    }
    protected void SetDefaultHide()
    {
        if (obj == null)
            SetObject();
        color.a = 0f;
        obj.color = color;
        isShowAlpha = false;
        isHideAlpha = false;
    }
    protected void SetDefaultHide(Color c)
    {
        if (obj == null)
            SetObject();
        color = c;
        color.a = 0f;
        obj.color = color;
        isShowAlpha = false;
        isHideAlpha = false;
    }

    protected void SetBig()
    {
        if (obj == null)
            SetObject();
        isBig = true;
        isSmall = false;
    }
    protected void SetSmall()
    {
        if (obj == null)
            SetObject();
        isBig = false;
        isSmall = true;
    }
    protected void SetDefaultScale()
    {
        if (obj == null)
            SetObject();
        obj.transform.localScale = scale;
    }
    protected void SetDefaultBig()
    {
        if (obj == null)
            SetObject();
        obj.transform.localScale = maxScale;
    }
    protected void SetDefaultBig(Vector3 max)
    {
        if (obj == null)
            SetObject();
        obj.transform.localScale = max;
    }
    protected void SetDefaultSmall()
    {
        if (obj == null)
            SetObject();
        obj.transform.localScale = minScale;
    }
    protected void SetDefaultSmall(Vector3 min)
    {
        if (obj == null)
            SetObject();
        obj.transform.localScale = min;
    }
    protected void SetMinAndMaxScale(Vector3 min, Vector3 max)
    {
        if (obj == null)
            SetObject();
        minScale = min;
        maxScale = max;
    }
    #endregion
}
