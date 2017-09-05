using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//对几个弹出界面的管理，只有一个是显示，其他都隐藏并恢复默认
public class UIManager : MonoBehaviour {
    public static event System.Action SwitchCameraEvent;
    public static event System.Action<Image> SwitchRoamEvent;

    List<GameObject> popupUIs = new List<GameObject>();
    List<Image> iconImages = new List<Image>();

    Transform map;
    Transform roam;
    Transform vr;
    Transform music;
    Transform music2;
    Transform comment;
    Transform share;
    Transform about;

    AudioSource audio;
    //List<Transform> iconScales = new List<Transform>();
	// Use this for initialization
    void Awake()
    {
        map = transform.Find("Icons/Map");
        roam = transform.Find("Icons/Roam");
        vr = transform.Find("Icons/VR");
        music = transform.Find("Icons/Music");
        music2 = transform.Find("Icons/Music2");
        music2.gameObject.SetActive(false);
        comment = transform.Find("Icons/Comment");
        share = transform.Find("Icons/Share");
        about = transform.Find("Icons/About");

        audio = transform.Find("Icons").GetComponent<AudioSource>();
    }
	void Start () {
		foreach(Transform temp in transform)
        {
            if(temp.name.Contains("Panel"))
            {
                popupUIs.Add(temp.gameObject);
            }
        }
        Transform icons = transform.Find("Icons");
        foreach(Transform temp in icons)
        {
            iconImages.Add(temp.GetComponent<Image>());
        }
	}
	// Update is called once per frame
	void Update () {
		
	}
    public void IconColorToDefault()
    {
        foreach(Image i in iconImages)
        {
            i.color = Color.white;
        }
    }
    public void IconsToHide()
    {
        foreach(Image i in iconImages)
        {
            i.gameObject.SetActive(false);
        }
    }
    public void PopupToDefault()
    {
        foreach(GameObject g in popupUIs)
        {
            g.SetActive(false);
        }
    }

    public void ClickIcon(string iconName)
    {
        foreach(GameObject g in popupUIs)
        {
            if(g.name.Contains(iconName))
            {
                g.SetActive(true);
                return;
            }
        }

        switch(iconName)
        {
            case "Map":

                break;
            case "Roam":
                if(SwitchRoamEvent != null)
                {
                    Image img = roam.GetComponent<Image>();
                    SwitchRoamEvent(img);

                    if(img.color == Color.white)
                    {
                        foreach(Image i in iconImages)
                        {
                            if(i.gameObject.name.Contains("Music2"))
                            {
                                if(!audio.isPlaying)
                                {
                                    i.gameObject.SetActive(true);
                                }
                            }
                            else if (i.gameObject.name.Contains("Music"))
                            {
                                if (audio.isPlaying)
                                {
                                    i.gameObject.SetActive(true);
                                }
                            }
                            else
                            {
                                i.gameObject.SetActive(true);
                            }
                        }
                    }
                    else
                    {
                        IconsToHide();
                        OnlyDisplayOneUI("Roam");
                    }
                }
                break;
            case "VR":
                if(SwitchCameraEvent != null)
                {
                    Image img = roam.GetComponent<Image>();
                    img.color = Color.white;
                    SwitchCameraEvent();
                }
                break;
            case "Music":
                music.gameObject.SetActive(false);
                music2.gameObject.SetActive(true);
                audio.Pause();
                break;
            case "Music2":
                music.gameObject.SetActive(true);
                music2.gameObject.SetActive(false);
                audio.Play();
                break;
        }
    }
    //只显示某个Icon
    void OnlyDisplayOneUI(string iconName)
    {
        foreach(Image i in iconImages)
        {
            if(i.gameObject.name.Contains(iconName))
            {
                i.gameObject.SetActive(true);
                return;
            }
        }
    }
}
