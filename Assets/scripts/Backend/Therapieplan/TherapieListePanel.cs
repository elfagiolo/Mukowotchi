using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TherapieListePanel : MonoBehaviour {

    public GameObject therapyListElementPrefab;

    public List<TherapieListElement> elements;

    // Use this for initialization
    void Start()
    {
        if (therapyListElementPrefab == null)
        {
            Debug.LogError("therapyListElementPrefab not assigned in TherapieListePanel");
        }
    }

    public void AddElement(Therapie _therapie)
    {
        TherapieListElement element = GameObject.Instantiate(therapyListElementPrefab, transform).GetComponent<TherapieListElement>();
        element.SetTherapie(_therapie);
        elements.Add(element);
    }

    public void RemoveAll()
    {
        foreach (TherapieListElement e in elements)
        {
            Destroy(e.gameObject);
        }
        elements.Clear();
    }

    public void UpdateList(List<Therapie> _therapies)
    {
        RemoveAll();
        foreach(Therapie t in _therapies)
        {
            AddElement(t);
        }
    }
}
