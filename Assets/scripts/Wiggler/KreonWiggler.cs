using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KreonWiggler : Wiggler {

    private void OnEnable()
    {
        ButtonWiggleBlinkManager.kreonEvent += CheckWiggle;
    }

    private void OnDisable()
    {
        ButtonWiggleBlinkManager.kreonEvent -= CheckWiggle;
    }
}
