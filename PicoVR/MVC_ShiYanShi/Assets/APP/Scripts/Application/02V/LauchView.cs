using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

//把MainCamera拖到Titles/Logo/VideoPlayer/Camera

public class LauchView : BaseView {
    VideoPlayer vplayer;
    bool isFirst;

    public override string Name
    {
        get
        {
            return Consts.V_StartLogo;//这边返回的是视图字符串
        }
    }

	// Use this for initialization
    void Awake()
    {
        vplayer = GetComponent<VideoPlayer>();
    }
	void Start () {
	}
	// Update is called once per frame
	void Update () {
        //当前帧和总帧数相参数5帧时，进入下一个场景
		if(vplayer.frame >= (long)vplayer.clip.frameCount - 5 && !isFirst)
        {
            isFirst = true;
            Game.Instance.MainStatusSwitch(MainGameStatus.Logo);
        }
        #region VideoPlayer关于帧的属性
        //else
        //{
        //    print(vplayer.isPlaying);
        //    print(vplayer.clip.frameRate);//帧率
        //    print(vplayer.clip.length);//秒数
        //    print(vplayer.frame);
        //    print(vplayer.frameCount);//总帧数
        //}
        #endregion
    }
    void BGAlpha()
    {
        Color c = GetComponent<RawImage>().color;
        c.a = 0;
        GetComponent<RawImage>().color = c;
    }

    public override void HandleEvent(string eventName, object data)
    {
        print("LauchView.HandleEvent:  " + eventName);
        switch (eventName)
        {
            case Consts.C_ShowMyLogo:
                {
                    vplayer.Play();
                    Invoke("BGAlpha", 0.5f);//0.5秒后把背景变成透明（这样做可以黑背景过渡到视频播放，不会有天空盒闪过）
                }
                break;
        }
    }
    public override void RegisterEvents()
    {
        //这里关心命令，不是视图
        print("LauchView.RegisterEvents:  " + Consts.C_ShowMyLogo);
        AttentionEvents.Add(Consts.C_ShowMyLogo);
    }
}
