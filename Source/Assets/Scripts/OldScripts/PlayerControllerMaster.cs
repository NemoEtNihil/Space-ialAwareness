
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMaster : MonoBehaviour
{
    public float moveSpeed;
    public bool useController;
    private Rigidbody myRigidbody;
    //private Rigidbody shieldRigidbody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Camera mainCamera;
    public GameObject theShield;
    public GameObject theGun;
    public float blockTimer = 5f;
    private float nextBlock;
    //public Shootable selfHealth;
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f; //affects rigidbody
    public Transform gunEnd; //the line from the gun
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f); //How long the laser is visible
    private WaitForSeconds chargeDuration = new WaitForSeconds(.01f);
    //private AudioSource gunAudio; //Sound effect for gun
    private LineRenderer laserLine; //Draws straight line between array of 2 points in 3d
    private float nextFire; //holds time until player can fire again
    private float nextCharge;
    public float chargeTimer = 5f;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        //shieldRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //commented movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        //Rotate with Mouse
        if (!useController)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }


        //Rotate with Controller
        if (useController)
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Right_Horizontal") + Vector3.forward * Input.GetAxisRaw("Right_Vertical");
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }

        if ((Input.GetMouseButton(0) || Input.GetAxisRaw("Right_Trigger") != 0) && Time.time > nextFire && !theShield.activeSelf) //Fire1 = ctrl, jump = space
        {
            //GetMouseButtonDown is once per click, GetMouseButton checks every frame
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());

            //Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //gets exact center of fps cam
            //Vector3 rayOrigin = transform.forward;
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

        if(Input.GetMouseButtonDown(1) && nextCharge < Time.time)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, weaponRange) && hit.collider.tag == "Enemy")
            {
                nextCharge = Time.time + chargeTimer;
                //Vector3 targetLocation = new Vector3(hit.point.x, 0, hit.point.z);
                int targetHealth = hit.collider.GetComponent<Shootable>().currentHealth;
                Shootable health = hit.collider.GetComponent<Shootable>();
                health.Damage(targetHealth);
                transform.position = hit.point;
                //StartCoroutine(ChargeTarget(hit));
            }
        }

        if (Input.GetAxisRaw("Left_Trigger") != 0 || Input.GetMouseButton(2))
        {
            if (!theShield.activeSelf && Time.time >= nextBlock)
            {
                nextBlock = Time.time + blockTimer;
                theGun.SetActive(false);
                theShield.SetActive(true);
                theShield.GetComponent<Shootable>().enabled = true;
                theShield.GetComponent<BoxCollider>().enabled = true;
                theShield.GetComponent<Shootable>().currentHealth = 10;
            }
        }

        if ((Time.time >= nextBlock && theShield.activeSelf) || (useController && Input.GetAxisRaw("Left_Trigger") == 0 && theShield.activeSelf || !useController && !Input.GetMouseButton(2) && theShield.activeSelf))
        {
            theShield.GetComponent<Shootable>().enabled = false;
            theShield.GetComponent<BoxCollider>().enabled = false;
            theShield.SetActive(false);
            theGun.SetActive(true);
            Debug.Log("current block timer: " + nextBlock);
            nextBlock = Time.time + blockTimer;
            Debug.Log("next block timer: " + nextBlock);
        }

        if (gameObject.GetComponent<Shootable>().currentHealth <= 0)
        {
            theGun.SetActive(false);
            theShield.SetActive(false);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.SetActive(false);
        }

    }

    private IEnumerator ShotEffect()
    {
        //gunAudio.play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    /*
    private IEnumerator ChargeTarget(RaycastHit hit)
    {
        Vector3 targetLocation = new Vector3(hit.point.x, 0, hit.point.z);
        yield return chargeDuration;
        transform.Translate(targetLocation);
    }
    */
    

    private void FixedUpdate()
    {
        //myRigidbody.velocity = moveVelocity;
    }
}
