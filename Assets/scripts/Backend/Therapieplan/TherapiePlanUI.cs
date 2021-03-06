﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum TherapieTyp
{
    MEDIKAMENT,
    INHALATION,
    PHYSIOTHERAPIE
}

public class TherapiePlanUI : MonoBehaviour
{
    public static TherapiePlanUI instance;

    private TherapiePlanManager manager;

    //Canvas zum ein und ausschalten
    public GameObject therapiePlanCanvas;
    public GameObject therapieTypCanvas;
    public GameObject therapieNeuCanvas;

    //InputFields zum zurücksetzen
    public InputField inputName;
    public InputField inputHour;
    public InputField inputMinute;
    public InputField inputDurMin;
    public InputField inputDurSec;
    public InputField inputCount;

    public Toggle[] tgWeekDays;

	public Image imageThumb;
    public Image imageNextColor;
    public Image imagePreviousColor;
    public Image imageNextImage;
    public Image imagePreviousImage;

    public Sprite inhalationsImage;
    public Sprite phyioImage;

    //Unterschiedliche MedikamentenBilder
    public Sprite[] imagesTabletten;

	public Color[] colorsImage;

    //Panels für Listenerstellung
    public TimesListPanel timesListPanel;
    public TherapieListePanel therapiePanel;
    
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

    public int colorIndex = 0;

    //Anzahl (Nur bei Medikamenten)
    public int count = 1;
	public int imageIndex;
    //Dauer (Nur bei Inhalation) in Minuten
    public float duration = 5.0f; //Default 5 min.
    private int durMin;
    private int durSec;
    //Sporttyp (Nur bei Physio)
    public SportTyp sportTyp;

    //Daten die zum Hinzufügen gespeichert werden
    public List<float> times;
    public List<int> counts;
    public List<float> durations;
    
    public void Awake()
    {
        instance = this;
        times = new List<float>();
        counts = new List<int>();
        durations = new List<float>();
        Reset();
    }
    public void Start()
    {
        manager = TherapiePlanManager.instance;
        therapiePanel.UpdateList(manager.TherapiePlan.Therapien);
    }


    //Reset the Values of both this class and the inputfields/toggles
    private void Reset()
    {
        therapieName = "";
        inputName.text = therapieName;

        hour = 0;
        inputHour.text = "";
        minute = 0;
        inputMinute.text = "";
        count = 1;
        inputCount.text = "";
        durMin = 0;
        inputDurMin.text = "";
        durSec = 0;
        inputDurSec.text = "";

        weekdays = new bool[7];
        for(int i= 0;i<7;i++)
        {
            weekdays[i] = true;
            tgWeekDays[i].isOn = true;
        }

        //Clear the Lists
        times.Clear();
        counts.Clear();
        durations.Clear();
        
        //Remove all Elements from the TimeListPanel
        timesListPanel.RemoveAll();
        imageThumb.color = Color.white;

        imageNextColor.gameObject.SetActive(false);
        imagePreviousColor.gameObject.SetActive(false);
        imageNextImage.gameObject.SetActive(false);
        imagePreviousImage.gameObject.SetActive(false);
    }

    //Switch To the TypeChoicePanel
    public void SwitchToTypeChoice()
    {
        therapiePlanCanvas.SetActive(false);
        therapieTypCanvas.SetActive(true);
    }

    //Choose your Type and go to the NewTherapyPanel
    public void ChooseType(int typeIndex)
    {
        therapieTyp = (TherapieTyp)typeIndex;
        therapieTypCanvas.SetActive(false);
        therapieNeuCanvas.SetActive(true);
        Reset();
        ShowActiveImage();
        if (therapieTyp == TherapieTyp.MEDIKAMENT)
        {
            imageNextColor.gameObject.SetActive(true);
            imagePreviousColor.gameObject.SetActive(true);
            imageNextImage.gameObject.SetActive(true);
            imagePreviousImage.gameObject.SetActive(true);
            ShowActiveColor();
        }
    }

    //Cancel whatever you where doing and return to the Therapyplan
    public void Cancel()
    {
        therapieNeuCanvas.SetActive(false);
        therapieTypCanvas.SetActive(false);
        therapiePlanCanvas.SetActive(true);
        Reset();
    }

    public void EditTherapie(Therapie _therapie)
    {
        Reset();
        therapieName = _therapie.Name;
        inputName.text = therapieName;

        times = _therapie.Times;
        _therapie.Weekdays.CopyTo(weekdays,0);

        for (int i = 0; i < 7; i++)
        {
            tgWeekDays[i].isOn = weekdays[i];
        }

        if (_therapie is Medikament)
        {
            therapieTyp = TherapieTyp.MEDIKAMENT;
            Medikament medi = (_therapie as Medikament);
            counts = medi.Counts;
            imageIndex = medi.ImageIndex;
            FindColorIndex(medi.Color);
			ShowActiveColor ();
            manager.TherapiePlan.RemoveMedikament(medi);
        }
        else if(_therapie is Inhalation)
        {
            therapieTyp = TherapieTyp.INHALATION;
            Inhalation inha = (_therapie as Inhalation);
            durations = inha.Durations;
            manager.TherapiePlan.RemoveInhalation(inha);
        }
        else
        {
            therapieTyp = TherapieTyp.PHYSIOTHERAPIE;
            manager.TherapiePlan.RemovePhysiotherapie(_therapie as Physiotherapie);
        }
        ShowActiveImage();
        therapiePlanCanvas.SetActive(false);
        therapieNeuCanvas.SetActive(true);
        timesListPanel.UpdateList(times, counts, durations);
        therapiePanel.UpdateList(manager.TherapiePlan.Therapien);
        manager.needsToSave = true;
    }


    public void AddTherapie()
    {
        switch(therapieTyp)
        {
            case TherapieTyp.MEDIKAMENT:
			Medikament medi = new Medikament(therapieName, description, weekdays, times, imageIndex, colorsImage[colorIndex], counts);
                manager.TherapiePlan.AddMedikament(medi);
                therapiePanel.AddElement(medi);
                break;
            case TherapieTyp.INHALATION:
			Inhalation inha = new Inhalation(therapieName, description, weekdays, times, durations);
                manager.TherapiePlan.AddInhalation(inha);
                therapiePanel.AddElement(inha);
                break;
            case TherapieTyp.PHYSIOTHERAPIE:
                Physiotherapie physio = new Physiotherapie(weekdays, times, sportTyp);
                manager.TherapiePlan.AddPhysiotherapie(physio);
                therapiePanel.AddElement(physio);
                break;
            default:
                break;
        }
        therapieNeuCanvas.SetActive(false);
        therapiePlanCanvas.SetActive(true);
        Reset();
        manager.needsToSave = true;
    }

    public void AddTime()
    {
        float fTime = TherapiePlan.TimeIntToFloat(hour, minute);

        //If this timeslot is already taken, do not add it
        if (times.Contains(fTime)) return;

        //Add timeslot to list
        times.Add(fTime);
        //Add count/duration depending on TherapieType
        if (therapieTyp == TherapieTyp.MEDIKAMENT)
            counts.Add(count);
        else if (therapieTyp == TherapieTyp.INHALATION)
            durations.Add(duration);
        //Update the timeListPanel to show the new Timeslot
        timesListPanel.UpdateList(times, counts, durations);
    }

    public void RemoveTime(float _time)
    {
        //Find the Index of the timeslot given
        int index = times.IndexOf(_time);

        //Use the index to delete from counts/durations depending on TherapieType
        if (therapieTyp == TherapieTyp.MEDIKAMENT)
            counts.RemoveAt(index);
        else if (therapieTyp == TherapieTyp.INHALATION)
            durations.RemoveAt(index);

        //Remove the timeslot
        times.Remove(_time);
        //Update the timeListpanel to remove the timeslot as well
        timesListPanel.UpdateList(times, counts, durations);
    }

    public void SetWeekDay(int i, bool b)
    {
        weekdays[i] = b;
    }

    public void SetHour(int i)
    {
        hour = Mathf.Clamp(i, 0, 23);
    }

    public void SetMinute(int i)
    {
        minute = Mathf.Clamp(i, 0, 59);
    }

    public void SetCount(int i)
    {
        count = i;
    }

    public void SetDurationMinute(int i)
    {
        durMin = i;
        duration = TherapiePlan.TimeIntToFloat(durMin, durSec);
    }

    public void SetDurationSeconds(int i)
    {
        durSec = i;
        duration = TherapiePlan.TimeIntToFloat(durMin, durSec);
    }

	public void ShowActiveColor()
	{
        int nextIndex = colorIndex + 1 < colorsImage.Length ? colorIndex + 1 : 0;
        imageNextColor.color = colorsImage[nextIndex];
        int previousIndex = colorIndex - 1 >= 0 ? colorIndex - 1 : colorsImage.Length - 1;
        imagePreviousColor.color = colorsImage[previousIndex];
        imageThumb.color = colorsImage [colorIndex];
	}

	public void NextColor()
	{
		colorIndex = colorIndex + 1 < colorsImage.Length ? colorIndex + 1 : 0; 
		ShowActiveColor ();
	}

	public void PreviousColor()
	{
		colorIndex = colorIndex - 1 >= 0 ? colorIndex - 1 : colorsImage.Length-1; 
		ShowActiveColor ();
	}

    public void NextImage()
    {
        imageIndex = imageIndex + 1 < imagesTabletten.Length ? imageIndex + 1 : 0;
        ShowActiveImage();
    }

    public void PreviousImage()
    {
        imageIndex = imageIndex - 1 >= 0 ? imageIndex - 1 : imagesTabletten.Length - 1;
        ShowActiveImage();
    }

    public void ShowActiveImage()
    {
        if(therapieTyp == TherapieTyp.MEDIKAMENT)
        {
            imageThumb.sprite = imagesTabletten[imageIndex];
            return;
        }
        if (therapieTyp == TherapieTyp.INHALATION)
        {
            imageThumb.sprite = inhalationsImage;
            return;
        }
        if(therapieTyp == TherapieTyp.PHYSIOTHERAPIE)
        {
            imageThumb.sprite = phyioImage;
            return;
        }
    }

    public void FindColorIndex(Color c)
	{
		for (int i = 0; i < colorsImage.Length; i++) 
		{
			if(colorsImage[i] == c)
				colorIndex = i;
		}
	}

    public void QuitMenu()
    {
        if(manager.needsToSave)
        {
            manager.SaveDataToDisk();
            manager.ScheduleTherapyForSetDays();
        }
    }

    public void ScheduleTherapy()
    {
        manager.ScheduleTherapyForSetDays();
    }
}
