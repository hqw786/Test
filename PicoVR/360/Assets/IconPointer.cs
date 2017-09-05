using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconPointer : MonoBehaviour , IPointerClickHandler
{
    UIManager uimanager;
	// Use this for initialization
	void Start () {
        uimanager = transform.parent.parent.GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        uimanager.ClickIcon(eventData.pointerEnter.name);
    }
}
