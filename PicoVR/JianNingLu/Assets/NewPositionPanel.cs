using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewPositionPanel : MonoBehaviour ,IPointerClickHandler
{
    List<Button> buttonList = new List<Button>();
    
    Button btnPoint1;
    public Transform point1;
    Button btnPoint2;
    public Transform point2;

    Button btnMineMap;
    Text mineMapName;

    // Use this for initialization
    void Awake()
    {
        btnPoint1 = transform.Find("BtnPoint1").GetComponent<Button>();
        btnPoint1.onClick.AddListener(OnBtnPoint1Click);
        buttonList.Add(btnPoint1);

        btnPoint2 = transform.Find("BtnPoint2").GetComponent<Button>();
        btnPoint2.onClick.AddListener(OnBtnPoint2Click);
        buttonList.Add(btnPoint2);

        btnMineMap = transform.Find("MapBG").transform.Find("BtnMineMap").GetComponent<Button>();
        btnMineMap.onClick.AddListener(OnBtnMineMapClick);
        mineMapName = btnMineMap.transform.Find("Text").GetComponent<Text>();
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void ButtonsDefault()
    {
        foreach(Button b in buttonList)
        {
			if (b.image.sprite != b.spriteState.disabledSprite)
				//b.image.sprite = b.spriteState.disabledSprite;
				b.image.color = Color.white;
        }
    }
    public void OnBtnPoint1Click()
    {
		MainManager.Instance.CloseAutoRoam();
        MainManager.Instance.WarpToNewPosition(point1);
        //其他按钮恢复默认
        ButtonsDefault();
        //点击的按钮变成红色
        //btnPoint1.image.sprite = btnPoint1.spriteState.pressedSprite;
		btnPoint1.image.color = Color.red;
        gameObject.SetActive(false);
    }
    public void OnBtnPoint2Click()
    {
		MainManager.Instance.CloseAutoRoam();
        MainManager.Instance.WarpToNewPosition(point2);
        //其他按钮恢复默认
        ButtonsDefault();
        //点击的按钮变成红色
        //btnPoint2.image.sprite = btnPoint2.spriteState.pressedSprite;
		btnPoint2.image.color = Color.red;
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name.Contains("BtnMineMap") || eventData.pointerEnter.transform.parent.name.Contains("BtnMineMap"))
        {
            return;
        }
		if (eventData.pointerEnter.name == this.name || eventData.pointerEnter.transform.parent.name == this.name)
		{
			if(MainManager.Instance.isAutoRoam)
			{
				transform.parent.Find("MenuPanel/BtnAutoRoam").transform.Find("Image").gameObject.SetActive(true);
				transform.parent.Find("MenuPanel/BtnSelectNewPosition").transform.Find("Image").gameObject.SetActive(false);
			}
			transform.parent.Find("MenuPanel/BtnSelectNewPosition").transform.Find("Image").gameObject.SetActive(false);
			gameObject.SetActive(false);
		}
    }
    void OnBtnMineMapClick()
    {
        if (mineMapName.text.Equals("显示小地图"))
        {
            MainManager.Instance.isShowMineMap = true;
            UIManager.Instance.ShowUI(Define.uiPanelMineMap, true);
            mineMapName.text = "隐藏小地图";
        }
        else
        {
            MainManager.Instance.isShowMineMap = false;
            UIManager.Instance.HideUI(Define.uiPanelMineMap);
            mineMapName.text = "显示小地图";
        }
        //mineMapName.text = mineMapName.text.Equals("显示小地图") ? "隐藏小地图" : "显示小地图";
    }
}
