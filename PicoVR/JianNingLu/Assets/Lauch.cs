using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

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
            Invoke("BGAlpha", 0.5f);
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
