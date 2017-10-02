using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour  {

    //[SerializeField]
    //private int m_iNumOfBites = 2;

    [SerializeField]
    private int fettwert = 0;
    [SerializeField]
    public Sprite[] sprites;

    private float m_fEatingSpeed = 0.5f;
    private Vector3 m_v3OriginalPosition;
    private float m_fTimer = 0f;
    private SpriteRenderer m_renderer;

    private int bites;

    private void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        bites = 0;
    }

    private void Start()
    {
        m_v3OriginalPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mousePosition;
    }

    private void OnMouseUp()
    {
        transform.position = m_v3OriginalPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Muki"))
        {
            m_fTimer += Time.deltaTime;

            if (m_fTimer >= m_fEatingSpeed)
            {
                m_fTimer = 0f;
                BiteOff();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_fTimer = 0f;
    }

    void BiteOff()
    {
        bites++;
        if (bites < sprites.Length)
            m_renderer.sprite = sprites[bites];
        else
            Eaten();
    }

    void Eaten()
    {
        Destroy(this.gameObject);
    }
}
