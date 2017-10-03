using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public static Score s_instance;

    void Awake()
    {
        if (!s_instance) s_instance = this;
        else Destroy(transform.parent.gameObject);
    }
}
