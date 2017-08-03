using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UITextFadeEffect : MonoBehaviour
{
    public bool isFlicker = true;
    protected bool isShow;//渐显
    protected bool isHide;//渐隐

    protected bool isBig;//变大
    protected bool isSmall;//变小

    protected Vector3 minScale = Vector3.zero;
    protected Vector3 maxScale = Vector3.zero;

    protected Color color;//颜色（主要控制透明通道）
    protected Vector3 scale;
    protected Text obj;

    float timerColor;
    float timerScale;
    private bool isTwoWayAlpha;//
    private float minAlpha;//
    private float maxAlpha;//
    private float twoWayAlphaTimer;//

    protected void Awake()
    {
        SetObject();
    }
    protected void Start()
    {
        SetTwoWayTransition(0.5f,1f,2f);
    }
    protected void Update()
    {
        if (obj == null) return;
        if(isShow)
        {
            ColorTransition(1f, ref isShow, true);
        }
        if(isHide)
        {
            ColorTransition(0f, ref isHide, false);
        }
        if(isBig)
        {
            ScaleTransition(maxScale, ref isBig, true);
        }
        if(isSmall)
        {
            ScaleTransition(minScale, ref isSmall, false);
        }
        if (isFlicker)
        {
            ColorAlphaTransition();
        }
        else
        {
            if(isTwoWayAlpha)
            {
                ColorAlphaTransition();
            }
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
                obj.color = color;
            }
        }
        else
        {
            if (color.a <= 0.05f)
            {
                timerColor = 0f;
                value  = false;
                color.a = alpha;
                obj.color = color;
                if(hide)
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
    protected void ColorAlphaTransition()
    {
        float a = Mathf.PingPong(Time.time, (maxAlpha - minAlpha)) + minAlpha;
        color.a = a / twoWayAlphaTimer;
        obj.color = color;

    }

    #region 基础功能
    public void SetTwoWayTransition(float min, float max, float time)
    {
        if (obj == null)
            SetObject();
        isTwoWayAlpha = true;
        minAlpha = min;
        maxAlpha = max;
        twoWayAlphaTimer = time;
        color.a = minAlpha;
        obj.color = color;
    }
    protected void SetObject()
    {
        obj = GetComponent<Text>();
        color = obj.color;
        scale = obj.transform.localScale;
    }
    protected void SetShow()
    {
        if (obj == null)
            SetObject();
        SetDefaultHide();
        isShow = true;
        isHide = false;
    }
    protected void SetHide()
    {
        if (obj == null)
            SetObject();
        SetDefaultShow();
        isHide = true;
        isShow = false;
    }
    protected void SetDefaultShow()
    {
        if (obj == null)
            SetObject();
        color.a = 1f;
        obj.color = color;
        isShow = false;
        isHide = false;
    }
    protected void SetDefaultShow(Color c)
    {
        if (obj == null)
            SetObject();
        color = c;
        color.a = 1f;
        obj.color = color;
        isShow = false;
        isHide = false;
    }
    protected void SetDefaultHide()
    {
        if (obj == null)
            SetObject();
        color.a = 0f;
        obj.color = color;
        isShow = false;
        isHide = false;
    }
    protected void SetDefaultHide(Color c)
    {
        if (obj == null)
            SetObject();
        color = c;
        color.a = 0f;
        obj.color = color;
        isShow = false;
        isHide = false;
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
