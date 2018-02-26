using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int gunDamage;
    public int currentHealth;
    public bool repulsed;

    private void Start()
    {
        repulsed = false;
    }
    // Use this for initialisation
    void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.tag == "Enemy" && repulsed )|| other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet") //|| other.gameObject.tag == "Shield")
        {
            Shootable health = other.gameObject.GetComponent<Shootable>();
            if (health != null)
                health.Damage(gunDamage);
            //Debug.Log("Id of victim: " + other.gameObject.name + ", health = " + other.gameObject.GetComponent<Shootable>().currentHealth);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (gameObject.GetComponent<Shootable>().currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }

    public void repulsedBullet()
    {
        //Debug.Log("In bullet controller repulsedBullet");
        gameObject.GetComponent<BoxCollider>().size = new Vector3(3,1,1); 
        speed *= -1;
        repulsed = true;
    }
}
