using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour
{
    public float chargeRange;
    private float nextCharge;
    public float chargeTimer;
    public float chargeAOE;
    public float comboMultiplier;
    public float chargeAOEDamage;
    private int damage;
    public Vector3 area = new Vector3(5f, -0.1f, 5f);
    public Vector3 comboArea = new Vector3(10f, -0.1f, 10f);
    public GameObject chargeField;
    //public GameObject theShield;
    //private WaitForSeconds chargeDuration = new WaitForSeconds(.01f);
    RaycastHit hit;
    public float visualDuration;
    private bool combo;
    private Collider[] colliders;
    private Vector3 destination;
    private bool dashed;
    public int chargeSpeed;
    private Vector3 direction;
    private float oopsTimer;
    private GameObject target;
    private int targetHealth;
    private Shootable health;
    public GameObject ChargeImage;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextCharge) { ChargeImage.SetActive(true); }
        if (Input.GetMouseButtonDown(1) && nextCharge<Time.time)
        {
            if (Time.time >= nextCharge) ChargeImage.SetActive(true);
            if (Physics.Raycast(transform.position, transform.forward, out hit, chargeRange) && hit.collider.tag == "Enemy")
            {
                StartCoroutine(StartCharge());
                hit.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //chargeField.transform.localScale = area;
                nextCharge = Time.time + chargeTimer;
                if (Time.time < nextCharge) { ChargeImage.SetActive(false); }
                //Vector3 targetLocation = new Vector3(hit.point.x, 0, hit.point.z);
                targetHealth = hit.collider.GetComponent<Shootable>().currentHealth;
                health = hit.collider.GetComponent<Shootable>();
                direction = hit.point - transform.position;
                destination = hit.point;
                dashed = true;
                direction.Normalize();
                gameObject.GetComponent<Rigidbody>().AddForce((direction) * chargeSpeed);
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                gameObject.GetComponent<GunController>().enabled = false;
                if (hit.collider.GetComponent<EnemyScript>().knockedOut)
                {
                    damage = (int)(comboMultiplier * chargeAOEDamage);
                    //chargeField.transform.localScale = comboArea;
                }
                target = hit.collider.gameObject;
                gameObject.GetComponent<Collider>().enabled = false;
                //hit.collider.GetComponent<EnemyController>().Death();
                gameObject.GetComponent<GunController>().ammo = gameObject.GetComponent<GunController>().maxAmmo;
                //Debug.Log("Just Reloaded, ammo = " + gameObject.GetComponent<GunController>().ammo);
                //StartCoroutine(ChargeTarget(hit));
                oopsTimer = Time.time + 2;
            }
        }

        /*
        if (transform.forward != direction && dashed)
            transform.forward = direction;
            */

        if(dashed && Time.time >= oopsTimer)
        {
            dashed = false;
            gameObject.GetComponent<PlayerMovement>().enabled = true;
        }

        if (Vector3.Distance(destination, transform.position) < 1 && dashed)
        {
            health.Damage(targetHealth);
            target.GetComponent<EnemyScript>().Die();
            StartCoroutine(EndCharge());
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            colliders = Physics.OverlapSphere(transform.position, chargeAOE);
            gameObject.GetComponent<Collider>().enabled = true;
            foreach (Collider col in colliders)
            {
                try
                {
                    col.GetComponent<Shootable>().currentHealth -= damage;
                }
                catch
                {
                }
            }
            dashed = false;
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            gameObject.GetComponent<GunController>().enabled = true;
        }
    }

  /*  private IEnumerator ChargeVisual()
    {
        chargeField.SetActive(true);
        yield return new WaitForSeconds(visualDuration);
        chargeField.SetActive(false);
    }*/

    private IEnumerator StartCharge()
    {
        anim.SetTrigger("Bash");
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator EndCharge()
    {
        anim.SetTrigger("EndBash");
        chargeField.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        chargeField.SetActive(false);
    }
}
