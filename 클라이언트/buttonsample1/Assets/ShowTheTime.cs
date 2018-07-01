using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTheTime : MonoBehaviour {
    float thistime = 0f;

    // Use this for initialization
    void Start()
    {
       
    }
	
	// Update is called once per frame
	void Update () {
        float sec = Time.deltaTime;
        sec += Time.deltaTime;
        Debug.Log(sec);
        Text timetext = GetComponent<Text>();
      //  if (sec > 1)
       // {
        //    thistime++;
           // sec = 0;
       // }
            timetext.text = thistime.ToString();
    }
}
