//using UnityEngine;
//using System.Collections;

//public class SceneFadeInOut : MonoBehaviour
//{
//    public float fadeSpeed = 1.5f;
//    public Texture texture;
//    GUITexture guiTexture;  
//    private bool sceneStarting = false;
//    private bool sceneEnding = false;
    
      
//    void Awake()
//    {
//        guiTexture = new GUITexture();
//        guiTexture.texture = texture;
//        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        
//    }
      
//    void Update()
//    {
//        if (sceneStarting)
//        {
//            StartScene();
//        }
          
//        if (sceneEnding)
//        {
//            EndScene();
//        }
//    }
      
//    void FadeToClear()
//    {
//        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
//    }
      
//    void FadeToBlack()
//    {
//        guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
//    }
      
//    void StartScene()
//    {
//        guiTexture.enabled = true;
//        FadeToBlack();
//        if(guiTexture.color.a >= 0.95f)
//        {
//            sceneEnding = true;
//        }
//    }
      
//    public void EndScene()
//    {
        
//        FadeToClear();
          
//        if(guiTexture.color.a < 0.05f)
//        {
//            guiTexture.color = Color.clear;
//            guiTexture.enabled = false;
//            sceneStarting  = false;
//            sceneEnding = false;
//            this.enabled = false;
//        }
//    }
      
//    public void StartFade()
//    {
//        sceneStarting = true;
//    }
//}

