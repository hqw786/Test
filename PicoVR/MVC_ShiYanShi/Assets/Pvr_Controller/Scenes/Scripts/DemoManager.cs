using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using Pvr_UnitySDKAPI;

public class DemoManager : MonoBehaviour
{

    public Transform m_point;
    public Ray ray;
    public Transform direction;
    public Transform m_dot;

    public Text toast;

    // Use this for initialization
    void Start()
    {

        ray = new Ray();
        ray.origin = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        ray.direction = direction.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200))
        {
            m_point.gameObject.SetActive(true);
            m_point.transform.position = hit.point + new Vector3(0, 0, -0.1f);
            m_point.DOKill();
            m_point.DOScale(0.025f, 0.5f);
            m_dot.gameObject.SetActive(false);

        }
        else
        {
            m_point.DOScale(0.0f, 0.2f);
            m_point.gameObject.SetActive(false);
            m_dot.gameObject.SetActive(true);
        }
    }
}
