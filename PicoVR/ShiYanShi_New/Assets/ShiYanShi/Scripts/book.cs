using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BookState
{
    normal,selected
}

public class book : MonoBehaviour {
    public static book Instance;

    public BookState bs;//书本状态，正常状态，选中状态

    public Color m_selectedColor;//书本的选中颜色
    public Color m_normalColor;//书本的正常颜色

    Vector3 m_selectedScale;//书本的选中缩放
    Vector3 m_normalScale;//书本的正常缩放

    Material ma;//书本的材质

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        bs = BookState.normal;
        ma = GetComponent<MeshRenderer>().material;
    }
	void Start () 
    {
        m_normalScale = this.transform.localScale;
        m_selectedScale = m_normalScale * 1.2f;
        f_changeState(BookState.normal);	
	}
    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="bs"></param>
	public void f_changeState(BookState bs)
    {
        this.bs = bs;
        if(this.bs == BookState.normal)
        {
            f_bookStateNormal();
        }
        else if(this.bs == BookState.selected)
        {
            f_bookStateSelected();
        }
    }
	// Update is called once per frame
	void Update () 
    {
		//if()
	}
    /// <summary>
    /// 正常状态
    /// </summary>
    void f_bookStateNormal()
    {
        transform.localScale = m_normalScale;
        ma.color = m_normalColor;
    }
    /// <summary>
    /// 选中状态
    /// </summary>
    void f_bookStateSelected()
    {
        transform.localScale = m_selectedScale;
        ma.color = m_selectedColor;
    }
    public void bookPointerEnter(GameObject go)
    {
        if (go.name == "book")
        {
            //if (bcBook == null)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
            //bcBook.enabled = false;
            //进入下一个流程
            //SYSManager.Instance.comeNextState();
            //Debug.Log("你按下了Book");
            //转到实验室，在协和里转了。以后要不要分离淡出，转向，淡入，这些步骤，有待观察
        }
    }
}
