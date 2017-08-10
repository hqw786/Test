using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerImage : MonoBehaviour , IPointerClickHandler {

    Transform uiImage;
    //Image image;
    //Image imageE;

    ShowImageInfo sii;
	// Use this for initialization
    void Awake()
    {
        uiImage = transform.Find("/Canvas/ShowImagePanel/ImagePanel");
        //image = uiImage.Find("Image").GetComponent<Image>();
        //imageE = uiImage.Find("ImageE").GetComponent<Image>();
    }
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        uiImage.gameObject.SetActive(true);
        ImagePanel ip = uiImage.GetComponent<ImagePanel>();
        print(sii.ImgE);
        ip.SetImage(sii.Img, sii.ImgE);

        gameObject.SetActive(false);
    }

    public void SetShowImageInfo(ShowImageInfo sii)
    {
        this.sii = sii;
    }
}
