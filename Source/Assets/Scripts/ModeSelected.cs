using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelected : MonoBehaviour {

    public bool judgeMode = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
