using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {
    public static SolarSystem Instance;
    [Header("中心：")]
    public Transform sun;
    public Transform earth;
    [Header("公转速率：")]
    public float rate_Revolution;
    [Header("自转速率：")]
    public float rate_Rotation;
    [Header("行星的公转速度：")]
    public float mercurySpeed;
    public float venusSpeed;
    public float earthSpeed;
    public float moonSpeed;
    public float marsSpeed;
    public float jupiterSpeed;
    public float saturnSpeed;
    public float uranusSpeed;
    public float neptuneSpeed;
    [Header("行星的自转速度：")]
    public float sunSpeed2;
    public float mercurySpeed2;
    public float venusSpeed2;
    public float earthSpeed2;
    public float moonSpeed2;
    public float marsSpeed2;
    public float jupiterSpeed2;
    public float saturnSpeed2;
    public float uranusSpeed2;
    public float neptuneSpeed2;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
