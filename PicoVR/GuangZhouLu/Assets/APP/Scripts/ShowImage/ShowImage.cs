using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour 
{

    ShowImageInfo sii;

    public int id;
    public string name;
    public string icon;
    public Sprite image;


    Transform uiPointerPanel;//Image是Gameobject
    Transform uiShowImg;//是
    Image img;
    Text text;
    PointerImage pointerImage;
    
    public void OnTriggerStay(Collider other)
    {
        //在触发器范围内，
    }

    public void OnTriggerExit(Collider other)
    {
        //离开触发器范围，UI隐藏
        uiPointerPanel.gameObject.SetActive(false);
        uiShowImg.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        //进入触发器范围，显示UI
        uiPointerPanel.gameObject.SetActive(true);
        //text.text = sii.Name;
        pointerImage.SetShowImageInfo(sii);
        uiPointerPanel.GetComponent<UIImageFadeEffect>().SetTwoWayTransition(0.5f,1f);
    }

	// Use this for initialization
    void Awake()
    {
        //取得相应的物体和组件
        sii = new ShowImageInfo() { ID = id, Name = name, Icon = icon, Img = image };
        uiPointerPanel = transform.Find("/Canvas/ShowImagePanel/PointerPanel");
        text = uiPointerPanel.Find("PointerImage/Text").GetComponent<Text>();
        pointerImage = uiPointerPanel.Find("PointerImage").GetComponent<PointerImage>();
        
        uiShowImg = transform.Find("/Canvas/ShowImagePanel/Image");
        img = uiShowImg.GetComponent<Image>();
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
