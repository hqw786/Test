using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

//把MainCamera搬到Titles/Logo/VideoPlayer/Camera

public class Lauch : MonoBehaviour {
    VideoPlayer vplayer;
    bool isFirst;
	// Use this for initialization
    void Awake()
    {
        vplayer = GetComponent<VideoPlayer>();
    }
	void Start () {
        vplayer.Play();
	}
	// Update is called once per frame
	void Update () {
		if(!vplayer.isPlaying)
        {
            SceneManager.LoadScene(1);
        }
        if(!isFirst)
        {
            Invoke("BGAlpha", 0.5f);//0.5秒后把背景变成透明（这样做可以黑背景过渡到视频播放，不会有天空盒闪过）
            isFirst = true;
        }
	}
    void BGAlpha()
    {
        Color c = GetComponent<RawImage>().color;
        c.a = 0;
        GetComponent<RawImage>().color = c;
    }
}
