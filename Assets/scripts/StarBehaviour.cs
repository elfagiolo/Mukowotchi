using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehaviour : MonoBehaviour {

    [SerializeField]
    private float m_fSizingSpeed = 50f;
    [SerializeField]
    private float m_fMinSize = 0.005f;

    private Transform m_transform;
    private Rigidbody2D m_rigid;
    private bool m_bDisappearing = false;

    void Awake()
    {
        m_transform = transform;
        m_rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_bDisappearing)
        {
            m_transform.localScale = m_transform.localScale * (1f - (m_fSizingSpeed * Time.deltaTime));

            if (m_transform.localScale.magnitude < m_fMinSize)
            {
                Score.s_instance.StarArrived();
                Destroy(gameObject);
            }
        }
    }

	public void Disappear()
    {
        if (m_bDisappearing) return;
        m_bDisappearing = true;
        m_rigid.drag = 50f;
    }

}
