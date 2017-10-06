using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Therapie
{
    [SerializeField]
    //Name der Therapie
    protected string therapieName;

    public string Name
    {
        get
        {
            return therapieName;
        }
        set
        {
            therapieName = value;
        }
    }

    [SerializeField]
    //Kurze Beschreibung
    protected string description;
    public string Description
    {
        get
        {
            return description;
        }
        set
        {
            description = value;
        }
    }

    [SerializeField]
    //Wochentage
    protected bool[] weekdays;
    public bool[] Weekdays
    {
        get
        {
            return weekdays;
        }
        set
        {
            value.CopyTo(weekdays,0);
        }
    }

    [SerializeField]
    //Uhrzeit
    protected List<float> times;
    public List<float> Times
    {
        get
        {
            return times;
        }
    }


    public void SetWeekDay(int _index, bool _checked)
    {
        weekdays[_index] = _checked;
    }

    public void AddTime(float _time)
    {
        times.Add(_time);
    }

    public void RemoveTime(float _time)
    {
        times.Remove(_time);
    }
}
