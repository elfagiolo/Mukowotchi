using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TherapieListElement : MonoBehaviour
{
    public Text therapyName;
    public Text therapyTimes;
    public Text therapyWeekdays;

    public Color colorMedi;
    public Color colorInha;
    public Color colorPhysi;

    private Therapie therapie;

    public void SetTherapie(Therapie _therapie)
    {
        therapie = _therapie;

        if (therapie is Medikament)
            GetComponent<Image>().color = colorMedi;
        else if (therapie is Inhalation)
            GetComponent<Image>().color = colorInha;
        else
            GetComponent<Image>().color = colorPhysi;

        therapyName.text = therapie.Name;

        bool[] weekdays = therapie.Weekdays;
        string days = "";
        int allCount = 0;
        if (weekdays[0])
        {
            days += "Mo ";
            allCount++;
        }
        if (weekdays[1])
        {
            days += "Di ";
            allCount++;
        }
        if (weekdays[2])
        {
            days += "Mi ";
            allCount++;
        }
        if (weekdays[3])
        {
            days += "Do ";
            allCount++;

        }
        if (weekdays[4])
        {
            days += "Fr ";
            allCount++;
        }
        if (weekdays[5])
        {
            days += "Sa ";
            allCount++;
        }

        if (weekdays[6])
        {
            days += "So ";
            allCount++;
        }

        if (allCount > 6) days = "Täglich";

        therapyWeekdays.text = days;

        string times = "";
        for(int i = 0; i < therapie.Times.Count; i++)
        {
            if (i > 2)
            {
                times += "...";
                break;
            }
            int[] hm = TherapiePlan.TimeFloatToInt(therapie.Times[i]);
            times += hm[0] + ":" + hm[1].ToString("D2");
            if (therapie is Medikament)
                times += "(" + (therapie as Medikament).Counts[i] + ")";
            else if (therapie is Inhalation)
            {
                int[] time = TherapiePlan.TimeFloatToInt((therapie as Inhalation).Durations[i]);
                times += "(" + time[0] + ":" + time[1].ToString("D2") + ")";
            }
            times += "\n";

        }
        therapyTimes.text = times;
    }

    public void EditMe()
    {
        TherapiePlanUI.instance.EditTherapie(therapie);
    }

}
