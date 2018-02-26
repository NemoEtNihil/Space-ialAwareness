using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Formations : MonoBehaviour {

    public float defaultDistance = 2f;

    public enum Shape { Triangle, Rectangle, Line };
    public Dictionary<Shape, Formation> formations = new Dictionary<Shape, Formation>();

    void Awake()
    {
        Formation f;
        
        //Triangle formation
        f = new Formation();
        f.Distance = defaultDistance;
        f.StartingWidth = 2;
        f.NextRowWidthBonus = 1;
        formations.Add(Shape.Triangle, f);

        //Rectangle formation
        f = new Formation();
        f.Distance = defaultDistance;
        f.StartingWidth = 3;
        f.NextRowWidthBonus = 0;
        formations.Add(Shape.Rectangle, f);

        //Line formation
        f = new Formation();
        f.Distance = defaultDistance;
        f.StartingWidth = 1;
        f.NextRowWidthBonus = 0;
        formations.Add(Shape.Line, f);
    } 
}
