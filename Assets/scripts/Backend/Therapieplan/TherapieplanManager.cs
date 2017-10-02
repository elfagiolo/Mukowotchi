using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TherapieTyp
{
    MEDIKAMENT,
    INHALATION,
    PHYSIOTHERAPIE
}

public class TherapieplanManager : MonoBehaviour
{
    public static TherapieplanManager instance;

    private Sprite[] sprites;

    public TherapiePlan therapiePlan;

    //Über das UI editierbare Variablen

    //Typ
    TherapieTyp therapieTyp = TherapieTyp.MEDIKAMENT;

    //Name
    public string therapieName = "Medikament A";

    //Beschreibung
    public string description = "";

    //Uhrzeit
    public int hour = 0;
    public int minute = 0;

    public bool[] weekdays;

    public Color color;

    //Anzahl (Nur bei Medikamenten)
    public int count = 1;
    //Dauer (Nur bei Inhalation) in Minuten
    public float duration = 5.0f; //Default 5 min.
    //Sporttyp (Nur bei Physio)
    public SportTyp sportTyp;

    //Daten die zum Hinzufügen gespeichert werden
    private List<float> times;
    private List<int> counts;
    private List<float> durations;

    public BasicNotification notifier;

    public void Start()
    {
        instance = this;
        weekdays = new bool[7];
        therapiePlan = new TherapiePlan();
    }

    public void AddTherapie()
    {
        AddTime();
        switch(therapieTyp)
        {
            case TherapieTyp.MEDIKAMENT:
                therapiePlan.AddTherapie(new Medikament(name, description, weekdays, times, color, counts));
                return;
            case TherapieTyp.INHALATION:
                therapiePlan.AddTherapie(new Inhalation(name, description, weekdays, times, color, durations));
                return;
            case TherapieTyp.PHYSIOTHERAPIE:
                therapiePlan.AddTherapie(new Physiotherapie(weekdays, times, sportTyp));
                return;
            default:
                return;
        }
    }

    public void AddTime()
    {
        times.Add(therapiePlan.TimeIntToFloat(hour, minute));
    }

    public void RemoveTime()
    {
        times.Remove(therapiePlan.TimeIntToFloat(hour, minute));
    }

    public void SetWeekDay(int i, bool b)
    {
        weekdays[i] = b;
    }

    public void SetHour(int i)
    {
        hour = i;
    }

    public void SetMinute(int i)
    {
        minute = i;
    }

    public void SetCount(int i)
    {
        count = i;
    }

    public void ScheduleAllNotificationsForToday()
    {
        //notifier.Notify(therapiePlan.CheckMedicineForToday().ToArray());
    }
}
