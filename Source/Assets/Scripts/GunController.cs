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
    private Transform temp;
    private LineRenderer laserLine; //Draws straight line between array of 2 points in 3d
    private float nextFire; //holds time until player can fire again
    public WaitForSeconds shotDuration = new WaitForSeconds(.07f); //How long the laser is visible
    ///public GameObject theShield;
    public Slider ammoSlider;
    public int maxAmmo;
    public int ammo;
    private float regenNextAmmo;
    public float regenAmmoTimer;
    RaycastHit hit;
    private Animator anim;
    //public PlayerBullet bullet;
    //public float bulletSpeed;
    //public float speed;
    private int side = -1;

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
    }
}
