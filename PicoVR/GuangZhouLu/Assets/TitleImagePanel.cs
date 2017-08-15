using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleImagePanel : MonoBehaviour {
    float time = 1.5f;
    Transform textTransform;
    UIImageEffect uiiePanel;
    UITextEffect uitePanel;
    //UITextEffect uiteText;
	// Use this for initialization
	void Start () {
        textTransform = transform.Find("Image");

        uiiePanel = textTransform.GetComponent<UIImageEffect>();
        uitePanel = transform.Find("Text").GetComponent<UITextEffect>();

        uiiePanel.SetAlphaOneWay(0f, 1f, time);
        uitePanel.SetAlphaOneWay(0f, 1f, time);

        Invoke("Hide", 4f);
        Invoke("LoadScene", 6f);
	}
	void Hide()
    {
        uiiePanel.SetAlphaOneWay(1f, 0f, time);
        uitePanel.SetAlphaOneWay(1f, 0f, time);
    }
	// Update is called once per frame
	void Update () {
        
	}
    //增加一些提示功能，并显示帮助
    void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}
