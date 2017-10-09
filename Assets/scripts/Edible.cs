using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour  {

    //[SerializeField]
    //private int m_iNumOfBites = 2;

    public enum FoodType
    {
        NORMAL, PILL, KREON, WATER
    };

    [SerializeField]
    private int m_iFettwert = 0;
    [SerializeField]
    private Sprite[] m_arrSprites;
    [SerializeField]
    private FoodType m_foodType = FoodType.NORMAL;

    private float m_fEatingSpeed = 0.5f;
    private Vector3 m_v3OriginalPosition;
    private float m_fTimer = 0f;
    private SpriteRenderer m_renderer;

    private int m_iBites;
    private int m_iOrigSorting;
    private bool m_bEating = false;
    private Animator m_mukiAnim;

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

    private void Update()
    {
        if (m_bEating)
            Eating();
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mousePosition;

        m_renderer.sortingOrder = m_iOrigSorting + 5;
    }

    private void OnMouseUp()
    {
        //transform.position = m_v3OriginalPosition;

        //m_renderer.sortingOrder = m_iOrigSorting;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Muki"))
        {
            m_bEating = true;
            if (m_mukiAnim == null)
                m_mukiAnim = collision.GetComponent<Animator>();
            int eating = m_mukiAnim.GetInteger("eating");
            m_mukiAnim.SetInteger("eating",eating + 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Muki"))
        {
            m_bEating = false;
            m_fTimer = 0f;
            int eating = m_mukiAnim.GetInteger("eating");
            m_mukiAnim.SetInteger("eating", Mathf.Max(eating - 1,0));
        }
    }

    void Eating()
    {
        m_fTimer += Time.deltaTime;

        if (m_fTimer >= m_fEatingSpeed)
        {
            m_fTimer = 0f;
            BiteOff();
        }
    }

    // for each bite
    void BiteOff()
    {
        // check if the mouth is full rn for normal food
        if (m_foodType == FoodType.NORMAL)
        {
            if (Muki.s_instance.m_bMouthIsFull)
                return;
        }

        m_iBites++;
        if (m_iBites < m_arrSprites.Length)
            m_renderer.sprite = m_arrSprites[m_iBites];
        else
            Eaten();
    }

    // gets eaten and swallowed
    void Eaten()
    {
        switch (m_foodType)
        {
            case FoodType.NORMAL: 
                // spawn kreons
                var kSpawner = KreonManager.s_instance;
                if (kSpawner != null) {
                    kSpawner.SpawnKreons(m_iFettwert);
                }
                MukiNeeds.s_instance.UpdateDateAndTime(true);
                MukiNeeds.s_instance.Consumes(100f, true);
                break;
            case FoodType.KREON:
                KreonManager.s_instance.AddKreonToMouth();
                Muki.s_instance.addPill();
                break;
            case FoodType.PILL:
                Muki.s_instance.addPill();
                break;
            case FoodType.WATER:
                Muki.s_instance.SwallowWithWater();
                KreonManager.s_instance.SwallowKreons();
                MukiNeeds.s_instance.UpdateDateAndTime(false);
                MukiNeeds.s_instance.Consumes(100f, false);
                break;
        }

        // destroy this food
        Destroy(this.gameObject);
    }

    void OnDisable()
    {
        if (m_bEating)
        {
            m_bEating = false;
            int eating = m_mukiAnim.GetInteger("eating");
            m_mukiAnim.SetInteger("eating", -1);
        }
    }
}
