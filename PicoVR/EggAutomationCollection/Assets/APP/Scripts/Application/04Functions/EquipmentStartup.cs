using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentStartup : View {
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("MainCamera"))
        {
            //SendEvent(Consts.C_EquipmentSatrtup);
            spawnPoint.SetActive(true);
            spawnPoint.GetComponent<SpawnPoints>().Spawn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("MainCamera"))
        {
            //SendEvent(Consts.C_EquipmentStop);
            spawnPoint.SetActive(false);
        }
    }

    public GameObject spawnPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override string Name
    {
        get { return Consts.V_EquipmentStartup; }
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch(eventName)
        {
            case Consts.C_EquipmentSatrtup:
                {
                    //SendEvent(Consts.C_SpawnEgg);
                    spawnPoint.SetActive(true);
                    //SendEvent(Consts.C_SpawnEgg);
                    spawnPoint.GetComponent<SpawnPoints>().Spawn();
                }
                break;
            case Consts.C_EquipmentStop:
                {
                    //spawnPoint.GetComponent<SpawnPoints>().StopAllCoroutines();
                    spawnPoint.SetActive(false);
                }
                break;
        }
    }
}
