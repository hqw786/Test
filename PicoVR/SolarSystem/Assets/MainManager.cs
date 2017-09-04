using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : View {
    public static MainManager Instance;
    Camera camera;
    Transform xingQiu; 
    List<Animation> animList = new List<Animation>();
    public event System.Action<float> AnimationAddSpeedEvent;
    public event System.Action AnimationNormalSpeedEvent;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
        camera = Camera.main;
        xingQiu = transform.Find("/xingqiu");
    }
	void Start () {

        if(AnimationAddSpeedEvent != null)
        {
            float rand = Random.Range(20, 50);
            AnimationAddSpeedEvent(rand);
        }
        Invoke("Display", 3f);

        //DONE:临时使用，调试完成或发布时注释
        MVC.RegisterControl(Consts.C_EnterScene, typeof(EnterSceneCommand));
        
        SceneArgs sa = new SceneArgs();
        sa.SceneIndex = 5;
        SendEvent(Consts.C_EnterScene, sa);

        SendEvent(Consts.C_LatencyHiding);
        //
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene().name.Contains("Level"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Transform temp = transform.Find("/Canvas");
                Transform bi = temp.Find("BlackImage");
                Transform pp = temp.Find("PlanetPanel");
                if (!bi.gameObject.activeInHierarchy
                && !pp.gameObject.activeInHierarchy)
                {
                    pp.gameObject.SetActive(true);
                    SendEvent(Consts.C_ShowPlanet);
                }
            }
        }
	}
    void Display()
    {
        if(AnimationNormalSpeedEvent != null)
        {
            AnimationNormalSpeedEvent();
        }
    }

    public override string Name
    {
        get { return string.Empty; }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }
}
