using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewPositionPanel : MonoBehaviour ,IPointerClickHandler
{
    Button btnPoint1;
	// Use this for initialization
    void Awake()
    {
        btnPoint1 = transform.Find("BtnPoint1").GetComponent<Button>();
        btnPoint1.onClick.AddListener(OnBtnPoint1Click);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnBtnPoint1Click()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
