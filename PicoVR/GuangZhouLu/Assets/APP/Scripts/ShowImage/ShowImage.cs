using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour 
{

    ShowImageInfo sii;
    [HideInInspector]
    public int id;
    [HideInInspector]
    public string name;
    [HideInInspector]
    public string icon;
    public Sprite image;


    Transform uiPointerPanel;//查看图片的Panel
    Image smallImage;//查看图片的小图片
    /// <summary>
    /// 大图片的Panel
    /// </summary>
    Transform uiShowPanel;//点击小图片后显示的大图片的Panel
    PointerImage pointerImage;//点击小图片的
    
    public void OnTriggerStay(Collider other)
    {
        //在触发器范围内，
    }

    public void OnTriggerExit(Collider other)
    {
        //离开触发器范围，UI隐藏
        uiPointerPanel.gameObject.SetActive(false);
        uiShowPanel.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        //进入触发器范围，显示UI
        UIManager.Instance.ShowUI(Define.uiPanelShowImage);
        uiPointerPanel.gameObject.SetActive(true);
        pointerImage.SetShowImageInfo(sii);
        //ImageAlphaScaleEffect iase = uiPointerPanel.Find("PointerImage").GetComponent<ImageAlphaScaleEffect>();
        //iase.SetTwoWayTransition(0.2f, 1f, 2f);
    }

	// Use this for initialization
    void Awake()
    {
        //取得相应的物体和组件
        sii = new ShowImageInfo() { ID = id, Name = name, Icon = icon, Img = image };
        uiPointerPanel = transform.Find("/Canvas/ShowImagePanel/PointerPanel");
        pointerImage = uiPointerPanel.GetComponent<PointerImage>();
        uiShowPanel = transform.Find("/Canvas/ShowImagePanel/ImagePanel");
    }
	void Start () {
		//取得相应的物体和组件

        //设置变量值

        //隐藏自身
        uiPointerPanel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
     
}
