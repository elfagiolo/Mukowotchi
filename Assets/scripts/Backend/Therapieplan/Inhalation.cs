using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inhalation : Therapie
{
    List<float> durations;
    public List<float> Durations
    {
        get
        {
            return durations;
        }
    }

    public Inhalation()
    {
        weekdays = new bool[7];
        times = new List<float>();
        durations = new List<float>();
    }

    public Inhalation(string _name, string _description, bool[] _weekdays, List<float> _times, List<float> _durations):this()
    {
        therapieName = _name;
        description = _description;
        _weekdays.CopyTo(weekdays, 0);
        times.AddRange(_times);

        durations.AddRange(_durations);
    }

    public void AddDuration()
    {

    }

    public void RemoveDuration()
    {

    }

}
