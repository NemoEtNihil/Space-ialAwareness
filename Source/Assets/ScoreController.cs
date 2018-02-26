using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public static int score;
    public Text text;
    void Start()
    {
        text = GetComponent<Text>();
        score = GameControl.score;
    }


    void Update()
    {
        
        text.text = "Score:" + score;
    }
}
