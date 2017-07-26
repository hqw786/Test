using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutEffect : MonoBehaviour {
    public Texture mater;
    Material shaderMaterial;
    public bool isFadeIn;
    public bool isFadeOut;
	// Use this for initialization
    void Awake()
    {
        shaderMaterial = new Material(Shader.Find("Custom/FadeInFadeOut"));	
    }
	void Start () {
        shaderMaterial.SetTexture("_MainTex", mater);
	}
	
	// Update is called once per frame
	void Update () {
        if(isFadeOut)
        {
            float f = shaderMaterial.GetFloat("_Float1");
            if(f > -0.99f)
            {
                f = Mathf.Lerp(f, -1, Time.deltaTime*1.5f);
                shaderMaterial.SetFloat("_Float1", f);
                //Debug.Log(f);
            }
        }
        if(isFadeIn)
        {
            float f = shaderMaterial.GetFloat("_Float1");
            if (f < -0.01f)
            {
                f = Mathf.Lerp(f, 0, Time.deltaTime *1.5f);
                shaderMaterial.SetFloat("_Float1", f);
                //Debug.Log(f);
            }
        }
	}
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //拷贝源纹理到目的渲染纹理。这主要是用于实现图像效果
        //Blit设置dest到激活的渲染纹理，在材质上设置source作为
        //_MainTex属性，并且绘制一个全屏方块
        Graphics.Blit(source, destination, shaderMaterial);
    }
}
