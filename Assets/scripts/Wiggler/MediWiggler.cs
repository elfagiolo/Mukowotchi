using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediWiggler : Wiggler {

    private void OnEnable()
    {
        ButtonWiggleBlinkManager.mediEvent += CheckWiggle;
    }

    private void OnDisable()
    {
        ButtonWiggleBlinkManager.mediEvent -= CheckWiggle;
    }
}
