﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inhalation : Therapie
{
    List<float> durations;

    public Inhalation()
    {
        weekdays = new bool[7];
        times = new List<float>();
    }

    public Inhalation(string _name, string _description, bool[] _weekdays, List<float> _times,/* Sprite _image,*/ Color _color, List<float> _durations):this()
    {
        name = _name;
        description = _description;
        _weekdays.CopyTo(weekdays, 0);
        times.AddRange(_times);
        //image = _image;
        color = _color;

        durations.AddRange(_durations);
    }

}
