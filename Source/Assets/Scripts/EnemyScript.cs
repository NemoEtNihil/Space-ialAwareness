using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    [Space]
    [Header("Properties")]
    public float proximity = 6f;
    public float visionDistance = 15f;
    public float stoppingDistance = 10f;

    public float bleedOutTimer = 10f;
    public bool knockedOut = false;
    private float timeKnocked;
    public int maxHealth = 3;


    private bool isLeader = false;
    private Transform target;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private float distance;
    private NavMeshPath path;
    private NavMeshPath partialPath;
    private Animator anim;

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        AssignToGroup();
        if(isLeader)
            SelectPath();
        if (GameControl.enemyPaths[GetGroup()].Count > 0)
            target = GameControl.enemyPaths[GetGroup()].Peek();
        else
            target = player;
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(Running());
    }

    void Update()
    {
        if (!knockedOut)
        {
            distance = Vector3.Distance(transform.position, player.position);
            agent.SetDestination(target.position);
            if (isLeader && agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                if (GameControl.enemyPaths[GetGroup()].Count > 1)
                {
                    GameControl.enemyPaths[GetGroup()].Pop();
                    target = GameControl.enemyPaths[GetGroup()].Peek();
                }
                else
                    target = player;
            }

            if (HasVisual())
            {
                if (distance <= visionDistance)
                {
                    rb.rotation = Quaternion.LookRotation(player.position - transform.position);
                    StartCoroutine("Shoot");
                }
                if (distance <= stoppingDistance)
                {
                    StartCoroutine(Running());
                    agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                }

                if (distance < 10)
                    target = player;
            }
            else
            {
                agent.isStopped = false;
                StartCoroutine(Running());
            }
        }

        if (gameObject.GetComponent<Shootable>().currentHealth <= 0 && !knockedOut)
        {
            StartCoroutine(KnockedVisual());
        }

        //Proof of Concept for Self Raising, just adjust for getting rezzed outside
       /* if (knockedOut && Time.time > timeKnocked) //Self Res
        {
            StartCoroutine(RevivedVisual());
        }*/
        if (knockedOut && Time.time > timeKnocked) //Bleed out
            Die();
    }

    //assigns AI to a group of an AI in the proximity, if there is no ai in the proximity a new group is created
    public void AssignToGroup()
    {
        GameObject nearestAI = GameControl.FindClosestAI(gameObject);
        if (nearestAI != null && Vector3.Distance(nearestAI.transform.position, transform.position) <= proximity && GameControl.enemies[nearestAI.GetComponent<EnemyScript>().GetGroup()].Count < GameControl.currentLevelInfo.squadSize)
        {
            GameControl.enemies[nearestAI.GetComponent<EnemyScript>().GetGroup()].Add(gameObject);
            target = GameControl.enemies[GetGroup()][0].transform;
        }
        else
        {
            MakeLeader();
            List<GameObject> newList = new List<GameObject>();
            newList.Add(gameObject);
            GameControl.enemies.Add(newList);
            GameControl.enemyPaths.Add(new Stack<Transform>());
        }
    }

    //gets the group index of the AI
    public int GetGroup()
    {
        int groupNumber = -1;
        foreach (List<GameObject> group in GameControl.enemies)
        {
            groupNumber++;
            foreach (GameObject ai in group)
            {
                if (ai == gameObject)
                {
                    return groupNumber;
                }
            }
        }
        return -1;
    }

    //passes the leader of the group to the next AI, destroys the group if this is the last AI
    public void PassLeader()
    {
        if (GameControl.enemies[GetGroup()].Count > 1)
        {
            GameControl.enemies[GetGroup()][1].GetComponent<EnemyScript>().MakeLeader();
        }
        else
        {
            GameControl.enemyPaths.RemoveAt(GetGroup());
            GameControl.enemies.RemoveAt(GetGroup());
        }
    }

    //sets values of the AI to the leader values
    public void MakeLeader()
    {
        isLeader = true;
        target = player;
    }

    //checkes if the AI has a visual contact with the player
    public bool HasVisual()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.position - transform.position, out hit))
        {
            if (hit.transform.tag == "Player")
                return true;
        }
        return false;
    }

    //tries to select unique group path
    public void SelectPath()
    {
        if (GameControl.control.waypoints.Length > 0)
        {
            foreach (Transform t in GameControl.control.waypoints)
            {
                foreach (Stack<Transform> s in GameControl.enemyPaths)
                {
                    if (!s.Contains(t))
                    {
                        GameControl.enemyPaths[GetGroup()].Push(t);
                        return;
                    }
                }
            }
            GameControl.enemyPaths[GetGroup()].Push(GameControl.control.waypoints[Random.Range(0, GameControl.control.waypoints.Length)]);
        }
    }

    //organizes the group into a formation
    public void OrganizeGroup()
    {
        //todo
    }

    //kills the AI
    public void Die()
    {
        if (isLeader)
        {
            PassLeader();
        }

        foreach (List<GameObject> group in GameControl.enemies)
        {
            foreach (GameObject e in group)
            {
                if(e == this)
                {
                    group.Remove(e);
                    break;
                }
            }
        }

        Destroy(gameObject);
        ScoreController.score += 10;
    }

    private IEnumerator KnockedVisual()
    {
        knockedOut = true;
        gameObject.GetComponent<EnemyGunController>().enabled = false;
        timeKnocked = Time.time + bleedOutTimer;
        agent.SetDestination(gameObject.transform.position);
        anim.SetTrigger("Knocked");
        yield return new WaitForSeconds(0.4f);
    }

    private IEnumerator RevivedVisual()
    {
        knockedOut = false;
        gameObject.GetComponent<EnemyGunController>().enabled = true;
        agent.SetDestination(target.position);
        gameObject.GetComponent<Shootable>().currentHealth = maxHealth;
        anim.SetTrigger("Revived");
        yield return new WaitForSeconds(0.4f);
    }

    private IEnumerator Running()
    {
        anim.SetTrigger("Running");
        yield return new WaitForSeconds(0.0f);
    }
    private IEnumerator Shoot()
    {
        agent.isStopped = true;
        gameObject.GetComponent<EnemyGunController>().fire();
        //gameObject.GetComponent<EnemyGunController>().sound();
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.4f);
        if (distance > stoppingDistance)
        {
            anim.SetTrigger("ResumeRunning");
            yield return new WaitForSeconds(0.2f);
            agent.isStopped = false;
        }
    }
}
