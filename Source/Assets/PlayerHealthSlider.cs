using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour {
    public Slider healthSlider;
    public int currentHealth;
    public void takeDamage()
    {
        healthSlider.value =currentHealth;
    }
}
