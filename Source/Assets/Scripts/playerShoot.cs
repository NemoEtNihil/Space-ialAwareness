using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        AudioSource shootClip = GetComponent<AudioSource>();
        if (Input.GetButtonDown("Fire1"))
        {
           
            shootClip.Play();
          
        }
       // if (Input.GetButtonUp("Fire1"))
      //  {
      //      shootClip.Stop();
      //  }
	}
}
