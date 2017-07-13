using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowText : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler{
    GameObject text;
    bool isShow;
    bool isHide;
	// Use this for initialization
	void Start () {
        text = transform.Find("Image").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void FadeShow()
    {
        isShow = true;
        isHide = false;
    }
    public void FadeHide()
    {
        isShow = false;
        isHide = true;
    }


    void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
