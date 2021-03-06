﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedizinButton : MonoBehaviour
{
    public GameObject medicamentPrefab;

    public Sprite[] images;

    public List<float> removalList;

    public void SpawnAllMedicine()
    {
        removalList = new List<float>();
        TherapiePlanManager manager = TherapiePlanManager.instance;
        if (manager == null)
        {
            Debug.LogError("No Manager on MedizinButton");
            return;
        }
        foreach (float time in manager.AktiveTherapieZeiten)
        {
            int dayOfWeek = (int)System.DateTime.Now.DayOfWeek - 1;
            dayOfWeek = dayOfWeek < 0 ? 6 : dayOfWeek;

            //If the time is from yesterday
            if (time > System.DateTime.Now.Hour)
            {
                dayOfWeek -= 1;
                dayOfWeek = dayOfWeek < 0 ? 6 : dayOfWeek;
            }

            foreach (TherapiePlan.TherapieInfo therapieInfo in manager.TherapiePlan.GetTherapieInfoFor(dayOfWeek, time))
            {
                Therapie t = manager.TherapiePlan.Therapien[therapieInfo.index];
                if (therapieInfo.count != 0)
                {
                    Debug.Log("Spawn" + therapieInfo.count + t.Name);

                    for (int i = 0; i < therapieInfo.count; i++)
                    {
                        //Spawn medikamente
                        SpriteRenderer sprite = Instantiate(medicamentPrefab).GetComponent<SpriteRenderer>();
                        sprite.sprite = images[(t as Medikament).ImageIndex];
                        sprite.color = (t as Medikament).Color;

                    }
                    removalList.Add(time);
                }
            }
        }
        foreach(float time in removalList)
        {
            manager.RemoveActiveTherapy(time);
        }
        removalList.Clear();
        ButtonWiggleBlinkManager.instance.CheckActiveTherapies();
    }

}
