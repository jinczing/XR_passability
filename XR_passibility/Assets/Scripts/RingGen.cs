using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGen : MonoBehaviour {

    // poly: how many edge the polygon has, bigR: the radius of the polygon (intersect with the connection point of edges), smallR: the radius of the cylinder
    public int poly;
    public float bigR;
    public float smallR;

    // used to assemble the ring
    public GameObject component;

    // d: the vertical distance between center of the polygon and the edgy
    private float angle; // the unit of angle is radian, not degree!!!
    private float len;
    private float d;
    


	// Use this for initialization
	void Start () {

        // simplified notation
        float R = bigR;
        float r = smallR;

        // initialize reusable variable
        angle = 360 / poly * Mathf.Deg2Rad;
        len = Mathf.Abs(R * Mathf.Sin(angle / 2) * 2);
        d = R * Mathf.Cos(angle / 2) * 0.75f;

        // angle(in rad) for each component respectively
        float degree = 0;

        GameObject[] objs = new GameObject[poly];

        for(int i=0; i<poly; i++)
        {
            objs[i] = Instantiate(component);
            objs[i].transform.SetParent(this.transform);
            
            // set to the default position so that subsequent operation will be more easy
            objs[i].transform.position += new Vector3(0, d, 0);

            // calculate and assign position calculated from angle of current obj
            objs[i].transform.position += new Vector3(d * Mathf.Sin(degree), d * Mathf.Cos(degree) - d, 0);


            // rotate to correct orientation
            objs[i].transform.Rotate(0, 0, (-1) * degree * Mathf.Rad2Deg);

            // adjust scale based on bigR and smallR
            objs[i].transform.localScale = Vector3.Scale(objs[i].transform.localScale, new Vector3(2 * r, 0.5f * len, 2 * r));

            degree += angle;
        }
        


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
