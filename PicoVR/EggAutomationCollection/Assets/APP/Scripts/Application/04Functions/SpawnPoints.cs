using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//挂到某个物体上面
public class SpawnPoints : View //按预置点生成鸡蛋（还有一种，脚本放到所有鸡模型的父物体上，随机得子模型的鸡，从特写位置生成鸡）
{
    public GameObject prefabEgg;
    public Transform eggs;
    [Range(0.01f, 1)]
    public float spawnTime;
    [Range(1, 5)]
    public float spawnIntervalTime;
    /// <summary>
    /// 按鸡模型生成蛋
    /// </summary>
    /// <param name="chicken"></param>
    IEnumerator SpawnEgg(string chicken)
    {
        if (chicken.Equals("chicken"))
        {
            while (true)
            {
                int r = Random.Range(0, transform.childCount);
                InstantiationEgg(transform.GetChild(r));
                yield return new WaitForSeconds(spawnTime);
            }
        }
        yield return new WaitForSeconds(1f);
    }
    /// <summary>
    /// 按预置点生成蛋
    /// </summary>
    IEnumerator SpawnEgg()
    {
        while(true)
        {
            foreach(Transform temp in transform)
            {
                foreach (Transform t in temp)
                {
                    int r = Random.Range(0, 2);
                    if (r == 1)
                    {
                        InstantiationEgg(t);
                    }
                    yield return new WaitForSeconds(spawnTime);
                }
            }
            yield return new WaitForSeconds(spawnIntervalTime);
        }
    }
    void InstantiationEgg(Transform t)
    {
        GameObject g = Instantiate(prefabEgg);
        g.transform.parent = eggs;
        // g.transform.localScale = Vector3.one * 0.06f;
        g.transform.position = t.position;
    }
    void Awake()
    {

    }
    void Start()
    {
        //开始就执行，或者延时执行也可以，在协程中调。（如果要指定执行，可以在Updata里调。）
        
        
        
    }
    void Update()
    {

    }

    public override string Name
    {
        get { return Consts.V_SpawnPoints; }
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch(eventName)
        {
            case Consts.C_SpawnEgg:
                {
                    if (name.Contains("SpawnPoints"))
                    {
                        StartCoroutine(SpawnEgg());
                    }
                    else
                    {
                        StartCoroutine(SpawnEgg("chicken"));
                    }
                }
                break;
        }
    }
    public override void RegisterEvents()
    {
        attentionEvents.Add(Consts.C_SpawnEgg);
    }
}
