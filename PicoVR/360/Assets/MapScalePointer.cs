using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapScalePointer : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
{
    public string skyboxName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        print(eventData.pointerEnter);
        GameObject.Find("/Canvas").GetComponent<UIManager>().ModifySkybox(skyboxName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}
