using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconPointer : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
{
    UIImageEffect uiie;
    UIManager uimanager;
	// Use this for initialization
	void Start () {
        uimanager = transform.parent.parent.GetComponent<UIManager>();
        uiie = GetComponent<UIImageEffect>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        uimanager.ClickIcon(eventData.pointerEnter.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name.Contains("Roam"))
        {
            if(uimanager.mainCameraMove.roamMode == RoamMode.Normal
                && uimanager.vrCameraMove.roamMode == RoamMode.Normal)
            {
                uiie.SetScaleOneWay(Vector3.one * 1.8f, Vector3.one * 1.5f, 0.15f);
                //uiie.SetColorOneWay(new Color(101f / 255f, 198f / 255f, 1f), Color.white, 0.5f);
            }
        }
        else
        {
            uiie.SetScaleOneWay(Vector3.one * 1.8f, Vector3.one * 1.5f, 0.15f);
            //uiie.SetColorOneWay(new Color(101f / 255f, 198f / 255f, 1f), Color.white, 0.5f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name.Contains("Roam"))
        {
            if (uimanager.mainCameraMove.roamMode == RoamMode.Normal
                && uimanager.vrCameraMove.roamMode == RoamMode.Normal)
            {
                uiie.SetScaleOneWay(Vector3.one * 1.5f, Vector3.one * 1.8f, 0.15f);
                //uiie.SetColorOneWay(Color.white, new Color(101f / 255f, 198f / 255f, 1f), 0.5f);
            }
        }
        else
        {
            uiie.SetScaleOneWay(Vector3.one * 1.5f, Vector3.one * 1.8f, 0.15f);
            //uiie.SetColorOneWay(Color.white, new Color(101f / 255f, 198f / 255f, 1f), 0.5f);
        }
    }
}
