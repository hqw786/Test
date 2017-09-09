using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggLaying : MonoBehaviour {
    public bool isLeftDir;
    public bool isRightDir;

    float RandLayEggTime;
    float timer;
    bool isLayEgg;

    GameObject eggPrefab;
    Transform parent;
	// Use this for initialization
    void Awake()
    {
        isLayEgg = false;
        eggPrefab = Resources.Load<GameObject>("Prefabs/jidan");
        parent = GameObject.Find("/Eggs").transform;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isLayEgg)
        {
            isLayEgg = false;
            RandLayEggTime = Random.Range(10, 21);
            //多少概率产蛋
            if(Random.Range(1,101) == 1)
            {
                //实例化鸡蛋
                GameObject egg = Instantiate(eggPrefab);
                egg.transform.parent = parent;
                egg.transform.localScale = Vector3.one;
                if(isLeftDir)
                {
                    egg.transform.position = transform.position + new Vector3(0f,0f,-0.1f);
                }
                if(isRightDir)
                {
                    egg.transform.position = transform.position + new Vector3(0f, 0f, 0.1f);
                }
            }
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >= RandLayEggTime)
            {
                timer = 0f;
                isLayEgg = true;
            }
        }
	}
}
