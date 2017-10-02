using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TherapiePlan
{
    [SerializeField]
    List<Therapie> therapieListe;

    public void AddTherapie(Therapie _therapie)
    {
        therapieListe.Add(_therapie);
    }

    public float TimeIntToFloat(int _hour, int _minute)
    {
        float fTime = _hour + _minute / 60.0f;
        return fTime;
    }

    public int[] TimeFloatToInt(float fTime)
    {
        int[] hourMinute = new int[2];
        hourMinute[0] = (int)Mathf.Floor(fTime);
        hourMinute[1] = (int)((fTime - hourMinute[0]) * 60.0f);
        Debug.Log(fTime + "->" + hourMinute[0] + ":" + hourMinute[1]);
        return hourMinute;
    }
}
