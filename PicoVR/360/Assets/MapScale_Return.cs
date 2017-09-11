using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapScale_Return : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
{
    GameObject text;
    UIImageEffect uiie;
    UIManager uimanager;
	// Use this for initialization
    void Awake()
    {
        text = transform.Find("Text").gameObject;
        uiie = GetComponent<UIImageEffect>();
        uimanager = transform.parent.parent.GetComponent<UIManager>();
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
        uimanager.ClickIcon("Map");
    }
}
