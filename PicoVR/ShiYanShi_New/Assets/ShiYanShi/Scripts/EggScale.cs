using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScale : MonoBehaviour {
    [HideInInspector]
    public bool isScale;
    bool isKeepScale;
    Around around;
	// Use this for initialization
	void Start () {
        around = GetComponent<Around>();
        around.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isKeepScale)
        {
            Vector3 position = transform.localPosition;
            transform.localScale = Vector3.one * 2f;
            position.y = Mathf.Lerp(position.y,0.1f,0.05f);
            if(position.y >0.09f)
            {
                position.y = 0.1f;
                around.enabled = true;
            }
            transform.localPosition = position;
        }
        else
        {
            if (isScale)
            {
                isScale = false;
                transform.localScale = Vector3.one * 2f;
            }
            else
            {
                transform.localScale = Vector3.one * 1.5f;
                Vector3 position = transform.localPosition;
                position.y = Mathf.Lerp(position.y, 0f, 0.05f);
                if(position.y<0.01f)
                {
                    position.y = 0f;
                }
                transform.localPosition = position;
            }
        }
	}
    public void SetScale()
    {
        isScale = true;
    }

    public void keepScale()
    {
        isKeepScale = true;
    }
    public void resetKeeyScale()
    {
        isKeepScale = false;
        around.enabled = false;
        transform.rotation = Quaternion.identity;
    }

    public void OnEggPinkClick()
    {
        //SYSManager.Instance.OnBtnPinkClick();
        Transform t = transform.Find("/shiyanshi/Menu");
        if(t)
        {
            t.GetComponent<RayUI>().SetEggPress(this.gameObject);
        }
    }
    public void OnEggRedClick()
    {
        //SYSManager.Instance.OnBtnRedClick();
        Transform t = transform.Find("/shiyanshi/Menu");
        if (t)
        {
            t.GetComponent<RayUI>().SetEggPress(this.gameObject);
        }
    }
    public void OnEggGreenClick()
    {
        //SYSManager.Instance.OnBtnGreenClick();
        Transform t = transform.Find("/shiyanshi/Menu");
        if (t)
        {
            t.GetComponent<RayUI>().SetEggPress(this.gameObject);
        }
    }

    public void OnFodder1Click()
    {
        //SYSManager.Instance.OnBtnPinkClick();
        Transform t = transform.Find("/shiyanshi/Menu");
        if (t)
        {
            t.GetComponent<RayUI>().SetFodderPress(this.gameObject);
        }
    }
    public void OnFodder2Click()
    {
        //SYSManager.Instance.OnBtnRedClick();
        Transform t = transform.Find("/shiyanshi/Menu");
        if (t)
        {
            t.GetComponent<RayUI>().SetFodderPress(this.gameObject);
        }
    }
    public void OnFodder3Click()
    {
        //SYSManager.Instance.OnBtnGreenClick();
        Transform t = transform.Find("/shiyanshi/Menu");
        if (t)
        {
            t.GetComponent<RayUI>().SetFodderPress(this.gameObject);
        }
    }
}
