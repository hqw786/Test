using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapScale_LeftRight : MonoBehaviour ,IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
{
    Scrollbar scroll;
    UIImageEffect uiie;
	// Use this for initialization
    void Awake()
    {
        uiie = GetComponent<UIImageEffect>();
        scroll = transform.parent.GetComponent<Scrollbar>();
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
        if(eventData.pointerEnter.name.Contains("Left"))
        {
            scroll.value -= 0.05f;
        }
        if(eventData.pointerEnter.name.Contains("Right"))
        {
            scroll.value += 0.05f;
        }
    }
}
