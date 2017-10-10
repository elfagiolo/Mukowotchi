using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonWiggleBlinkManager : MonoBehaviour
{
    public static ButtonWiggleBlinkManager instance;

    public delegate void MediEvent(bool wiggle);
    public static event MediEvent mediEvent;

    public delegate void InhaEvent(bool wiggle);
    public static event InhaEvent inhaEvent;

    public delegate void PhysiEvent(bool wiggle);
    public static event PhysiEvent physiEvent;

    public delegate void FoodEvent(bool wiggle);
    public static event FoodEvent foodEvent;

    public delegate void DrinkEvent(bool wiggle);
    public static event DrinkEvent drinkEvent;

    public delegate void KreonEvent(bool wiggle);
    public static event KreonEvent kreonEvent;

    public delegate void RightArrowEvent(bool wiggle);
    public static event RightArrowEvent rightArrowEvent;

    public delegate void LeftArrowEvent(bool wiggle);
    public static event LeftArrowEvent leftArrowEvent;

    public bool mediWiggle = false;
    public bool inhaWiggle = false;
    public bool physiWiggle = false;
    public bool foodWiggle = false;
    public bool drinkWiggle = false;
    public bool kreonWiggle = false;

    public bool rightArrowBlink = false;
    public bool leftArrowBlink = false;

    // Use this for initialization
    void Awake ()
    {
        Debug.Log("Awake");
        if (instance != null)
            Destroy(this);
        instance = this;
	}

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckActiveTherapies();
        CheckNeeds();
        CheckKreon();
    }

    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void CheckActiveTherapies()
    {
        //Reset All
        mediWiggle = false;
        inhaWiggle = false;
        physiWiggle = false;
        rightArrowBlink = false;

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
                    mediWiggle = true;
                else if (therapieInfo.duration != 0)
                    inhaWiggle = true;
                else
                    physiWiggle = true;
            }
        }


        if (mediWiggle || inhaWiggle || physiWiggle)
            rightArrowBlink = true;

        if (mediEvent != null)
            mediEvent(mediWiggle);
        if (inhaEvent != null)
            inhaEvent(inhaWiggle);
        if (physiEvent != null)
            physiEvent(physiWiggle);
        if (rightArrowEvent != null)
            rightArrowEvent(rightArrowBlink);
    }

    public void CheckNeeds()
    {
        foodWiggle = false;
        drinkWiggle = false;
        leftArrowBlink = false;

        if (MukiNeeds.s_instance.HungerPoints > 5.0f)
            foodWiggle = true;
        if (MukiNeeds.s_instance.ThirstPoints > 5.0f)
            drinkWiggle = true;

        if (drinkWiggle || foodWiggle)
            leftArrowBlink = true;

        if (foodEvent != null)
            foodEvent(foodWiggle);
        if (drinkEvent != null)
            drinkEvent(drinkWiggle);
        if (leftArrowEvent != null)
            leftArrowEvent(leftArrowBlink);
    }

    public void CheckKreon()
    {

    }
}
