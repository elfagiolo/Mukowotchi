using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Medikament : Therapie
{
    private List<int> counts;

    public Medikament()
    {
        weekdays = new bool[7];
        times = new List<float>();
    }

    public Medikament(string _name, string _description, bool[] _weekdays, List<float> _times,/* Sprite _image,*/ Color _color, List<int> _counts):this()
    {
        name = _name;
        description = _description;
        _weekdays.CopyTo(weekdays, 0);
        times.AddRange(_times);
        //image = _image;
        color = _color;
        counts.AddRange(_counts);
    }

    public void AddCount(int count)
    {
        counts.Add(count);
    }
}
