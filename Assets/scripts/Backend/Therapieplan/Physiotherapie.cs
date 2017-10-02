using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SportTyp
{
    RUTSCHE,
    GIRAFFE,
    SCHRAUBE,
    HALBMOND
}

[System.Serializable]
public class Physiotherapie : Therapie
{

    private SportTyp sportTyp;

    public Physiotherapie()
    {
        name = "Sport";
        description = "Unterschiedliche Sportübungen";

        weekdays = new bool[7];
        times = new List<float>();
    }

    public Physiotherapie(bool[] _weekdays, List<float> _times, SportTyp _sportTyp) : this()
    {
        _weekdays.CopyTo(weekdays,0);
        times.AddRange(_times);

        sportTyp = _sportTyp;
    }
}
