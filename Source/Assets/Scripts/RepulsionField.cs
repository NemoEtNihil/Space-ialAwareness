using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepulsionField : MonoBehaviour {

    //public GameObject repulseField;
    public GameObject repulseFX;
    public float repulseDuration;
    public float repulseTimer;
    public float sphereRadius;
    private float nextRepulse;
    private float repulseEnd;
    private bool repulsing;
    private Collider[] colliders;
    public Vector3 area;
    public GameObject RFImage;


    /*private void Start()
    {
        repulseField.transform.localScale = area;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextRepulse) RFImage.SetActive(true);
        if ((Input.GetAxisRaw("Left_Trigger") != 0 || Input.GetMouseButtonDown(2)) && Time.time > nextRepulse)
        {
            if (Time.time >= nextRepulse) RFImage.SetActive(true);
            //if (Time.time > nextRepulse) { RFImage.SetActive(true); }
            //Debug.Log("In repulse trigger: " + Time.time);
            nextRepulse = Time.time + repulseTimer + repulseDuration;

            repulseEnd = Time.time + repulseDuration;
            //repulseField.SetActive(true);
            repulseFX.SetActive(true);
            repulsing = true;
            RFImage.SetActive(false);
            if (Time.time < nextRepulse) { RFImage.SetActive(false); }
        }
        if (Time.time <= repulseEnd && repulsing)
        {
            //Debug.Log("In repulse duration: " + Time.time);
            colliders = Physics.OverlapSphere(transform.position, sphereRadius);
            foreach (Collider col in colliders)
            {
                try
                {
                    if (!col.gameObject.GetComponent<BulletController>().repulsed)
                        col.gameObject.GetComponent<BulletController>().repulsedBullet();
                }
                catch
                {
                }
            }
        }
        else if(repulsing && Time.time >= repulseEnd)
        {
            //Debug.Log("In repulse end: " + Time.time);
            //repulseField.SetActive(false);
            repulseFX.SetActive(false);
            repulsing = false;
        }
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        try
        {
            if (!other.gameObject.GetComponent<BulletController>().repulsed)
                other.gameObject.GetComponent<BulletController>().repulsedBullet();
        }
        catch
        {
        }
    }*/
}

