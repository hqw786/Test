using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour {
    public Transform parent;
    UIManager uimanager;
    Scrollbar scroll;
    ScrollRect scrollRect;

    public static string[] mapImageNames = new string[] 
        { "cubemap11", "cubemap_12", "cubemap_13", 
            "cubemap_14", "cubemap_15", "cubemap_16", 
            "cubemap_35", "cubemap_36" };
	// Use this for initialization
	void Start () {
        uimanager = transform.parent.GetComponent<UIManager>();
        scrollRect = transform.Find("Scroll View").GetComponent<ScrollRect>();
        scroll = scrollRect.transform.Find("Scrollbar Horizontal").GetComponent<Scrollbar>();
        LoadByIO();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void LoadByIO()
    {
        for (int i = 0; i < mapImageNames.Length; i++)
        {
            GameObject temp = Resources.Load<GameObject>("Prefabs/MapImage");
            string path = "cubemap/" + mapImageNames[i];
            object o = Resources.Load(path);
            Texture2D texture = Resources.Load(path) as Texture2D;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            GameObject g = Instantiate(temp);
            g.transform.parent = parent;
            g.transform.localScale = Vector3.one;
            Image image = g.GetComponent<Image>();
            image.sprite = sprite;
            g.transform.Find("Text").GetComponent<Text>().text = uimanager.GetSpotChineseName(mapImageNames[i] + "M");
            g.GetComponent<MapScalePointer>().skyboxName = mapImageNames[i] + "M";
        }
    }
    public void OnBtnLeftClick()
    {
        scroll.FindSelectableOnLeft();
        bool b = scrollRect.vertical;
    }
    public void OnBtnRightClick()
    {
        scroll.FindSelectableOnRight();
    }
    //public void OnClickLeft() 
    //{ // Debug.Log(center.index); 
    //    if(center.index < 1)
    //        center.index -= 1; 
    //    SpringPanel.Begin(scrollView.gameObject, new Vector3(-center.index * 1000,0,0),10); 
    //}
    //public void OnClickRight() 
    //{ //Debug.Log(center.index); 
    //    if(center.index > 1) 
    //        center.index += 1; 
    //    SpringPanel.Begin(scrollView.gameObject, new Vector3(-center.index * 1000,0,0),10); 
    //    // center.Recenter(); 
    //}
}
