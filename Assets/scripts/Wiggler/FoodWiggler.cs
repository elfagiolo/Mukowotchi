using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodWiggler : Wiggler
{
    private void OnEnable()
    {
        ButtonWiggleBlinkManager.foodEvent += CheckWiggle;
    }

    private void OnDisable()
    {
        ButtonWiggleBlinkManager.foodEvent -= CheckWiggle;
    }
}
