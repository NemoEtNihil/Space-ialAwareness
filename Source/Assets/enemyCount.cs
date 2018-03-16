using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyCount : MonoBehaviour
{

    public static int currentEnemyCount;
    public Text text;
    void Start()
    {
        text = GetComponent<Text>();
        currentEnemyCount = GameControl.control.currentEnemyCount;

    }


    void Update()
    {
        currentEnemyCount = GameControl.control.currentEnemyCount;
        text.text = "Enemies Remaining:" + currentEnemyCount;
    }
}