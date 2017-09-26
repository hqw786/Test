using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum APPStatus
{
    Init,Start,Run,End,Exit
}
public class Facade : Singelation<Facade>
{
    Pool pool;
    Sound sound;
	// Use this for initialization
	void Awake()
    {
        pool = GetComponent<Pool>();
        sound = GetComponent<Sound>();
    }
    void Start () {
        pool.AddPool(Consts.SPN_monster1);
        for (int i = 0; i < 10; i++)
        {
            GameObject g = pool.GetObject(Consts.SPN_monster1);
            g.SetActive(true);
            g.transform.position = Vector3.zero + new Vector3(1f * i, 1f, 0f);
            g.transform.DOMove(Vector3.forward * 100f, 10f).SetLoops(3 , LoopType.Yoyo);
            g.transform.DOScale(Vector3.one * 1.5f, 5f).SetLoops(-1, LoopType.Yoyo);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
