using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWiggler : Wiggler
{ 
    private void OnEnable()
    {
        ButtonWiggleBlinkManager.drinkEvent += CheckWiggle;
    }

    private void OnDisable()
    {
        ButtonWiggleBlinkManager.drinkEvent -= CheckWiggle;
    }
}
