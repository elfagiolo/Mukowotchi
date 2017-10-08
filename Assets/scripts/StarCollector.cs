using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Score"))
        {
            StarBehaviour s = col.gameObject.GetComponent<StarBehaviour>();
            if (s == null)
            {
                Debug.LogError("Every Score-Tagged Object needs a Starbehaviour script attached");
                return;
            }

            s.Disappear();
        }
    }
}
