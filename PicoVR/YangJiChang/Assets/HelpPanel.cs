using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelpPanel : MonoBehaviour,IPointerClickHandler
{
    //Button btnHelp;
	// Use this for initialization
	void Start () {
		//btnHelp = transform.Find("HelpPanel")
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
    }
}
