using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesListPanel : MonoBehaviour
{
    public GameObject timesListElementPrefab;

    public List<TimesListElement> elements;

	// Use this for initialization
	void Start ()
    {
		if(timesListElementPrefab == null)
        {
            Debug.LogError("timesListElementPrefab not assigned in TimesListPanel");
        }
	}

    public void AddElement(float time, int count, float duration)
    {
        TimesListElement element = GameObject.Instantiate(timesListElementPrefab, transform).GetComponent<TimesListElement>();
        elements.Add(element);
        if (duration > 0)
            element.SetValues(time, 0, duration);
        else if (count > 0)
            element.SetValues(time, count, 0.0f);
        else
            element.SetValues(time, 0, 0.0f);
    }

    public void UpdateList(List<float> _times, List<int> _counts, List<float> _durations)
    {
        RemoveAll();
        bool inhalation = false;
        bool medication = false;
        if (_counts.Count == _times.Count) medication = true;
        else if (_durations.Count == _times.Count) inhalation = true;

        for (int i = 0; i < _times.Count; i++)
        {
            AddElement(_times[i], (medication ? _counts[i] : -1), (inhalation?_durations[i]:-1.0f));
        }
    }

    public void RemoveAll()
    {
        foreach(TimesListElement e in elements)
        {
            Destroy(e.gameObject);
        }
        elements.Clear();
    }
}
