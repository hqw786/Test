using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : ApplicationBase<Sound> 
{
    private AudioSource bgPlay;
    private AudioSource soundPlay;
	// Use this for initialization
    void Awake()
    {
        base.Awake();
        bgPlay = gameObject.AddComponent<AudioSource>();
        bgPlay.loop = true;
        soundPlay = gameObject.AddComponent<AudioSource>();
    }
	public void BGMPlay(string musicName)
    {
        bgPlay.clip = Resources.Load<AudioClip>("Music/" + musicName);
        bgPlay.Play();
    }
    public void SoundPlay(string musicName)
    {
        soundPlay.clip = Resources.Load<AudioClip>("Music/" + musicName);
        soundPlay.Play();
    }
}
