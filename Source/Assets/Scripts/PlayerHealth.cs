using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    // Use this for initialization
    public GameObject theGun;
    //public GameObject theShield;
    public int maxOverHealth;
    public int maxHealth;
    public int overHealth;
    public int healthDecrementTimer;
    public int healthDecrementAmount;
    private float decrementNextHealth;
    public Slider healthSlider;
    public Slider overSlider;
    public GameObject hSlide;
    public GameObject oSlide;
    public GameObject GOScreen;


    void Start ()
    {
        gameObject.GetComponent<Shootable>().currentHealth = maxHealth;
	}

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Shootable>().currentHealth > maxHealth)
            overHealth = gameObject.GetComponent<Shootable>().currentHealth - maxHealth;
        else
        {
            overHealth = 0;
            decrementNextHealth = Time.time + healthDecrementTimer;
        }
        healthSlider.value = gameObject.GetComponent<Shootable>().currentHealth;
		//theGun.SetActive(false);
		overSlider.value = gameObject.GetComponent<Shootable>().currentHealth;
        //Debug.Log("Health: " + gameObject.GetComponent<Shootable>().currentHealth + " Over: " + overHealth);

        if (gameObject.GetComponent<Shootable>().currentHealth > maxHealth)
        {
            oSlide.SetActive(true);
            hSlide.SetActive(false);
            /*if (gameObject.GetComponent<Shootable>().currentHealth > maxHealth + maxOverHealth)
                gameObject.GetComponent<Shootable>().currentHealth = maxOverHealth + maxHealth;
            
           overHealth = (gameObject.GetComponent<Shootable>().currentHealth) - maxHealth;*/
            
            overSlider.value = gameObject.GetComponent<Shootable>().currentHealth;
        }
            
        if (overHealth > 0 && Time.time > decrementNextHealth)
        {
            overSlider.enabled = true;
            oSlide.SetActive(true);
            hSlide.SetActive(false);
            gameObject.GetComponent<Shootable>().currentHealth = gameObject.GetComponent<Shootable>().currentHealth - healthDecrementAmount;
            overSlider.value = gameObject.GetComponent<Shootable>().currentHealth; theGun.SetActive(false);
            //Debug.Log("Over Health: " + overHealth + ", Current Health: " + gameObject.GetComponent<Shootable>().currentHealth);
        }

        if (gameObject.GetComponent<Shootable>().currentHealth <= 100)
        {
            oSlide.SetActive(false);
            overSlider.enabled = false;
            healthSlider.enabled= true;
            hSlide.SetActive(true);
            healthSlider.value = gameObject.GetComponent<Shootable>().currentHealth;
        }
        if (gameObject.GetComponent<Shootable>().currentHealth <= 0)
        {
            
            oSlide.SetActive(false);
            hSlide.SetActive(true);
            healthSlider.value = gameObject.GetComponent<Shootable>().currentHealth; theGun.SetActive(false);
            //theShield.SetActive(false);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.SetActive(false);
            GOScreen.SetActive(true);

        }
    }
}
