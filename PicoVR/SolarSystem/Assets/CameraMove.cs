using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraStatus
{
    lockSolar,
    lockPlanet
}
public class CameraMove : MonoBehaviour {
    [HideInInspector]
    public CameraStatus lockStatus;

    //[HideInInspector]
    Transform lockPlanet;

    [Header("摄像机与行星保持的距离：")]
    public float r_sun;
    public float r_mercury;
    public float r_venus;
    public float r_earth;
    public float r_mars;
    public float r_jupiter;
    public float r_saturn;
    public float r_uranus;
    public float r_neptune;
	// Use this for initialization
	void Start () {
        lockStatus = CameraStatus.lockSolar;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if(lockStatus == CameraStatus.lockPlanet)
        {
            float dis = GetDistance();
            //可以移动和缩放
            //Vector3 endPoint = lockPlanet.position - Vector3.forward * dis;
            //print(Time.fixedDeltaTime);
            //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, endPoint, 0.8f);
            Camera.main.transform.position = lockPlanet.position - Vector3.forward * dis;
        }
    }
	void Update () {
		
	}
    
    public void CameraPositionMove(Transform planet)
    {
        //TODO:直接移到行星的指定位置(以后要加效果再说）
        lockStatus = CameraStatus.lockPlanet;
        lockPlanet = planet;
        Camera.main.transform.rotation = Quaternion.identity;
    }
    float GetDistance()
    {
        float temp = 0;
        switch (lockPlanet.name)
        {
            case "Sun":
                temp = r_sun;
                break;
            case "Mercury":
                temp = r_mercury;
                break;
            case "Venus":
                temp = r_venus;
                break;
            case "Earth":
                temp = r_earth;
                break;
            case "Mars":
                temp = r_mars;
                break;
            case "Jupiter":
                temp = r_jupiter;
                break;
            case "Saturn":
                temp = r_saturn;
                break;
            case "Uranus":
                temp = r_uranus;
                break;
            case "Neptune":
                temp = r_neptune;
                break;
        }
        return temp;
    }
}
