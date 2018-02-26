using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed;
    public int gunDamage;
    public int currentHealth;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (gameObject.GetComponent<Shootable>().currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bullet") //|| other.gameObject.tag == "Shield")
        {
            Shootable health = other.gameObject.GetComponent<Shootable>();
            if (health != null)
                health.Damage(gunDamage);
        }
        Destroy(gameObject);
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }

}
