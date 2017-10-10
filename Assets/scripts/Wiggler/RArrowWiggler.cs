using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RArrowWiggler : Wiggler {

    private void OnEnable()
    {
        ButtonWiggleBlinkManager.rightArrowEvent += CheckWiggle;
    }

    private void OnDisable()
    {
        ButtonWiggleBlinkManager.rightArrowEvent -= CheckWiggle;
    }
}
