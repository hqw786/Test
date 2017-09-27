using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Sound
{
    #region 单例
    private static Sound instance;
    private Sound() { }
    public static Sound Instance
    {
        get
        {
            if (instance == null) instance = new Sound();
            return instance;
        }
    }
    #endregion;
    AudioClip bgm;
    AudioClip em;
    public void PlayBGM(string bgmName , Transform point = null)
    {
        bgm = Resources.Load<AudioClip>("Music/" + bgmName);
        if(point != null)
            AudioSource.PlayClipAtPoint(bgm, point.position);
        else
            AudioSource.PlayClipAtPoint(bgm, Vector3.zero);
    }
    public void PlayEffectMusic(string emName,Transform point = null)
    {
        em = Resources.Load<AudioClip>("Music/" + emName);
        if (point != null)
            AudioSource.PlayClipAtPoint(em, point.position);
        else
            AudioSource.PlayClipAtPoint(em, Vector3.zero);
    }
}
