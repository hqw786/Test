using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {
    List<MiniMapPointInfo> spotInfos = new List<MiniMapPointInfo>();
    List<Image> spotImages = new List<Image>();

    UIManager uimanager;
	// Use this for initialization
	void Awake()
    {
        Transform temp = transform.Find("MapBG");
        foreach(Transform t in temp)
        {
            spotInfos.Add(t.GetComponent<MiniMapPointInfo>());
            spotImages.Add(t.GetComponent<Image>());
        }
        uimanager = transform.parent.GetComponent<UIManager>();
    }
    void Start () 
    {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DisplayCurSpotColor(Sprite dsprite)
    {
        for (int i = 0; i < spotInfos.Count; i++)
        {
            spotImages[i].sprite = dsprite;
        }
    }
    public void OnBtnCloseClick()
    {
        uimanager.IconsToShow();
        gameObject.SetActive(false);
    }
}
