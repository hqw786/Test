using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingMuTrans : MonoBehaviour {
    Material material;
    Color color;
    public bool isHide;
    public bool isDisplay;
    // Use this for initialization
    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        color = material.color;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHide)
        {
            float r = Mathf.Lerp(color.r, 0f, 0.01f);
            material.color = new Color(r, r, r, 1);
            color = material.color;
            if (color.r < 0.1f)
            {
                isHide = false;
            }
        }
        if (isDisplay)
        {
            float r = Mathf.Lerp(color.r, 1f, 0.01f);
            material.color = new Color(r,r,r,1);
            color = material.color;
            if (color.r > 0.95f)
            {
                isDisplay = false;
            }
        }
    }
    public void SetHide()
    {
        isHide = true;
    }
    public void SetDisplay()
    {
        isDisplay = true;
    }
}
