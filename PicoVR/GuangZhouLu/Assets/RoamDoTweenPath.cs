using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RoamDoTweenPath : MonoBehaviour {
    DOTweenPath dtp;
    public Button button;
    Vector3[] wayPoints;
	// Use this for initialization
	void Start () {
        dtp = GetComponent<DOTweenPath>();

        button.onClick.AddListener(OnButtonClick);
                
        LoadDoTweenPath();
	}
    public void OnButtonClick()
    {
        transform.DOPath(wayPoints, 45f, PathType.Linear);
    }
    private void LoadDoTweenPath()
    {
        List<Vector3> temp = new List<Vector3>();
        foreach (Transform t in transform)
        {
            temp.Add(t.position);
        }
        wayPoints = new Vector3[temp.Count];
        wayPoints = temp.ToArray();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
