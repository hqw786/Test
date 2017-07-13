using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Lauch : MonoBehaviour {
    VideoPlayer vplayer;
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
	}
}
