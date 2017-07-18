using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ExitButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
    GameObject image;
	// Use this for initialization
	void Start () {
        image = transform.Find("Image").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        image.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.SetActive(true);
    }
}
