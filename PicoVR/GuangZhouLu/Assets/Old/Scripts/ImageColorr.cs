using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorr : MonoBehaviour {

    public Image m_imagePlayer_pos;

    // Use this for initialization
    void Start () {
        m_imagePlayer_pos = this.GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        while (m_imagePlayer_pos.color.a < 1)
        {
            m_imagePlayer_pos.color = new Color(1, 1, 1, m_imagePlayer_pos.color.a + 0.1f);
        }
        m_imagePlayer_pos.color = new Color(1, 1, 1, 1);
        while (m_imagePlayer_pos.color.a > 0)
        {
            m_imagePlayer_pos.color = new Color(1, 1, 1, m_imagePlayer_pos.color.a - 0.1f);
        }
        m_imagePlayer_pos.color = new Color(1, 1, 1, 0);
    }
}
