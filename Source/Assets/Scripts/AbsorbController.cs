using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbController : MonoBehaviour
{

    public float sphereRadius;
    private Collider[] colliders;
    private GameObject player;
    private GameObject target;
    public GameObject absorbField;
    public int absorbAmount;
    //public LayerMask layerMask;
    private float nextAbsorb;
    public float absorbTimer;
    //private bool absorbing;
    public Vector3 area = new Vector3(10f, -0.1f, 10f);
    public float visualDuration;
    public GameObject AbsorbImage;

    // Update is called once per frame
    void LateUpdate ()
    {
        if (Time.time > nextAbsorb) { AbsorbImage.SetActive(true); }
        if (Input.GetButtonDown("Jump") && Time.time > nextAbsorb)
        {
            //absorbing = true;
            //absorbField.transform.localScale = area;
            StartCoroutine(AbsorbVisual());
            colliders = Physics.OverlapSphere(transform.position, sphereRadius);   
            foreach (Collider col in colliders)
            {
                try
                {
                    if (col.gameObject.GetComponent<EnemyScript>().knockedOut)
                    {
                        nextAbsorb = Time.time + absorbTimer;
                        if (Time.time < nextAbsorb) { AbsorbImage.SetActive(false); }
                        col.gameObject.GetComponent<EnemyScript>().Die();
                        gameObject.GetComponent<Shootable>().currentHealth += absorbAmount;
                    }
                }
                catch
                {
                }
            }
        }
    }

    private IEnumerator AbsorbVisual()
    {
        absorbField.SetActive(true);
        yield return new WaitForSeconds(visualDuration);
        absorbField.SetActive(false);
    }
}
