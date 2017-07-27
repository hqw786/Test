using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pvr_UnitySDKAPI;
using DG.Tweening;

public class EggLaying : MonoBehaviour {
    public Transform m_point;
    public Transform direction;
    public Ray ray;
    public Transform m_dot;
    public Text text;
    Pvr_GazeInputModule gazeInput;

    public Transform target;
	// Use this for initialization
	void Start () {
        gazeInput = direction.parent.parent.Find("Event").GetComponent<Pvr_GazeInputModule>();
	}
	
	// Update is called once per frame
	void Update () {
        ray.direction = direction.position - transform.position;

        Debug.DrawRay(transform.position, ray.direction,Color.green);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            m_point.gameObject.SetActive(true);
            m_point.transform.position = hit.point + new Vector3(0, 0, -0.1f);
            m_point.DOKill();
            m_point.DOScale(0.025f, 0.5f);
            m_dot.gameObject.SetActive(false);
            print(hit.collider.gameObject.name);
            target = hit.collider.transform;
            text.text = target.name;
        }
        else
        {
            m_point.DOScale(0.0f, 0.2f);
            m_point.gameObject.SetActive(false);
            m_dot.gameObject.SetActive(true);
            target = null;
        }

		if(Pvr_GazeInputModule.gazeGameObject != null &&　Pvr_GazeInputModule.gazeGameObject.name == "BtnEggLaying")
        {
            print("产蛋");
        }
        if(Controller.UPvr_GetKeyDown(Pvr_KeyCode.TOUCHPAD) ||Input.GetMouseButtonDown(0))
        {
            print(target);
        }
	}
}
