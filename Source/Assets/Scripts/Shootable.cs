using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shootable : MonoBehaviour
{
    //public Slider healthSlider;
    public int currentHealth;
	public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        //healthSlider.value = currentHealth;
        /*if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }*/
    }
}
