using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristCollide : MonoBehaviour {

    //public Renderer renderer;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wrist"))
        {
            
            
            //Renderer renderer = GetComponentInChildren<Renderer>();

            foreach(Transform child in this.transform)
            {
                Renderer renderer = child.GetComponent<Renderer>();

                renderer.material.SetColor("_Color", Color.green);
            }

           
            
            
        }
    }
}
