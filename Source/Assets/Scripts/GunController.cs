using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public int gunDamage;
    public float fireRate;
    public float weaponRange;
    public float hitForce; //affects rigidbody
    public Transform gunEndRight; //the line from the gun
    public Transform gunEndLeft;
    public float regenAmmoTimer;
    public Slider ammoSlider;
    public int maxAmmo;
    public int ammo;
    public WaitForSeconds shotDuration = new WaitForSeconds(.07f); //How long the laser is visible
    public GameObject shotFXFront;
    public GameObject shotFXBack;


    private Transform temp;
    private LineRenderer laserLine; //Draws straight line between array of 2 points in 3d
    private float nextFire; //holds time until player can fire again
    private float regenNextAmmo;
    private Animator anim;
    private int side = -1;
    private Vector3 aimAssist = new Vector3(0.5f,0f,0f);
    private RaycastHit hitA, hitB, hitC;
    private Vector3 aimPoint;
    private Shootable health;

    //public GameObject theShield;
    //public PlayerBullet bullet;
    //public float bulletSpeed;
    //public float speed;
    //RaycastHit hit;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start ()
    {
        laserLine = GetComponent<LineRenderer>();
        ammo = maxAmmo;
        //Time.timeScale = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        ammoSlider.value = ammo;
		if (ammo < maxAmmo && Time.time > regenNextAmmo)
        {
            regenNextAmmo = Time.time + regenAmmoTimer;
            ammo++;
            ammoSlider.value = ammo;
            //Debug.Log("Regened Ammo: " + ammo);
        }
            
        if ((Input.GetMouseButtonDown(0) || Input.GetAxisRaw("Right_Trigger") != 0) && ammo > 0 && Time.time > nextFire) //&& !theShield.activeSelf) //Fire1 = ctrl, jump = space
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        AudioSource shootClip = GetComponent<AudioSource>();
        nextFire = Time.time + fireRate;
        StartCoroutine(ShotEffect());
        ammo--;
        ammoSlider.value = ammo;
        if (Input.GetButtonDown("Fire1"))
        {
            shootClip.Play();
        }
        //PlayerBullet newBullet = Instantiate(bullet, gunEnd.position, gunEnd.rotation) as PlayerBullet;
        //newBullet.gunDamage = gunDamage;
        //newBullet.speed = bulletSpeed;
        //newBall.rigidbody.velocity = (hit.point - transform.position).normalized * speed; -example code

    }

    private IEnumerator ShotEffect()
    {
        //gunAudio.play();
        //anim.SetBool("Fire", true);
        side *= -1;
        if (side == 1)
        {
            anim.SetTrigger("Fire_Right");
            temp = gunEndRight;
        }
        else
        {
            anim.SetTrigger("Fire_Left");
            temp = gunEndLeft;
        }
        yield return new WaitForSeconds(0.2f);

        laserLine.SetPosition(0, temp.position);

        Physics.Raycast(transform.position, transform.forward, out hitA, weaponRange);
        Physics.Raycast(transform.position + aimAssist, transform.forward, out hitB, weaponRange);
        Physics.Raycast(transform.position - aimAssist, transform.forward, out hitC, weaponRange);

        if (hitA.collider.GetComponent<Shootable>() || hitB.collider.GetComponent<Shootable>() || hitC.collider.GetComponent<Shootable>())
        {
            Shootable shootableA = hitA.transform.GetComponent<Shootable>();
            Shootable shootableB = hitB.transform.GetComponent<Shootable>();
            Shootable shootableC = hitC.transform.GetComponent<Shootable>();

            if (shootableA != null && shootableB != null && shootableC != null)
            {
                if (hitA.distance <= hitB.distance && hitA.distance <= hitC.distance)
                    successfulShot(hitA);
                else if (hitB.distance <= hitA.distance && hitB.distance <= hitC.distance)
                    successfulShot(hitB);
                else if (hitC.distance <= hitA.distance && hitC.distance <= hitB.distance)
                    successfulShot(hitC);
            }
            else if (shootableA != null && shootableB != null)
            {
                if (hitA.distance <= hitB.distance)
                    successfulShot(hitA);
                else if (hitB.distance > hitA.distance)
                    successfulShot(hitB);
            }
            else if (shootableB != null && shootableC != null)
            {
                if (hitB.distance <= hitC.distance)
                    successfulShot(hitB);
                else if (hitB.distance > hitC.distance)
                    successfulShot(hitC);
            }
            else if (shootableA != null && shootableC != null)
            {
                if (hitA.distance <= hitC.distance)
                    successfulShot(hitA);
                else if (hitA.distance > hitC.distance)
                    successfulShot(hitC);
            }
            else if (shootableA != null)
                successfulShot(hitA);
            else if (shootableB != null)
                successfulShot(hitB);
            else if (shootableA != null)
                successfulShot(hitC);
        }
        else if (!(hitA.collider.GetComponent<Shootable>() || hitB.collider.GetComponent<Shootable>() || hitC.collider.GetComponent<Shootable>()))
        {
            laserLine.SetPosition(1, hitA.point);
            shotFXFront.transform.position = hitA.point;
            shotFXFront.transform.position = hitA.point;
        }
        else
        {
            laserLine.SetPosition(1, transform.position + (transform.forward * weaponRange));
            shotFXFront.transform.position = transform.position + (transform.forward * weaponRange);
            shotFXBack.transform.position = transform.position + (transform.forward * weaponRange);
        }
        laserLine.enabled = true;
        shotFXFront.SetActive(true);
        yield return shotDuration;
        laserLine.enabled = false;
        shotFXFront.SetActive(false);
        shotFXBack.SetActive(false);
        //anim.SetBool("Fire", false);

        /* Working for single ray cast
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
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
        //anim.SetBool("Fire", false);
        */
    }

    private void successfulShot(RaycastHit targetHit)
    {
        laserLine.SetPosition(1, targetHit.point);
        shotFXFront.transform.position = targetHit.point;
        shotFXBack.transform.position = targetHit.point;
        if (targetHit.transform.tag == "Enemy")
            shotFXBack.SetActive(true);
        health = targetHit.collider.GetComponent<Shootable>();
        if (health != null)
            health.Damage(gunDamage);
        if (targetHit.rigidbody != null)
            targetHit.rigidbody.AddForce(-targetHit.normal * hitForce);
    }
}
