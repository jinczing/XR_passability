using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangRes : MonoBehaviour {

    private static bool isChange;

	// Use this for initialization
	void Start () {
        isChange = false;
        print(Screen.currentResolution);
        Resolution[] resolutions = Screen.resolutions;

        foreach(var res in resolutions)
        {
            print(res);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangResolution()
    {
        if (!isChange)
        {
            Screen.SetResolution(640, 480, true, 60);
            isChange = true;
        } else {
            Screen.SetResolution(1920, 1080, true, 60);
            isChange = false;
        }
        print(Screen.currentResolution);
            
    }
}
