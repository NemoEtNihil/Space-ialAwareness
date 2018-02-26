using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f; //affects rigidbody
    public Transform gunEnd; //the line from the gun
    //public Camera fpsCam;


    //private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f); //How long the laser is visible
    //private AudioSource gunAudio; //Sound effect for gun
    private LineRenderer laserLine; //Draws straight line between array of 2 points in 3d
    private float nextFire; //holds time until player can fire again

	void Start ()
    {
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
        //fpsCam = GetComponentInParent<Camera>();
        //fpsCam = GetComponent<Camera>();
	}


    //void Update()

    public void Fire ()
    {
        //if ((Input.GetMouseButton(0) != false || Input.GetAxisRaw("Right_Trigger") != 0) && Time.time > nextFire) //Fire1 = ctrl, jump = space
        if (Time.time > nextFire) //Fire1 = ctrl, jump = space
        {
            //GetMouseButtonDown is once per click, GetMouseButton checks every frame
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());

            //Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //gets exact center of fps cam
            //Vector3 rayOrigin = transform.forward;
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);
            if (Physics.Raycast(transform.position, transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                Shootable health = hit.collider.GetComponent<Shootable>();
                if (health != null)
                    health.Damage(gunDamage);
                if (hit.rigidbody != null)
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
            else
                laserLine.SetPosition(1, transform.position + (transform.forward * weaponRange));

        }
	}

    private IEnumerator ShotEffect()
    {
        //gunAudio.play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
