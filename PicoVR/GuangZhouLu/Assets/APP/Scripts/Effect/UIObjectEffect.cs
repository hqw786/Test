using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIObjectEffect<T> : MonoBehaviour where T : Graphic
{
    protected T obj;//是产生效果的UI对象
    public bool isAlphaFlicker = true;//是否透明闪烁（为真，只要显示就闪烁，为假，手动设置是否闪烁）
    public bool isColorFlicker = true;//是否颜色闪烁
    public bool isScaleFlicker = true;//是否缩放闪烁

    protected bool isAlpha;//渐显,透明度
    protected Color alpha;//
    protected Color originalAlpha;//原始颜色和透明度（主要控制透明通道）
    protected float timerAlpha;//透明过渡时长
    protected float aTime;//单向透明过渡计时
    protected bool isTwoWayAlpha;//是否双向
    protected float minAlpha;//最小透明值
    protected float maxAlpha;//最大透明值
    protected float twoWayAlphaTimer;//双向时间（最小到最大的过渡时长）
    protected float twoWayAlphaShowTimer;//TODO:双向显示时间(闪烁后什么效果隐藏还是不闪烁)，以后再说
    protected bool activeIsFalseOfHideOver;

    protected bool isColor;//渐变颜色
    protected Color color;
    protected Color originalColor;//原始颜色
    protected float timerColor;//颜色过渡时长
    protected float cTime;//单向颜色过渡计时
    protected bool isTwoWayColor;//是否双向
    protected Color minColor;//最小颜色
    protected Color maxColor;//最大颜色
    protected float twoWayColorTimer;//TODO:双向时长（最小到最大的过渡时长）以后再计算个公式出来
    protected float twoWayColorShowTimer;//TODO:双向显示时间(闪烁后什么效果隐藏还是不闪烁)，以后再说

    protected bool isScale;//变大
    protected Vector3 scale;
    protected Vector3 originalScale;//原始大小
    protected float timerScale;//缩放过渡时长
    protected float sTime;//单向绽放过渡计时
    protected bool isTwoWayScale;//是否双向
    protected Vector3 minScale = Vector3.zero;//设置最小大小
    protected Vector3 maxScale = Vector3.zero;//设置最大大小
    protected float twoWayScaleTimer;//TODO:双向时长（最小到最大的过渡时长）以后再计算个公式出来
    protected float twoWayScaleShowTimer;//TODO:双向显示时间(闪烁后什么效果隐藏还是不闪烁)，以后再说

    protected void Awake()
    {
        //设置对象
        SetObject();
    }
    protected void Start()
    {
        if (isAlphaFlicker)
            SetAlphaTwoWay(0.2f, 1f, 2f);
    }
    protected void Update()
    {
        if (obj == null) return;
        if (isAlpha)
        {
            AlphaTransitionOneWay();
        }
        if (isColor)
        {
            ColorTransitionOneWay();
        }
        if (isScale)
        {
            ScaleTransition();
        }
        if (isAlphaFlicker)
        {
            AlphaTransitionTwoWay();
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
    /// 绽放过渡渐变
    /// </summary>
    /// <param name="scale">最终值</param>
    /// <param name="value">要改变的变量</param>
    /// <param name="big">是否放大</param>
    protected void ScaleTransition()
    {
        sTime += Time.deltaTime;
        obj.transform.localScale = Vector3.Lerp(minScale, maxScale, sTime / timerScale);
        if (obj.transform.localScale == maxScale)
        {
            isScale = false;
            sTime = 0f;
        }
    }


    #region 透明
    /// <summary>
    /// 单向透明度过渡
    /// </summary>
    protected void AlphaTransitionOneWay()
    {
        aTime += Time.deltaTime;
        alpha.a = Mathf.Lerp(minAlpha, maxAlpha, aTime / timerAlpha);
        obj.color = alpha;
        if (alpha.a == maxAlpha)
        {
            isAlpha = false;
            aTime = 0f;
            if (maxAlpha < minAlpha && activeIsFalseOfHideOver)
            {
                activeIsFalseOfHideOver = false;
                gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 双向透明过渡
    /// </summary>
    protected void AlphaTransitionTwoWay()
    {
        float temp = Mathf.PingPong(Time.time, twoWayAlphaTimer) / twoWayAlphaTimer;
        alpha.a = minAlpha + (maxAlpha - minAlpha) * temp;
        obj.color = alpha;
    }

    //单向从指定透明度1到到指定透明度2
    public void SetAlphaOneWay(float min, float max, float transTime, bool active = false)
    {
        isAlpha = true;
        if (min >= 0f)
            minAlpha = min;
        else
            minAlpha = alpha.a;
        maxAlpha = max;
        timerAlpha = transTime;
        activeIsFalseOfHideOver = active;
        alpha.a = minAlpha;
        obj.color = alpha;
    }
    public void SetAlphaTwoWay(float min, float max, float transTime, float showTime = 0f)
    {
        isTwoWayAlpha = true;
        minAlpha = min;
        maxAlpha = max;
        twoWayAlphaTimer = transTime;
        twoWayAlphaShowTimer = showTime;
        alpha.a = minAlpha;
        obj.color = alpha;
    }
    //设置为某个透明度
    public void SetAlpha(float a)
    {
        alpha.a = a;
        obj.color = alpha;
    }
    public void SetAlphaDefault()
    {
        obj.color = originalAlpha;
    }
    #endregion
    

    #region 颜色
    /// <summary>
    /// 单向颜色过渡渐变
    /// </summary>
    protected void ColorTransitionOneWay()
    {
        cTime += Time.deltaTime;
        color = Color.Lerp(minColor, maxColor, cTime / timerColor);
        obj.color = color;
        if (color == maxColor)
        {
            isColor = false;
            cTime = 0f;
        }
    }
    /// <summary>
    /// 双向透明过渡
    /// </summary>
    protected void ColorTransitionTwoWay()
    {
        float temp = Mathf.PingPong(Time.time, twoWayColorTimer) / twoWayColorTimer;
        color = Color.Lerp(minColor, maxColor, temp);
        obj.color = color;
        if(color == maxColor)
        {
            Color c = maxColor;
            maxColor = minColor;
            minColor = c;
        }
    }
    public void SetColorOneWay(Color min, Color max, float transTime)
    {
        isColor = true;
        minColor = min;
        maxColor = max;
        timerColor = transTime;
        color = minColor;
        obj.color = color;
    }
    public void SetColorTwoWay(Color min, Color max, float transTime, float showTime = 0f)
    {
        isTwoWayColor = true;
        minColor = min;
        maxColor = max;
        twoWayColorTimer = transTime;
        twoWayColorShowTimer = showTime;
        color = minColor;
        obj.color = color;
    }
    //设置为某个颜色
    public void SetColor(Color c)
    {
        color = c;
        obj.color = color;
    }
    public void SetColorDefault()
    {
        obj.color = originalColor;
    }
    #endregion

    #region 缩放
    /// <summary>
    /// 单向缩放过渡
    /// </summary>
    protected void ScaleTransitionOneWay()
    {
        sTime += Time.deltaTime;
        scale = Vector3.Lerp(minScale, maxScale, sTime / timerScale);
        obj.transform.localScale = scale;
        if (scale == maxScale)
        {
            isScale = false;
            sTime = 0f;
        }
    }
    /// <summary>
    /// 双向缩放过渡
    /// </summary>
    protected void ScaleTransitionTwoWay()
    {
        float temp = Mathf.PingPong(Time.time, twoWayColorTimer) / twoWayColorTimer;
        scale = Vector3.Lerp(minScale, maxScale, temp);
        obj.transform.localScale = scale;
        if (scale == maxScale)
        {
            Vector3 v = maxScale;
            maxScale = minScale;
            minScale = v;
        }
    }
    public void SetScaleOneWay(Vector3 min, Vector3 max, float transTime)
    {
        isColor = true;
        minScale = min;
        maxScale = max;
        timerScale = transTime;
        scale = minScale;
        obj.transform.localScale = scale;
    }
    public void SetScaleTwoWay(Vector3 min, Vector3 max, float transTime, float showTime = 0f)
    {
        isTwoWayScale = true;
        minScale = min;
        maxScale = max;
        twoWayScaleTimer = transTime;
        twoWayScaleShowTimer = showTime;
        scale = minScale;
        obj.transform.localScale = scale;
    }
    //设置为某个大小
    public void SetScale(Vector3 v)
    {
        scale = v;
        obj.transform.localScale = scale;
    }
    public void SetScaleDefault()
    {
        obj.transform.localScale = originalScale;
    }
    #endregion
    public void SetObject()
    {
        obj = GetComponent<T>();//获得图片或文本组件
        color = obj.color;//获得原始颜色和透明度
        alpha = color;
        scale = obj.transform.localScale;//获得原始大小

        originalAlpha = alpha;
        originalColor = color;
        originalScale = scale;
    }
    //#region 基础功能
    //public void SetTwoWayTransition(float min, float max, float time)
    //{
    //    isTwoWayAlpha = true;
    //    minAlpha = min;
    //    maxAlpha = max;
    //    twoWayAlphaTimer = time;
    //    color.a = minAlpha;
    //    obj.color = color;
    //}

    //protected void SetShow()
    //{
    //    if (obj == null)
    //        SetObject();
    //    SetDefaultHide();
    //    isAlpha = true;
    //    isHideAlpha = false;
    //}
    //protected void SetHide()
    //{
    //    if (obj == null)
    //        SetObject();
    //    SetDefaultShow();
    //    isHideAlpha = true;
    //    isAlpha = false;
    //}
    //protected void SetDefaultShow()
    //{
    //    if (obj == null)
    //        SetObject();
    //    color.a = 1f;
    //    obj.color = color;
    //    isAlpha = false;
    //    isHideAlpha = false;
    //}
    //protected void SetDefaultShow(Color c)
    //{
    //    if (obj == null)
    //        SetObject();
    //    color = c;
    //    color.a = 1f;
    //    obj.color = color;
    //    isAlpha = false;
    //    isHideAlpha = false;
    //}
    //protected void SetDefaultHide()
    //{
    //    if (obj == null)
    //        SetObject();
    //    color.a = 0f;
    //    obj.color = color;
    //    isAlpha = false;
    //    isHideAlpha = false;
    //}
    //protected void SetDefaultHide(Color c)
    //{
    //    if (obj == null)
    //        SetObject();
    //    color = c;
    //    color.a = 0f;
    //    obj.color = color;
    //    isAlpha = false;
    //    isHideAlpha = false;
    //}

    //protected void SetBig()
    //{
    //    if (obj == null)
    //        SetObject();
    //    isScale = true;
    //    isSmall = false;
    //}
    //protected void SetSmall()
    //{
    //    if (obj == null)
    //        SetObject();
    //    isScale = false;
    //    isSmall = true;
    //}
    //protected void SetDefaultScale()
    //{
    //    if (obj == null)
    //        SetObject();
    //    obj.transform.localScale = scale;
    //}
    //protected void SetDefaultBig()
    //{
    //    if (obj == null)
    //        SetObject();
    //    obj.transform.localScale = maxScale;
    //}
    //protected void SetDefaultBig(Vector3 max)
    //{
    //    if (obj == null)
    //        SetObject();
    //    obj.transform.localScale = max;
    //}
    //protected void SetDefaultSmall()
    //{
    //    if (obj == null)
    //        SetObject();
    //    obj.transform.localScale = minScale;
    //}
    //protected void SetDefaultSmall(Vector3 min)
    //{
    //    if (obj == null)
    //        SetObject();
    //    obj.transform.localScale = min;
    //}
    //protected void SetMinAndMaxScale(Vector3 min, Vector3 max)
    //{
    //    if (obj == null)
    //        SetObject();
    //    minScale = min;
    //    maxScale = max;
    //}
    //#endregion
}
