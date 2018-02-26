using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    public Transform target;
    public float time = 0.3F;
    private Vector3 velocity;
    private Vector3 targetPos;

    void Awake()
    {
        velocity = new Vector3();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        targetPos = target.transform.position + new Vector3(0, 15, -15);
        //transform.LookAt(target.position);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, time);
    }
}