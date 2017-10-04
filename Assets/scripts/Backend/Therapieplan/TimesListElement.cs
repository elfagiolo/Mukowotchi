using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimesListElement : MonoBehaviour
{
    public Text time_hour;
    public Text time_minute;
    public Text countField;
    public Text duration_min;
    public Text duration_sec;

    public Color normalColor;
    public Color fadedColor;

    private float ftime;
    private int icount;
    private float fduration;

    public void Start()
    {
        normalColor = time_hour.color;   
    }
    
    public void SetValues(float _time, int _count, float _duration)
    {
        ftime = _time;
        icount = _count;
        fduration = _duration;
        int[] time = TherapiePlan.TimeFloatToInt(_time);
        time_hour.text = time[0].ToString();
        time_minute.text = time[1].ToString("D2");

        countField.text = _count.ToString();
        countField.color = _count == 0 ? fadedColor : normalColor;


        int[] duration = TherapiePlan.TimeFloatToInt(_duration);
        duration_min.text = duration[0].ToString();
        duration_sec.text = duration[1].ToString("D2");

        duration_min.color = _duration == 0.0f ? fadedColor : normalColor;
        duration_sec.color = _duration == 0.0f ? fadedColor : normalColor;
    }

    public void DeleteMe()
    {
        TherapiePlanUI.instance.RemoveTime(ftime);
    }
}
