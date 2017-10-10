using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LArrowWiggler : Wiggler {

    private void OnEnable()
    {
        ButtonWiggleBlinkManager.leftArrowEvent += CheckWiggle;
    }

    private void OnDisable()
    {
        ButtonWiggleBlinkManager.leftArrowEvent -= CheckWiggle;
    }
}
