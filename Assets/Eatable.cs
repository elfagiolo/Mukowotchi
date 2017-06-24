using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour  {

    //[SerializeField]
    //private int m_iNumOfBites = 2;

    private float m_fEatingSpeed = 0.5f;
    private Vector3 m_v3OriginalPosition;
    private float timer = 0f;

    private void Awake()
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
            timer += Time.deltaTime;

            if (timer >= m_fEatingSpeed)
                Eaten();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer = 0f;
    }

    void Eaten()
    {
        Destroy(this.gameObject);
    }
}
