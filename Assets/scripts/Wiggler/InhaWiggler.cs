using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhaWiggler : Wiggler
{
    private void OnEnable()
    {
        ButtonWiggleBlinkManager.inhaEvent += CheckWiggle;
    }

    private void OnDisable()
    {
        ButtonWiggleBlinkManager.inhaEvent -= CheckWiggle;
    }
}
