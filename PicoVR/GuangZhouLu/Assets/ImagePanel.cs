using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImagePanel : MonoBehaviour , IPointerClickHandler {
    Image image;
    Image imageE;

    Vector2 size;
    Vector2 sizee;
	// Use this for initialization
    void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
        imageE = transform.Find("ImageE").GetComponent<Image>();

        size = image.rectTransform.rect.size;
        sizee = imageE.rectTransform.rect.size;
    }
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        OnDefaultSize();
        gameObject.SetActive(false);
    }
    public void OnDefaultSize()
    {
        image.rectTransform.sizeDelta = size;
        imageE.rectTransform.sizeDelta = sizee;
    }
    public void SetImage(Sprite img, Sprite imge)
    {
        ModifyImageSize(img, imge);
        if (img != null)
        {
            image.sprite = img;
            image.gameObject.SetActive(true);
        }
        else
        {
            image.gameObject.SetActive(false);
        }

        if (imge != null)
        {
            imageE.sprite = imge;
            imageE.gameObject.SetActive(true);
        }
        else
        {
            imageE.gameObject.SetActive(false);
        }
    }
    void ModifyImageSize(Sprite img, Sprite imge)
    {
        if (img != null)
        {
            float rate = img.rect.height / img.rect.width;
            float rateO = image.rectTransform.rect.height / image.rectTransform.rect.width;
            if (rate >= rateO)
            {//以高为准
                float h = img.rect.height / image.rectTransform.rect.size.y;
                image.rectTransform.sizeDelta = new Vector2(img.rect.width / h, image.rectTransform.rect.height);
            }
            else
            {//以宽为准
                float w = img.rect.width / image.rectTransform.rect.size.x;
                image.rectTransform.sizeDelta = new Vector2(image.rectTransform.rect.width, img.rect.height / w);
            }
        }

        if (imge != null)
        {
            float ratee = imge.rect.height / imge.rect.width;
            float rateeO = imageE.rectTransform.rect.height / imageE.rectTransform.rect.width;
            if (ratee >= rateeO)
            {//以高为准
                float he = imge.rect.height / imageE.rectTransform.rect.size.y;
                imageE.rectTransform.sizeDelta = new Vector2(imge.rect.width / he, imageE.rectTransform.rect.height);
            }
            else
            {//以宽为准
                float we = imge.rect.width / imageE.rectTransform.rect.size.x;
                imageE.rectTransform.sizeDelta = new Vector2(imageE.rectTransform.rect.width, imge.rect.height / we);
            }
        }
        else
        {
            //print("IMAGE EFFECT IS NULL");
        }
    }
}
