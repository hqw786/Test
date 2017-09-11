using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//对几个弹出界面的管理，只有一个是显示，其他都隐藏并恢复默认
public class UIManager : MonoBehaviour {
    public static event System.Action SwitchCameraEvent;
    public static event System.Action<Image> SwitchRoamEvent;

    List<GameObject> popupUIs = new List<GameObject>();
    List<GameObject> escUIs = new List<GameObject>();
    List<Image> iconImages = new List<Image>();

    Transform map;
    Transform roam;
    Transform vr;
    Transform music;
    Transform music2;
    Transform comment;
    Transform share;
    Transform about;

    Transform mapScale;
    public Transform miniMap;
    Transform flagPositions;

    AudioSource audio;

    Texture2D cursorTexture;

    float timerOfNoOperation;//无操作计时
    float timerOfNextMap;//进入下一地图时间
    bool isNoOperation;//是否无操作
    bool isNoOperationRoam;//是否无操作漫游
    [HideInInspector]
    public CameraManager cameraManager;
    [HideInInspector]
    public CameraMove mainCameraMove;
    [HideInInspector]
    public CameraMove vrCameraMove;
    [HideInInspector]
    public string curSkyboxName;
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

        mapScale = transform.Find("MapScale");
        miniMap = transform.Find("MiniMap");
        flagPositions = transform.Find("/FlagPositions");

        audio = transform.Find("Icons").GetComponent<AudioSource>();

        cursorTexture = Resources.Load<Texture2D>("Image/icon/Scale");
        Transform cameraSwitch = GameObject.Find("/CameraSwitch").transform;
        mainCameraMove = cameraSwitch.Find("MainCamera").GetComponent<CameraMove>();
        vrCameraMove = cameraSwitch.Find("VRCamera").GetComponent<CameraMove>();
        cameraManager = mainCameraMove.transform.parent.GetComponent<CameraManager>();

        escUIs.Add(miniMap.gameObject);
        escUIs.Add(mapScale.gameObject);
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
        //Invoke("FirstExecute",1f);
        FirstExecute();
	}
    void FirstExecute()
    {
        //ClickIcon("Map");
        ModifySkybox("cubemap11M");
    }
	// Update is called once per frame
	void Update () {
        if(isNoOperation)
        {
            timerOfNoOperation += Time.deltaTime;
            if (timerOfNoOperation >= 10f)
            {
                if (mainCameraMove.roamMode == RoamMode.Normal
                && vrCameraMove.roamMode == RoamMode.Normal)
                {
                    ClickIcon("Roam");
                    isNoOperationRoam = true;
                    mapScale.gameObject.SetActive(false);
                }
                if (isNoOperationRoam)
                {
                    timerOfNextMap += Time.deltaTime;
                    if (timerOfNextMap >= 50f)
                    {
                        //更换天空盒
                        MoveToNextSkybox();
                        //
                        timerOfNoOperation = 0F;
                        timerOfNextMap = 0F;
                        isNoOperationRoam = false;
                        Image img = roam.GetComponent<Image>();
                        SwitchRoamEvent(img);
                    }
                }
            }
        }
        else
        {
            timerOfNoOperation = 0f;
        }
        if(Input.anyKey || Input.anyKeyDown 
            || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0
            || Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            isNoOperation = false;
            //if(mainCameraMove.roamMode == RoamMode.Roam || vrCameraMove.roamMode == RoamMode.Roam)
            //{
            //    ClickIcon("Roam");
            //}
        }
        else
        {
            if (mainCameraMove.roamMode == RoamMode.Normal && vrCameraMove.roamMode == RoamMode.Normal)
            {
                isNoOperation = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsHavePopupUI())
            {
                foreach(GameObject g in popupUIs)
                {
                    g.SetActive(false);
                }
                
            }
            else if(IsHaveOtherUI())
            {
                foreach(GameObject g in escUIs)
                {
                    g.SetActive(false);
                }
                IconsToShow();
            }
            else if(IsAutoRoam())
            {
                ClickIcon("Roam");
            }
            else if (IsVrMode())
            {
                cameraManager.SwitchCameraMode();
            }
            else
            {
                Application.Quit();
            }
        }
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
    public void IconsToShow()
    {
        foreach (Image i in iconImages)
        {
            if (i.gameObject.name.Contains("Music2"))
            {
                if (!audio.isPlaying)
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
                mapScale.gameObject.SetActive(!mapScale.gameObject.activeInHierarchy);
                if (mapScale.gameObject.activeInHierarchy)
                {
                    IconsToHide();
                    OnlyDisplayOneUI("Map");
                }
                else
                {
                    IconsToShow();
                }
                break;
            case "Roam":
                isNoOperationRoam = false;
                if(SwitchRoamEvent != null)
                {
                    Image img = roam.GetComponent<Image>();
                    SwitchRoamEvent(img);

                    if(!roam.gameObject.activeInHierarchy)
                    {
                        IconsToShow();
                    }
                    else
                    {
                        IconsToHide();
                        //OnlyDisplayOneUI("Roam");
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

    //更换天空盒
    public void ModifySkybox(string skyboxName)
    {
        Material material = Resources.Load<Material>("cubemap/" + skyboxName);
        RenderSettings.skybox = material;
        curSkyboxName = skyboxName;
        DisplayFlag();
    }
    public void MoveToNextSkybox()
    {
        string n = RenderSettings.skybox.name;
        int index = -1;
        for(int i = 0; i < MapPanel.mapImageNames.Length;i++)
        {
            if(n.Contains(MapPanel.mapImageNames[i]))
            {
                index = i;
                break;
            }
        }
        if(index >= 0 && index < MapPanel.mapImageNames.Length -1)
        {
            curSkyboxName = MapPanel.mapImageNames[++index] + "M";
            ModifySkybox(curSkyboxName);
        }
        else if(index == MapPanel.mapImageNames.Length -1)
        {
            curSkyboxName = MapPanel.mapImageNames[0] + "M";
            ModifySkybox(curSkyboxName);
        }
    }
    void DisplayFlag()
    {
        if(curSkyboxName.Contains("cubemap11M"))
        {
            flagPositions.gameObject.SetActive(true);
        }
    }

    bool IsHavePopupUI()
    {
        bool b = false;
        foreach(GameObject g in popupUIs)
        {
            if(g.activeInHierarchy)
            {
                b = true;
                break;
            }
        }
        return b;
    }

    bool IsHaveOtherUI()
    {
        bool b = false;
        foreach(GameObject g in escUIs)
        {
            if(g.activeInHierarchy)
            {
                b = true;
                break;
            }
        }
        return b;
    }

    bool IsAutoRoam()
    {
        if(mainCameraMove.roamMode == RoamMode.Roam || vrCameraMove.roamMode == RoamMode.Roam)
        {
            return true;
        }
        return false;
    }

    bool IsVrMode()
    {
        if(cameraManager.mode == CameraMode.VR)
        {
            return true;
        }
        return false;
    }
    internal string GetSpotChineseName(string name)
    {
        string str = string.Empty;
        switch(name)
        {
            case "cubemap11M":
                str = "风景区航拍";
                break;
            case "cubemap_12M":
                str = "风景区广场";
                break;
            case "cubemap_13M":
                str = "千年古榕";
                break;
            case "cubemap_14M":
                str = "风情长廊";
                break;
            case "cubemap_15M":
                str = "风雨古桥";
                break;
            case "cubemap_16M":
                str = "出水洞口";
                break;
            case "cubemap_35M":
                str = "人间净土沿途";
                break;
            case "cubemap_36M":
                str = "人间净土";
                break;
        }
        return str;
    }
}
