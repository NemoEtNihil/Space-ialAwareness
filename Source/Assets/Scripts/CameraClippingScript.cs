using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClippingScript : MonoBehaviour
{
    public Transform player;

    //List of all objects that we have hidden.
    public List<GameObject> objectsToHide;
    public LayerMask wallMask;
    public Material mat;
    public Material transMat;

    private Vector3 hitBoxSize;
    private float camToPlayerDist;
    private RaycastHit[] hits;

    private void Start()
    {
        objectsToHide = new List<GameObject>();
        wallMask = LayerMask.GetMask("Walls");
        camToPlayerDist = Vector3.Distance(transform.position, player.position);
        hitBoxSize = new Vector3(((Mathf.Tan(Camera.main.fieldOfView) * camToPlayerDist) * 2) / 3, 5, 0.5f);
    }

    void Update()
    {
        hits = Physics.BoxCastAll(transform.position, hitBoxSize, transform.forward, Quaternion.identity, camToPlayerDist, wallMask);

        //hits = Physics.RaycastAll(transform.position, transform.forward, camToPlayerDist, wallMask); //raycast
        Debug.DrawRay(transform.position, transform.forward * 200, Color.red);
        for (int i = 0; i < hits.Length; i++)
        {
            GameObject currentHit = hits[i].transform.gameObject;
            if (!objectsToHide.Contains(currentHit))
            {
                objectsToHide.Add(currentHit);
                //currentHit.GetComponent<Renderer>().enabled = false;
                currentHit.GetComponent<Renderer>().material = transMat;
            }
        }

        for (int i = 0; i < objectsToHide.Count; i++)
        {
            bool isHit = false;

            for (int j = 0; j < hits.Length; j++)
            {
                if (hits[j].transform.gameObject == objectsToHide[i])
                {
                    isHit = true;
                    break;
                }
            }

            if (!isHit)
            {
                GameObject wasHidden = objectsToHide[i];
                //wasHidden.GetComponent<Renderer>().enabled = true;
                wasHidden.GetComponent<Renderer>().material = mat;
                objectsToHide.RemoveAt(i);
                i--;
            }
        }
    }
}
