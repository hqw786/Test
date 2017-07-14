using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Transform m_trsPlayer;
    public Camera m_cameraPlayer;
    public CharacterController m_ccPlayer;

    public float m_dwSpeed = 25;
	public UIController m_cUIController;
	public m_eUIState m_eCurrState = m_eUIState.None;
	public m_eUIState m_eLastState = m_eUIState.None;



	public float floor = 1.52f;
	public float fly = 100f;

	/// <summary>
	/// bei
	/// </summary>
	public Vector3 m_v3North = Vector3.zero;
	/// <summary>
	/// nan
	/// </summary>
	public Vector3 m_v3South = Vector3.zero;

	// Use this for initialization
	void Start () {
		m_v3North = new Vector3 (136.9f, fly, -17.6f);
		m_v3South = new Vector3 (-100.5f,fly,12.746f);
	}
	
	// Update is called once per frame
	void Update () {
        if ((byte)m_eCurrState > 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("  Time   "+Time.time);
                m_cUIController.OnClickSelect(5);
            }
        }
        else
        {
		    if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			    m_ccPlayer.Move (new Vector3(m_trsPlayer.forward.x,0,m_trsPlayer.forward.z) * Time.deltaTime * m_dwSpeed);
		    } else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			    m_ccPlayer.Move (-new Vector3(m_trsPlayer.forward.x,0,m_trsPlayer.forward.z) * Time.deltaTime * m_dwSpeed);
		    } else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			    m_ccPlayer.Move (-new Vector3(m_trsPlayer.right.x,0,m_trsPlayer.right.z) * Time.deltaTime * m_dwSpeed);
		    } else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			    m_ccPlayer.Move (new Vector3(m_trsPlayer.right.x,0,m_trsPlayer.right.z) * Time.deltaTime * m_dwSpeed);
		    } else if (Input.GetKeyDown (KeyCode.Tab)) {
			    StartCoroutine (m_cUIController.ScreenEffect());
		    }

            if (Input.GetMouseButton(1))
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");
                y = Mathf.Clamp(y, -30, 40f);
                m_trsPlayer.eulerAngles = m_trsPlayer.eulerAngles + new Vector3(-y,x,0);
            }
            float fov = m_cameraPlayer.fov - Input.GetAxis("Mouse ScrollWheel")*3;
            fov = Mathf.Clamp(fov,10,30);
            m_cameraPlayer.fov = fov;
        }
			


    }
}
