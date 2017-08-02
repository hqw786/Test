using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerImage : MonoBehaviour , IPointerClickHandler {
    Text text;
    Transform uiImage;
    Image image;

    ShowImageInfo sii;
	// Use this for initialization
    void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
        uiImage = transform.Find("/Canvas/ShowImagePanel/Image");
    }
	void Start () {
        image = uiImage.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        uiImage.gameObject.SetActive(true);
        image.sprite = sii.Img;
        image.SetNativeSize();
        eventData.pointerEnter.transform.parent.gameObject.SetActive(false);
    }

    public void SetShowImageInfo(ShowImageInfo sii)
    {
        this.sii = sii;
        text.text = this.sii.Name;
    }
}
