using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class text2 : MonoBehaviour
{
    Quaternion currentHeading;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Text txtThis = GetComponent<Text>();
        currentHeading = Quaternion.Euler(0, Input.compass.trueHeading, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentHeading, Time.deltaTime * 2f);
        txtThis.text = Input.compass.trueHeading.ToString();
    }
}
