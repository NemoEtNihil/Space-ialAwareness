using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 6;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100;

    public bool useController;

    // Use this for initialization
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical"); //was raw in tutorial
        movement.Set(h, 0f, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime; //keeps diagonal movement to same distance
        playerRigidBody.MovePosition(transform.position + movement);
        //Move(h, v);
        //Turning();
        //Animating(h, v);
        if (!useController)
        {
            Debug.Log("I'm in Keyboard Only");
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) //out = pass by ref
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse); //Quaternion is a rotation
                playerRigidBody.MoveRotation(newRotation);
            }
        }


        //Rotate with Controller
        if(useController)
        {
            Debug.Log("I'm in Controller Only");
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Right_Horizontal") + Vector3.forward * -Input.GetAxisRaw("Right_Vertical");
            if(playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }
    }

/*
void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime; //keeps diagonal movement to same distance
        playerRigidBody.MovePosition(transform.position + movement);
    }

void Turning()
    {
        //Debug.Log(useController);
        //From tutorial, only works with Mouse
        /*if (!useController)
        {
            Debug.Log("I'm in Keyboard Only");
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) //out = pass by ref
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse); //Quaternion is a rotation
                playerRigidBody.MoveRotation(newRotation);
            }
        }   


        //Rotate with Controller
        // if(useController)
        //{
            Debug.Log("I'm in Controller Only");
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Right_Horizontal") + Vector3.forward * -Input.GetAxisRaw("Right_Vertical");
            if(playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        //}
    }
    */

    /*void Animating (float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }*/
}
