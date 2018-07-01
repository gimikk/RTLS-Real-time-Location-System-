using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class text1 : MonoBehaviour {
    public static string compass;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Text txtThis = GetComponent<Text>();

        txtThis.text = Input.compass.magneticHeading.ToString();
    }
}
