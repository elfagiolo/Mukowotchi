using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Therapie
{
    [SerializeField]
    //Name der Therapie
    protected string name;

    [SerializeField]
    //Kurze Beschreibung
    protected string description;
       
    [SerializeField]
    //Wochentage
    protected bool[] weekdays;

    [SerializeField]
    //Uhrzeit
    protected List<float> times;

    [SerializeField]
    //Color
    protected Color color;

    public void SetName(string _name)
    {
        name = _name;
    }

    public void SetDescription(string _description)
    {
        name = _description;
    }

    public void SetWeekDay(int _index, bool _checked)
    {
        weekdays[_index] = _checked;
    }

    public void SetWeekDays(bool[] _weekDays)
    {
        _weekDays.CopyTo(weekdays, 0);
    }

    public void AddTime(float _time)
    {
        times.Add(_time);
    }
    public void RemoveTime(float _time)
    {
        times.Remove(_time);
    }

    public void SetColor(Color _color)
    {
        color = _color;
    }
}
