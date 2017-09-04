using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour {
    float tempSpeed;
    float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rotation();
	}
    void Rotation()
    {
        speed = GetRotationSpeed();
        transform.Rotate(Vector3.up, speed * SolarSystem.Instance.rate_Rotation * Time.deltaTime);
    }
    float GetRotationSpeed()
    {
        switch (this.name)
        {
            case "Sun":
                tempSpeed = SolarSystem.Instance.sunSpeed2;
                break;
            case "Mercury":
                tempSpeed = SolarSystem.Instance.mercurySpeed2;
                break;
            case "Venus":
                tempSpeed = SolarSystem.Instance.venusSpeed2;
                break;
            case "Earth":
                tempSpeed = SolarSystem.Instance.earthSpeed2;
                break;
            case "Moon":
                tempSpeed = SolarSystem.Instance.moonSpeed2;
                break;
            case "Mars":
                tempSpeed = SolarSystem.Instance.marsSpeed2;
                break;
            case "Jupiter":
                tempSpeed = SolarSystem.Instance.jupiterSpeed2;
                break;
            case "Saturn":
                tempSpeed = SolarSystem.Instance.saturnSpeed2;
                break;
            case "Uranus":
                tempSpeed = SolarSystem.Instance.uranusSpeed2;
                break;
            case "Neptune":
                tempSpeed = SolarSystem.Instance.neptuneSpeed2;
                break;
        }
        return tempSpeed;
    }
}
