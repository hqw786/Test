using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePanel : MonoBehaviour {
    float time = 1.5f;
    Transform textTransform;
    UIImageEffect uiiePanel;
    UITextEffect uiteText;
	// Use this for initialization
	void Start () {
        textTransform = transform.Find("Text");

        uiteText = textTransform.GetComponent<UITextEffect>();
        uiteText.SetAlphaOneWay(0f, 1f, time);
        Invoke("Hide", 2f);
        Invoke("LoadScene", 4f);
	}
	void Hide()
    {
        uiteText.SetAlphaOneWay(1f, 0f, time);
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
