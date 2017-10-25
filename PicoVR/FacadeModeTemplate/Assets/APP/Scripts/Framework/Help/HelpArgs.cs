using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HelpMode
{
    DaA,//Distance and Angle,
    Collision,
    Time
}
public enum HelpActionMode
{
    TopHint,
    ToolTip,
    ToolTipToTask
}

public class HelpArgs : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (!isFirst && other.transform.tag.Contains("Player"))
        {
            if (playerCollision != null)
            {
                playerCollision(this);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!isFirst && collision.transform.tag.Contains("Player"))
        {
            if (playerCollision != null)
            {
                playerCollision(this);
            }
        }
    }

    public HelpMode mode;
//#if
    [Header("DaA:距离和角度")]
    public float distance;
    public float angle;
    [Header("Collision:碰撞和触发")]
    public string objectName;
    [Header("Time:时间长度")]
    public float time;
//#endif
    public HelpActionMode actionMode;
    [Header("TopHint:顶端提示")]
    public string TopContext;
    [Header("ToolTip:提示框")]
    public string context;
    [Header("ToolTipToTask:提示框，可加入任务栏")]
    public string taskContext;

    private bool isFirst;
    public event System.Action<HelpArgs> playerCollision;
    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        playerCollision += HelpManager.Instance.ExecuteHelpAction;
    }
}
