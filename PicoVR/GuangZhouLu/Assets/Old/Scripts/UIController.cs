using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Image[] m_arrImageSelect;

    public GameObject m_objMapPanel;
    public GameObject m_objCtrlPanel;

	public Controller m_cController;
	public Transform m_trsPlayer_pos;

	public Image m_imageScreenEffect;

    public Image m_imagePlayer_pos;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < m_arrImageSelect.Length; i++)
        {
            m_arrImageSelect[i].color = new Color(1,1,1,0.1f);
        }
        m_objMapPanel.SetActive(false);
        m_objCtrlPanel.SetActive(false);
		m_imageScreenEffect.enabled = false;
		OnClickSelect (0);
    }
	
	// Update is called once per frame
	void Update () {
		m_trsPlayer_pos.localPosition = new Vector3 (-m_cController.m_trsPlayer.position.z* 0.5f,m_cController.m_trsPlayer.position.x* 2.15f,0)+new Vector3(-18f,-75f,0);
		//Debug.Log (m_cController.m_trsPlayer.position);
	}

    public void OnClickSelect(int index) {
		if (index > 3) {
            if (m_cController.m_trsPlayer.position.y > 20) {
				index = 1;
			} else {
				index = 0;
			}
		}
        Debug.Log(m_cController.m_eCurrState+"   "+index);
        if (m_cController.m_eCurrState != (m_eUIState)index)
        {
            if (m_cController.m_eLastState != m_cController.m_eCurrState)
                m_cController.m_eLastState = m_cController.m_eCurrState;
            m_cController.m_eCurrState = (m_eUIState)index;
            if ((byte)m_cController.m_eCurrState < 2)
            {
                StartCoroutine(ScreenEffect_1());
            }
        }
        else
        {
            return;
        }
        for (int i = 0; i < m_arrImageSelect.Length; i++)
        {
            if (index == i)
            {
                m_arrImageSelect[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                m_arrImageSelect[i].color = new Color(1, 1, 1, 0.1f);
            }
                
            if (index == 2)
            {
                m_objMapPanel.SetActive(true);
                StopCoroutine("PosPlayer");
                StartCoroutine("PosPlayer");
            }
            else
            {
                m_objMapPanel.SetActive(false);
            }

            if (index == 3)
            {
                m_objCtrlPanel.SetActive(true);
            }
            else
            {
                m_objCtrlPanel.SetActive(false);
            }


        }
    }

    public void CloseCtrlPanel() {
        OnClickSelect(5);
    }

	public IEnumerator ScreenEffect(){
		m_imageScreenEffect.enabled = true;
		while (m_imageScreenEffect.color.a < 1) {
			yield return new WaitForEndOfFrame();
			m_imageScreenEffect.color = new Color (0,0,0,m_imageScreenEffect.color.a+0.1f);
		}
		m_imageScreenEffect.color = new Color (0,0,0,1);
		byte temp = (byte)((byte)m_cController.m_eCurrState+(byte)1);
		OnClickSelect (temp%2);
		SetPos ();
		while (m_imageScreenEffect.color.a > 0) {
			yield return new WaitForEndOfFrame();
			m_imageScreenEffect.color = new Color (0,0,0,m_imageScreenEffect.color.a-0.1f);
		}
		m_imageScreenEffect.color = new Color (0,0,0,0);
		m_imageScreenEffect.enabled = false;
	}

	public IEnumerator ScreenEffect_1(){
		m_imageScreenEffect.enabled = true;
		while (m_imageScreenEffect.color.a < 1) {
			yield return new WaitForEndOfFrame();
			m_imageScreenEffect.color = new Color (0,0,0,m_imageScreenEffect.color.a+0.1f);
		}
		m_imageScreenEffect.color = new Color (0,0,0,1);
		SetPos ();
		while (m_imageScreenEffect.color.a > 0) {
			yield return new WaitForEndOfFrame();
			m_imageScreenEffect.color = new Color (0,0,0,m_imageScreenEffect.color.a-0.1f);
		}
		m_imageScreenEffect.color = new Color (0,0,0,0);
		m_imageScreenEffect.enabled = false;
	}

	void SetPos(){
		float h = 0;
		switch (m_cController.m_eCurrState) {
		case m_eUIState.PlayerState:
			h = m_cController.floor;
			break;
		case m_eUIState.SkyState:
			h = m_cController.fly;
			break;
		}
		m_cController.m_trsPlayer.position = new Vector3 (m_cController.m_trsPlayer.position.x,h,m_cController.m_trsPlayer.position.z);
	}

	public void ChangePos(int index){
		switch (index) {
		case 0:
			m_cController.m_trsPlayer.position = new Vector3(m_cController.m_v3North.x,m_cController.m_trsPlayer.position.y,m_cController.m_v3North.z);
			break;
		case 1:
			m_cController.m_trsPlayer.position = new Vector3(m_cController.m_v3South.x,m_cController.m_trsPlayer.position.y,m_cController.m_v3South.z);
			break;
		}
	}

    public IEnumerator PosPlayer() {
        while (m_cController.m_eCurrState == m_eUIState.MapState)
        {
            while (m_imagePlayer_pos.color.a < 1)
            {
                yield return new WaitForEndOfFrame();
                m_imagePlayer_pos.color = new Color(1, 1, 1, m_imagePlayer_pos.color.a + 0.05f);
            }
            m_imagePlayer_pos.color = new Color(1, 1, 1, 1);
            while (m_imagePlayer_pos.color.a > 0)
            {
                yield return new WaitForEndOfFrame();
                m_imagePlayer_pos.color = new Color(1, 1, 1, m_imagePlayer_pos.color.a - 0.05f);
            }
            m_imagePlayer_pos.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
