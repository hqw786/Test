using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapScale_Map : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler ,IPointerExitHandler
{
    UIImageEffect uiie;
    Transform miniMap;
    UIManager uimanager;
	// Use this for initialization
    void Awake()
    {
        uiie = GetComponent<UIImageEffect>();
        uimanager = transform.parent.parent.GetComponent<UIManager>();
        miniMap = transform.parent.parent.Find("MiniMap");
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        uiie.SetScaleOneWay(Vector3.one * 1.2f, Vector3.one, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiie.SetScaleOneWay(Vector3.one, Vector3.one * 1.2f, 0.2f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //地图UI显示
        miniMap.gameObject.SetActive(true);
        uimanager.IconsToHide();
        //隐藏自身
        transform.parent.gameObject.SetActive(false);
    }
}
