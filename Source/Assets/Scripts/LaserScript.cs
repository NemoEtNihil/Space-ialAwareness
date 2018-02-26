using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    public LineRenderer laser;
    public GameObject laserEnd;

    private RaycastHit hit;
    private Ray r;

    void Awake()
    {
    }

    void Update()
    {
        r = new Ray(transform.position, transform.forward);
        laser.SetPosition(0, transform.position);
        if (Physics.Raycast(r, out hit, 100))
        {
            laser.SetPosition(1, hit.point);
            laserEnd.transform.position = hit.point;
            laserEnd.SetActive(true);
        }
        else
        {
            laser.SetPosition(1, r.GetPoint(100));
            laserEnd.SetActive(false);
        }

    }
}
