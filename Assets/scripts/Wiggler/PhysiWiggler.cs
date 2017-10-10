using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysiWiggler : Wiggler {

    private void OnEnable()
    {
        ButtonWiggleBlinkManager.physiEvent += CheckWiggle;
    }

    private void OnDisable()
    {
        ButtonWiggleBlinkManager.physiEvent -= CheckWiggle;
    }
}
