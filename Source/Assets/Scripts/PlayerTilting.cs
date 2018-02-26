using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTilting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Animate(horizontal, vertical);
    }

    /*
   void Animate(float h, float v)
   {
       /*Debug.Log("h: " + h + " v: " + v);
       /*if (h > -1 && h < 1)// && (transform.GetChild(5).rotation.z < 0.01 || transform.GetChild(5).rotation.z > -0.01))
       {
           Debug.Log("Moving h = 0: " + transform.GetChild(5).rotation.z);
           transform.GetChild(5).Rotate(0f, 0f, 0f);
       }
       if (h == 1 && (transform.GetChild(5).rotation.z > -0.01 || transform.GetChild(5).rotation.z == 0))
       {
           Debug.Log("Moving h = 1: " + transform.GetChild(5).rotation.z);
           transform.GetChild(5).Rotate(0f, 0f, -10f, Space.Self);
       }
       if (h == -1 && (transform.GetChild(5).rotation.z < 0.01 || transform.GetChild(5).rotation.z == 0))
       {
           Debug.Log("Moving h = -1: " + transform.GetChild(5).rotation.z);
           transform.GetChild(5).Rotate(0f, 0f, 10f, Space.Self);
       }
       /*if (v == 1 && (transform.GetChild(5).rotation.x > -0.01 || transform.GetChild(5).rotation.x == 0))
       {
           Debug.Log("Moving v = 1: " + transform.GetChild(5).rotation.x);
           transform.GetChild(5).Rotate(10f, 0f, 0f, Space.Self);
       }
       if (v == -1 && (transform.GetChild(5).rotation.x < 0.01 || transform.GetChild(5).rotation.x == 0))
       {
           Debug.Log("Moving v = -1: " + transform.GetChild(5).rotation.x);
           transform.GetChild(5).Rotate(-10f, 0f, 0f, Space.Self);
       }*/


    /*
    if (h == 1 && transform.GetChild(5).rotation.z > 0.01)
    {
        Debug.Log("Moving h = 1: " + transform.GetChild(5).rotation.z);
        transform.GetChild(5).Rotate(0f, 0f, -10f);
    }
    if (h == -1 && transform.GetChild(5).rotation.z < 0.01)
    {
        Debug.Log("Moving h = -1: " + transform.GetChild(5).rotation.z);
        transform.GetChild(5).Rotate(0f, 0f, 10f);
    }
    /*if (h == 0 && transform.GetChild(5).rotation.z)
    {
        Debug.Log("Moving h = 0: " + transform.GetChild(5).rotation.z);
        transform.GetChild(5).Rotate(0, 0, 0);
    }
    /*
    if (h == 1 && (transform.GetChild(5).rotation.z == 0 || transform.GetChild(5).rotation.z < 0))
        transform.GetChild(5).Rotate(0f, 0f, -10f);
    if (h  == -1 && (transform.GetChild(5).rotation.z == 0 || transform.GetChild(5).rotation.z > 0))
        transform.GetChild(5).Rotate(0f, 0f, 10f);
    if (h == 0 && (transform.GetChild(5).rotation.z < 0 || transform.GetChild(5).rotation.z > 0))
        transform.GetChild(5).Rotate(0f, 0f, 0f);
    /*if (h == 0)// && (transform.GetChild(5).rotation.z == 10f || transform.GetChild(5).rotation.z == -10f))
        transform.GetChild(5).Rotate(0f, 0f, 0f);
    //transform.rotation = transform.rotation * new Quaternion(0, 0, -10, 0);
    //transform.rotation = new Quaternion();


    /*
    if (v > 0 && (transform.GetChild(5).rotation.x == 0f || transform.GetChild(5).rotation.x == 10f))
        transform.GetChild(5).Rotate(-10f, 0f, 0f);
    if (v < 0 && (transform.GetChild(5).rotation.x == 0f || transform.GetChild(5).rotation.x == -10f))
        transform.GetChild(5).Rotate(10f, 0f, 0f);
    if (v == 0 && (transform.GetChild(5).rotation.x == 10f || transform.GetChild(5).rotation.x == -10f))
        transform.GetChild(5).Rotate(0f, 0f, 0f);
}*/
}
