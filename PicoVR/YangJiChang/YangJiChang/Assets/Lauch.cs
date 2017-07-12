using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lauch : MonoBehaviour {
    public MovieTexture movTexture;
	// Use this for initialization
	void Start () {
        movTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(!movTexture.isPlaying)
        {
            SceneManager.LoadScene(1);
        }
	}
}
