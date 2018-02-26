using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject player;
    public GameObject theShield;
    public GameObject theGun;
    public float blockDuration;
    private float nextBlock;
    public bool useController;
    private Vector3 playerDirection;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        //transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        //theShield.transform.rotation = player.transform.localRotation;
       /* if (Input.GetAxisRaw("Left_Trigger") != 0 || Input.GetMouseButton(2))
        {
            if (!theShield.activeSelf && Time.time >= nextBlock)
            {
                nextBlock = Time.time + blockDuration;
                theGun.SetActive(false);
                theShield.SetActive(true);
                theShield.GetComponent<Shootable>().enabled = true;
                theShield.GetComponent<BoxCollider>().enabled = true;
                theShield.GetComponent<Shootable>().currentHealth = 10;
            }
        }

        if (theShield.activeSelf && (Time.time >= nextBlock || useController && Input.GetAxisRaw("Left_Trigger") == 0 || !useController && !Input.GetMouseButton(2) || theShield.GetComponent<Shootable>().currentHealth <= 0))
        {
            theShield.GetComponent<Shootable>().enabled = false;
            theShield.GetComponent<BoxCollider>().enabled = false;
            theShield.SetActive(false);
            theGun.SetActive(true);
            nextBlock = Time.time + blockDuration;
        }
        */
    }


}
