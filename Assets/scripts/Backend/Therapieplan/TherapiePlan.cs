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
        public int therapieIndex;
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

    //Check the Therapieplan and get all notificationInfo for the given Day
    public NotificationInfo[] GetTherapiesForDay(DateTime _day, bool considerTime)
    {
        //Initialize list and days data
        List<NotificationInfo> infoStack = new List<NotificationInfo>();
        int dayOfWeek = (int)_day.DayOfWeek;
        dayOfWeek = (dayOfWeek - 1) < 0 ? 6 : dayOfWeek - 1;

        foreach (Therapie therapie in therapieListe)
        {
            if (therapie.Weekdays[dayOfWeek])
            {
                foreach (float time in therapie.Times)
                {
                    int[] iTime = TimeFloatToInt(time);
                    DateTime therapieTime = new DateTime(_day.Year, _day.Month, _day.Day, iTime[0], iTime[1], 0);
                    if (!considerTime || therapieTime > _day)
                    {
                        NotificationInfo info;
                        info.title = therapie.Name;
                        info.message = "Muki hat jetzt eine Therapie!";
                        info.time = therapieTime;
                        info.therapieIndex = therapie.Times.IndexOf(time);
                        infoStack.Add(info);
                    }
                }
            }
        }


        return infoStack.ToArray();
    }

    public NotificationInfo[] GetTherapyForToday()
    {
        return GetTherapiesForDay(DateTime.Now, true);
    }

    public NotificationInfo[] GetTherapieForXDaysFromNow(int daysToCheck)
    {
        List<NotificationInfo> infoStack = new List<NotificationInfo>();
        int currentDay = DateTime.Now.Day;
        infoStack.AddRange(GetTherapyForToday());
        for(int i = 1; i < daysToCheck; i++)
        {
            DateTime day = DateTime.Now;
            day = day.AddDays(i);
            Debug.Log("Checking DateTime:" + day.ToString());
            infoStack.AddRange(GetTherapiesForDay(day, false));
        }

        return infoStack.ToArray();
    }
}
