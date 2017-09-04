using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRevolution : MonoBehaviour {
    float speed;
    float tempSpeed;
    Transform centre;
    Transform tempCentre;
    Vector3 tempDirection;
    Vector3 direction;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Revolution();
	}

    void Revolution()
    {
        speed = GetRevolutionSpeed();
        centre = GetRevolutionCentre();
        direction = GetRevolutionDirection();
        if (!this.name.Contains("Moon_"))
        {
            transform.RotateAround(centre.position, direction, Time.deltaTime * speed * SolarSystem.Instance.rate_Revolution);
        }
        else
        {
            float angle = Time.time * speed * SolarSystem.Instance.rate_Revolution;
            float x = Mathf.Cos(angle % 360f) * 49f;
            float z = Mathf.Sin(angle % 360f) * 49f;
            Vector3 temp = new Vector3(x, 0f, z);
            transform.position = centre.position + temp;
        }
    }
    float GetRevolutionSpeed()
    {
        switch (this.name)
        {
            case "Mercury":
                tempSpeed = SolarSystem.Instance.mercurySpeed;
                break;
            case "Venus":
                tempSpeed = SolarSystem.Instance.venusSpeed;
                break;
            case "Earth":
                tempSpeed = SolarSystem.Instance.earthSpeed;
                break;
            case "Moon":
                tempSpeed = SolarSystem.Instance.moonSpeed;
                break;
            case "Mars":
                tempSpeed = SolarSystem.Instance.marsSpeed;
                break;
            case "Jupiter":
                tempSpeed = SolarSystem.Instance.jupiterSpeed;
                break;
            case "Saturn":
                tempSpeed = SolarSystem.Instance.saturnSpeed;
                break;
            case "Uranus":
                tempSpeed = SolarSystem.Instance.uranusSpeed;
                break;
            case "Neptune":
                tempSpeed = SolarSystem.Instance.neptuneSpeed;
                break;
        }
        return tempSpeed;
    }
    Transform GetRevolutionCentre()
    {
        switch (this.name)
        {
            case "Moon":
                tempCentre = SolarSystem.Instance.earth;
                break;
            default:
                tempCentre = SolarSystem.Instance.sun;
                break;
        }
        return tempCentre;
    }
    Vector3 GetRevolutionDirection()
    {
        switch(this.name)
        {
            case "Moon":
                tempDirection = Vector3.up;
                break;
            case "Venus":
                tempDirection = Vector3.down;
                break;
            default:
                tempDirection = Vector3.up;
                break;
        }
        return tempDirection;
    }
}
