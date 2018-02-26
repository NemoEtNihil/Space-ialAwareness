using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadarObject
{
    public Image Icon { get; set; }
    public GameObject Owner { get; set; }
}
public class radar : MonoBehaviour {
    public Transform playerPos;
    float MapScale = 2f;

    public static List<RadarObject> radObjects = new List<RadarObject>();

    public static void RegisterRadarObject(GameObject o, Image i)
    {
        Image image = Instantiate(i);
        radObjects.Add(new RadarObject() { Owner = o, Icon = image});
    }

    public static void RemoveRadarObject(GameObject o)
    {
        List<RadarObject> newList = new List<RadarObject>();
            for (int i = 0; i < radObjects.Count; i++)
        {
            if (radObjects[i].Owner == o)
            {
                Destroy(radObjects[i].Icon);
                continue;
            }
            else
                newList.Add(radObjects[i]);
        }
        radObjects.RemoveRange(0, radObjects.Count);
        radObjects.AddRange(newList);
    }

    void DrawRadarDots()
    {
        foreach (RadarObject Ro in radObjects)
        {
            Vector3 radarPos = ((Ro.Owner.transform.position - playerPos.position) * MapScale);
            Ro.Icon.transform.SetParent(this.transform);
            Ro.Icon.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + this.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
        DrawRadarDots();
	}
}
