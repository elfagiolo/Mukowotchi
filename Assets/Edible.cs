﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour  {

    //[SerializeField]
    //private int m_iNumOfBites = 2;

    [SerializeField]
    private int m_iFettwert = 0;
    [SerializeField]
    public Sprite[] m_arrSprites;

    private float m_fEatingSpeed = 0.5f;
    private Vector3 m_v3OriginalPosition;
    private float m_fTimer = 0f;
    private SpriteRenderer m_renderer;

    private int m_iBites;
    private int m_iOrigSorting;

    private void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_iBites = 0;
        m_iOrigSorting = m_renderer.sortingOrder;
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

        m_renderer.sortingOrder = m_iOrigSorting + 10;
    }

    private void OnMouseUp()
    {
        transform.position = m_v3OriginalPosition;

        m_renderer.sortingOrder = m_iOrigSorting;
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

    // for each bite
    void BiteOff()
    {
        m_iBites++;
        if (m_iBites < m_arrSprites.Length)
            m_renderer.sprite = m_arrSprites[m_iBites];
        else
            Eaten();
    }

    // gets eaten and swallowed
    void Eaten()
    {
        // spawn kreons
        var kSpawner = KreonManager.s_instance;
        if (!kSpawner)
            kSpawner.SpawnKreons(m_iFettwert);

        // destroy this food
        Destroy(this.gameObject);
    }
}
