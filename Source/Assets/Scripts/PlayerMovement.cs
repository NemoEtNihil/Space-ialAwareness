using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15f;
    public float vel;

    LineRenderer line;
    Vector3 moveVector;
    Rigidbody rb;
    int mouseMask;
    //int enemyMask;
    private Quaternion nuetral = new Quaternion(0, 0, 0, 0);
    private Quaternion forward = new Quaternion(10, 0, 0, 0);
    private Quaternion backward = new Quaternion(-10, 0, 0, 0);
    private Quaternion left = new Quaternion(0, 0, 10, 0);
    private Quaternion right = new Quaternion(0, 0, -10, 0);

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        mouseMask = LayerMask.GetMask("MouseMask");
        //enemyMask = LayerMask.GetMask("EnemyMask");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //shooting
        /*if (Input.GetButtonDown("Fire1"))
            Fire();
        line.enabled = false;*/
        vel = rb.velocity.magnitude;

    }


    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveVector.Set(horizontal, 0f, vertical);
        moveVector = moveVector.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + moveVector);

        Turn();

    }

    void Turn()
    {
        //mouse
        Ray cRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cRay, out hit, 100, mouseMask))
        {
            Vector3 relPos = hit.point - transform.position;
            relPos.y = 0;
            Quaternion rot = Quaternion.LookRotation(relPos);
            rb.MoveRotation(rot);
        }
    }


    void Fire()
    {
        //do something on fire
        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            Vector3[] points = {transform.position, new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z) };
            line.SetPositions(points);
            line.enabled = true;
            if (hit.transform.gameObject.layer == enemyMask)
                hit.transform.gameObject.GetComponent<DieScript>().Die();
        }*/
    }
}
