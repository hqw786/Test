using System;
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
    private bool isKeepScale;//保持缩放
    
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
                mat.shader = shaderN;
            }
            mat.color = color;
        }
        else
        {
            if (color.a <= 0.05f)
            {
                timerColor = 0f;
                value  = false;
                color.a = alpha;
                if(hide)
                {
                    obj.SetActive(false);
                }
            }
            mat.color = color;
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
                value = false;
                timerScale = 0f;
            }
            obj.transform.localScale = scale;
        }
        else
        {
            if(obj.transform.localScale.x  <= scale.x)
            {
                value = false;
                timerScale = 0f;
            }
            obj.transform.localScale = scale;
        }
    }

    #region 基础功能
    #region 设置对象
    /// <summary>
    /// 设置对象为所挂物体
    /// </summary>
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
    /// <summary>
    /// 设置对象为指定对象
    /// </summary>
    /// <param name="g"></param>
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
    #endregion
    /// <summary>
    /// 保持缩放大小
    /// </summary>
    public void SetKeepScale()
    {
        isKeepScale = true;
    }
    public void resetKeeyScale()
    {
        isKeepScale = false;
        //这个根据情况是否使用
        //transform.rotation = Quaternion.identity;
    }
    #region 设置显示功能
    /// <summary>
    /// 显示
    /// </summary>
    public void SetShow()
    {
        isShow = true;
        isHide = false;
        mat.shader = shaderT;
        color.a = 0f;
        mat.color = color;
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public void SetHide()
    {
        isHide = true;
        isShow = false;
        mat.shader = shaderT;
    }
    /// <summary>
    /// 默认显示
    /// </summary>
    protected void SetDefaultShow()
    {
        color.a = 1f;
        mat.color = color;
    }
    /// <summary>
    /// 默认显示颜色
    /// </summary>
    /// <param name="c"></param>
    protected void SetDefaultShow(Color c)
    {
        color = c;
        color.a = 1f;
        mat.color = color;
    }
    /// <summary>
    /// 默认隐藏
    /// </summary>
    protected void SetDefaultHide()
    {
        color.a = 0f;
        mat.color = color;
    }
    /// <summary>
    /// 默认隐藏颜色
    /// </summary>
    /// <param name="c"></param>
    protected void SetDefaultHide(Color c)
    {
        color = c;
        color.a = 0f;
        mat.color = color;
    }
    #endregion
    #region 设置缩放功能
    /// <summary>
    /// 放大
    /// </summary>
    public void SetBig()
    {
        isBig = true;
        isSmall = false;
    }
    /// <summary>
    /// 缩小
    /// </summary>
    public void SetSmall()
    {
        isBig = false;
        isSmall = true;
    }
    /// <summary>
    /// 恢复到默认大小
    /// </summary>
    protected void SetDefaultScale()
    {
        obj.transform.localScale = scale;
    }
    /// <summary>
    /// 默认放大
    /// </summary>
    protected void SetDefaultBig()
    {
        obj.transform.localScale = maxScale;
    }
    /// <summary>
    /// 默认放大到指定大小
    /// </summary>
    /// <param name="max"></param>
    protected void SetDefaultBig(Vector3 max)
    {
        obj.transform.localScale = max;
    }
    /// <summary>
    /// 默认缩小
    /// </summary>
    protected void SetDefaultSmall()
    {
        obj.transform.localScale = minScale;
    }
    /// <summary>
    /// 默认缩小到指定大小
    /// </summary>
    /// <param name="min"></param>
    protected void SetDefaultSmall(Vector3 min)
    {
        obj.transform.localScale = min;
    }
    /// <summary>
    /// 设置缩放的最大最小值
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    protected void SetMinAndMaxScale(Vector3 min, Vector3 max)
    {
        minScale = min;
        maxScale = max;
    }
    #endregion
    #endregion
}
