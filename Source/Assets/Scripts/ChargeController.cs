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

    public float chargeTimer;
    public float chargeAOE;
    public float comboMultiplier;
    public float chargeAOEDamage;
    public GameObject chargeField;
    public float visualDuration;
    public int chargeSpeed;
    public GameObject ChargeImage;
    public Slider slider;
    public float progress;

    private float nextCharge;
    private int damage;
    private RaycastHit hitA, hitB, hitC, hit;
    private bool combo;
    private Collider[] colliders;
    private Vector3 destination;
    private bool dashed;
    private Vector3 direction;
    private float oopsTimer;
    private GameObject target;
    private int targetHealth;
    private Shootable health;
    private Animator anim;
    private Vector3 aimAssist = new Vector3(0.5f, 0f, 0f);
    private Vector3 area = new Vector3(5f, -0.1f, 5f);
    private Vector3 comboArea = new Vector3(10f, -0.1f, 10f);

    //public GameObject theShield;
    //private WaitForSeconds chargeDuration = new WaitForSeconds(.01f);




    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        progress = 5f-(nextCharge - Time.time);
        slider.value = progress;
        if (Time.time > nextCharge) { ChargeImage.SetActive(true); progress = 5f; }
        if (Input.GetMouseButtonDown(1) && nextCharge<Time.time)
        {
            if (Time.time >= nextCharge) ChargeImage.SetActive(true);
                progress = 5f;

            hit = default(RaycastHit);

            Physics.Raycast(transform.position, transform.forward, out hitA, chargeRange);
            Physics.Raycast(transform.position + aimAssist, transform.forward, out hitB, chargeRange);
            Physics.Raycast(transform.position - aimAssist, transform.forward, out hitC, chargeRange);
            if(hitA.transform.tag == "Enemy" || hitB.transform.tag == "Enemy" || hitC.transform.tag == "Enemy")
            {

                EnemyScript enemyScriptA = hitA.transform.GetComponent<EnemyScript>();
                EnemyScript enemyScriptB = hitB.transform.GetComponent<EnemyScript>();
                EnemyScript enemyScriptC = hitC.transform.GetComponent<EnemyScript>();

                if (enemyScriptA != null && enemyScriptB != null && enemyScriptC != null)
                {
                    if (enemyScriptA.knockedOut)
                        hit = hitA;
                    else if (enemyScriptB.knockedOut)
                        hit = hitB;
                    else if (enemyScriptC.knockedOut)
                        hit = hitC;
                    else if (hitA.distance <= hitB.distance && hitA.distance <= hitC.distance)
                        hit = hitA;
                    else if (hitB.distance <= hitA.distance && hitB.distance <= hitC.distance)
                        hit = hitB;
                    else if (hitC.distance <= hitA.distance && hitC.distance <= hitB.distance)
                        hit = hitC;
                }
                else if (enemyScriptA != null && enemyScriptB != null)
                {
                    if (hitA.distance <= hitB.distance)
                        hit = hitA;
                    else if (hitB.distance > hitA.distance)
                        hit = hitB;
                }
                else if (enemyScriptB != null && enemyScriptC != null)
                {
                    if (hitB.distance <= hitC.distance)
                        hit = hitB;
                    else if (hitC.distance > hitB.distance)
                        hit = hitC;
                }
                else if (enemyScriptA != null && enemyScriptC != null)
                {
                    if (hitA.distance <= hitC.distance)
                        hit = hitA;
                    else if (hitC.distance > hitA.distance)
                        hit = hitB;
                }
                else if (enemyScriptA != null)
                    hit = hitA;
                else if (enemyScriptB != null)
                    hit = hitB;
                else if (enemyScriptC != null)
                    hit = hitC;



                //Mostly working code
                /*
                if (hitA.transform.GetComponent<EnemyScript>().knockedOut)
                    hit = hitA;
                else if (hitB.transform.GetComponent<EnemyScript>().knockedOut)
                    hit = hitB;
                else if (hitC.transform.GetComponent<EnemyScript>().knockedOut)
                    hit = hitC;
                //else if(enemyScriptA && hitB.transform.GetComponent<EnemyScript>() && hitC.transform.GetComponent<EnemyScript>())
                else if(hitA.transform.GetComponent<EnemyScript>() && hitB.transform.GetComponent<EnemyScript>() && hitC.transform.GetComponent<EnemyScript>())
                {
                    if (hitA.distance <= hitB.distance && hitA.distance <= hitC.distance)
                        hit = hitA;
                    else if (hitB.distance <= hitA.distance && hitB.distance <= hitC.distance)
                        hit = hitB;
                    else if (hitC.distance <= hitA.distance && hitC.distance <= hitB.distance)
                        hit = hitC;
                }
                else if (hitA.transform.GetComponent<EnemyScript>() && hitB.transform.GetComponent<EnemyScript>())
                {
                    if (hitA.distance <= hitB.distance)
                        hit = hitA;
                    else if (hitB.distance > hitA.distance)
                        hit = hitB;
                }
                else if (hitB.transform.GetComponent<EnemyScript>() && hitC.transform.GetComponent<EnemyScript>())
                {
                    if (hitB.distance <= hitC.distance)
                        hit = hitB;
                    else if (hitC.distance > hitB.distance)
                        hit = hitC;
                }
                else if (hitA.transform.GetComponent<EnemyScript>() && hitC.transform.GetComponent<EnemyScript>())
                {
                    if (hitA.distance <= hitC.distance)
                        hit = hitA;
                    else if (hitC.distance > hitA.distance)
                        hit = hitB;
                }
                else if(hitA.transform.GetComponent<EnemyScript>())
                    hit = hitA;
                else if (hitB.transform.GetComponent<EnemyScript>())
                    hit = hitB;
                else if (hitC.transform.GetComponent<EnemyScript>())
                    hit = hitC;
                    */
                damage = (int)chargeAOEDamage;
                hit.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //chargeField.transform.localScale = area;
                nextCharge = Time.time + chargeTimer;
                progress = 5f - (nextCharge - Time.time);
                slider.value = progress;
                if (Time.time < nextCharge) { ChargeImage.SetActive(false); }
                //Vector3 targetLocation = new Vector3(hit.point.x, 0, hit.point.z);
                targetHealth = hit.transform.GetComponent<Shootable>().currentHealth;
                health = hit.transform.GetComponent<Shootable>();
                direction = hit.point - transform.position;
                destination = hit.point;
                dashed = true;
                direction.Normalize();
                if (Vector3.Distance(destination, transform.position) > 4)
                    StartCoroutine(StartCharge());
                else
                    StartCoroutine(EndCharge());
                gameObject.GetComponent<Rigidbody>().AddForce((direction) * chargeSpeed);
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                gameObject.GetComponent<GunController>().enabled = false;
                gameObject.GetComponent<AbsorbController>().enabled = false;
                gameObject.GetComponent<RepulsionField>().enabled = false;
                target = hit.transform.gameObject;
                gameObject.GetComponent<Collider>().enabled = false;
                //hit.collider.GetComponent<EnemyController>().Death();
                gameObject.GetComponent<GunController>().ammo = gameObject.GetComponent<GunController>().maxAmmo;
                //Debug.Log("Just Reloaded, ammo = " + gameObject.GetComponent<GunController>().ammo);
                //StartCoroutine(ChargeTarget(hit));
                oopsTimer = Time.time + 2;
            }

            /* Working single ray cast
            if (Physics.Raycast(transform.position, transform.forward, out hit, chargeRange) && hit.collider.tag == "Enemy")
            {
                damage = (int)chargeAOEDamage;
                hit.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //chargeField.transform.localScale = area;
                nextCharge = Time.time + chargeTimer;
                progress = 5f - (nextCharge - Time.time);
                slider.value = progress;
                if (Time.time < nextCharge) { ChargeImage.SetActive(false); }
                //Vector3 targetLocation = new Vector3(hit.point.x, 0, hit.point.z);
                targetHealth = hit.collider.GetComponent<Shootable>().currentHealth;
                health = hit.collider.GetComponent<Shootable>();
                direction = hit.point - transform.position;
                destination = hit.point;
                dashed = true;
                direction.Normalize();
                if (Vector3.Distance(destination, transform.position) > 4)
                    StartCoroutine(StartCharge());
                else
                    StartCoroutine(EndCharge());
                gameObject.GetComponent<Rigidbody>().AddForce((direction) * chargeSpeed);
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                gameObject.GetComponent<GunController>().enabled = false;
                gameObject.GetComponent<AbsorbController>().enabled = false;
                gameObject.GetComponent<RepulsionField>().enabled = false;
                target = hit.collider.gameObject;
                gameObject.GetComponent<Collider>().enabled = false;
                //hit.collider.GetComponent<EnemyController>().Death();
                gameObject.GetComponent<GunController>().ammo = gameObject.GetComponent<GunController>().maxAmmo;
                //Debug.Log("Just Reloaded, ammo = " + gameObject.GetComponent<GunController>().ammo);
                //StartCoroutine(ChargeTarget(hit));
                oopsTimer = Time.time + 2;
            }*/
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
            StartCoroutine(EndCharge());
            if (hit.collider.GetComponent<EnemyScript>().knockedOut)
                damage = (int)(comboMultiplier * chargeAOEDamage);
            health.Damage(targetHealth);
            target.GetComponent<EnemyScript>().Die();
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
            gameObject.GetComponent<AbsorbController>().enabled = true;
            gameObject.GetComponent<RepulsionField>().enabled = true;
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
        yield return new WaitForSeconds(0.25f);
    }

    private IEnumerator EndCharge()
    {
        anim.SetTrigger("EndBash");
        chargeField.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        chargeField.SetActive(false);
    }

    private void SuccessfulCharge(RaycastHit rayHit)
    {

    }
}
