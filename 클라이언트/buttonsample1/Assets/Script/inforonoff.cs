using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inforonoff : MonoBehaviour {
    public static bool info_on = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Onclick()
    {
        if (info_on == true)
            info_on = false;

        else info_on = true;
    }
}
