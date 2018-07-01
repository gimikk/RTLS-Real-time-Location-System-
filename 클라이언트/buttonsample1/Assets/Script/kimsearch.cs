using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kimsearch : MonoBehaviour {
    public static bool gimssearch = false;
    public GameObject kimsearcj_btn;
    public GameObject Left;
    public GameObject Right;
    public int rlf;


    //  public enum mkey;
    // Use this for initialization
    void Start () {
        rlf = 1;
	}
	
	// Update is called once per frame
	void Update () {

        if (gimssearch == true)
        {
            Debug.Log("들어왔습" + rlf);
            rlf = txtReader2.kim_position;
            if (rlf == 0)
            {
                Left.SetActive(true);
                Right.SetActive(false);

            }
            else if (rlf == 2)
            {
                Left.SetActive(false);
                Right.SetActive(true);
            }
            else
            {
                Left.SetActive(false);
                Right.SetActive(false);
            }
        }
        
    }
    public void Onclick()
    {
        Debug.Log("click");
        if (gimssearch == true) gimssearch = false;
        else gimssearch = true;
        if (gimssearch == true)
        {
            inputthekey.kimid = 0;
            kimsearcj_btn.SetActive(true);
         
        }
    }


   
}
