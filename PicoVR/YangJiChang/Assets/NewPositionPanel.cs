using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewPositionPanel : MonoBehaviour ,IPointerClickHandler
{
    Button btnPoint1;
    public Transform point1;
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
    public void OnBtnPoint1Click()
    {
        MainManager.Instance.WarpToNewPosition(point1);
		btnPoint1.image.color = Color.red;
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerEnter.name == this.name || eventData.pointerEnter.transform.parent.name == this.name)
            gameObject.SetActive(false);
    }
}
