using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraStatus
{
    lockSolar,
    lockPlanet
}
public class CameraMove : MonoBehaviour {
    [Header("摄像机与行星保持的距离：")]
    public float r_sun;
    public float r_mercury;
    public float r_venus;
    public float r_earth;
    public float r_moon;
    public float r_mars;
    public float r_jupiter;
    public float r_saturn;
    public float r_uranus;
    public float r_neptune;
    
    [HideInInspector]
    public CameraStatus lockStatus;
    Transform lockPlanet;//近距离观看的星球

    Vector3 oldPositon;
    Quaternion oldRotation;
    


	// Use this for initialization
	void Start () {
        lockStatus = CameraStatus.lockSolar;
        oldPositon = transform.position;
        oldRotation = transform.rotation;
        ShowPlanetView.CameraReturnToDefaultEvent += ReturnToDefaultPosition;
	}

    private void ReturnToDefaultPosition()
    {
        transform.position = oldPositon;
        transform.rotation = oldRotation;
        lockStatus = CameraStatus.lockSolar;
    }
	
	// Update is called once per frame
    //void FixedUpdate()
    void LateUpdate()
    {
        if(lockStatus == CameraStatus.lockPlanet)
        {
            float dis = GetDistance();
            //可以移动和缩放
            Camera.main.transform.position = lockPlanet.position - Vector3.forward * dis;
        }
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
