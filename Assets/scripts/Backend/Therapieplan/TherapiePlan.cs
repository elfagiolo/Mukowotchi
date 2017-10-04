using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class TherapiePlan
{
    public struct NotificationInfo
    {
        public string title;
        public string message;
        public System.DateTime time;
    }

    [System.NonSerialized]
    List<Therapie> therapieListe;
    [SerializeField]
    List<Medikament> medikamentListe;
    [SerializeField]
    List<Inhalation> inhalationListe;
    [SerializeField]
    List<Physiotherapie> physiotherapieListe;

    public List<Therapie> Therapien
    {
        get
        {
            return therapieListe;
        }
    }

    public TherapiePlan()
    {
        therapieListe = new List<Therapie>();
        medikamentListe = new List<Medikament>();
        inhalationListe = new List<Inhalation>();
        physiotherapieListe = new List<Physiotherapie>();
    }

    public void InitTherapieListe()
    {
        therapieListe = new List<Therapie>();
        foreach(Medikament medi in medikamentListe)
        {
            therapieListe.Add(medi);
        }
        foreach (Inhalation inha in inhalationListe)
        {
            therapieListe.Add(inha);
        }
        foreach (Physiotherapie physi in physiotherapieListe)
        {
            therapieListe.Add(physi);
        }
    }

    public void AddTherapie(Therapie _therapie)
    {
        therapieListe.Add(_therapie);
    }

    public void AddMedikament(Medikament _medikament)
    {
        medikamentListe.Add(_medikament);
        AddTherapie(_medikament);
    }

    public void AddInhalation(Inhalation _inhalation)
    {
        inhalationListe.Add(_inhalation);
        AddTherapie(_inhalation);
    }

    public void AddPhysiotherapie(Physiotherapie _physiotherapie)
    {
        physiotherapieListe.Add(_physiotherapie);
        AddTherapie(_physiotherapie);
    }

    public void RemoveTherapie(Therapie _therapie)
    {
        therapieListe.Remove(_therapie);
    }

    public void RemoveMedikament(Medikament _medikament)
    {
        medikamentListe.Remove(_medikament);
        RemoveTherapie(_medikament);
    }

    public void RemoveInhalation(Inhalation _inhalation)
    {
        inhalationListe.Remove(_inhalation);
        RemoveTherapie(_inhalation);
    }

    public void RemovePhysiotherapie(Physiotherapie _physiotherapie)
    {
        physiotherapieListe.Remove(_physiotherapie);
        RemoveTherapie(_physiotherapie);
    }




    public static float TimeIntToFloat(int _hour, int _minute)
    {
        float fTime = _hour + _minute / 60.0f;
        return fTime;
    }

    public static int[] TimeFloatToInt(float fTime)
    {
        int[] hourMinute = new int[2];

        hourMinute[0] = Mathf.FloorToInt(fTime);
        float hours60 = fTime * 60.0f;
        //Debug.Log(hours60);
        hourMinute[1] = (int)(hours60 - hourMinute[0] * 60.0f);
        hourMinute[0] = Mathf.Clamp(hourMinute[0], 0, 23);

        //Debug.Log(fTime + "->" + hourMinute[0] + ":" + hourMinute[1]);
        return hourMinute;
    }

    public NotificationInfo[] CheckMedicineForToday()
    {
        List<NotificationInfo> infoStack = new List<NotificationInfo>();

        DateTime currentTime = DateTime.Now;
        int currentDayOfWeek = (int)currentTime.DayOfWeek;
        Debug.Log("The current day of week is:" + currentDayOfWeek);

        foreach(Therapie therapie in therapieListe)
        {
            if(therapie.Weekdays[currentDayOfWeek-1])
            {
                foreach(float time in therapie.Times)
                {
                    NotificationInfo info;
                    info.title = therapie.Name;
                    info.message = "Muki hat jetzt eine Therapie! Bitte hilf Muki dabei, gesund zu bleiben.";
                    int[] iTime = TimeFloatToInt(time);
                    info.time = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, iTime[0], iTime[1], 0);
                    infoStack.Add(info);
                }
            }
        }

        return infoStack.ToArray();
    }
}
