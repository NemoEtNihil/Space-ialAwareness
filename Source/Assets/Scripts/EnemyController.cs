using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //public double enemyRangeMin, enemyRangeMax;
    public float enemyRange;
    public Transform target;
    NavMeshAgent agent;
    public float bleedOutTimer;
    public bool knockedOut;
    private float timeKnocked;
    public int maxHealth;

    void Start()
    {
        //Time.timeScale = 0.1f;
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (!knockedOut)
        {
            agent.SetDestination(target.position);
            if (target.transform.position.x < transform.position.x + enemyRange && target.transform.position.x > transform.position.x - enemyRange && target.transform.position.z < transform.position.z + enemyRange && target.transform.position.z > transform.position.z - enemyRange)
            {
                agent.SetDestination(transform.position);
                transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
                gameObject.GetComponent<EnemyGunController>().fire();
            }
        }
        /*
        if (gameObject.GetComponent<Shootable>().currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }*/

        if (gameObject.GetComponent<Shootable>().currentHealth <= 0 && !knockedOut)
        {
            knockedOut = true;
            gameObject.GetComponent<EnemyGunController>().enabled = false;
            timeKnocked = Time.time + bleedOutTimer;
            agent.SetDestination(gameObject.transform.position);
        }
        if (knockedOut && Time.time > timeKnocked) //Bleed out
            Death();

        /*if (knockedOut && Time.time > timeKnocked) //Ressurected 
        {
            //Debug.Log("Current time: " + Time.time);
            //Debug.Log("Trying to resurrect");
            //Debug.Log("Time Knocked = " + timeKnocked);
            knockedOut = false;
            transform.Rotate(0, 0, -90);
            gameObject.GetComponent<Shootable>().currentHealth = maxHealth;
            gameObject.GetComponent<EnemyGunController>().enabled = true;
        }*/
        if (knockedOut)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
