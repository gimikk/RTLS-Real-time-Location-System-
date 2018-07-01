using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputthekey : MonoBehaviour {
    //   public GameObject itempanel;
    public int gap;
    private int[] tempID = new int[4];
    public Text IDD;
    public static int kimid;
    public GameObject kimsearcj_btn;
 
   // public bool check = false;


    // Use this for initialization
    void Start () {
        gap = 0;
        kimid = 0;
      

    }

    // Update is called once per frame
    void Update() {

        IDD.text = kimid.ToString();
        
        Debug.Log(gap);

    }
    public void triggerMenu(int i)
    {
 
        Debug.Log(i);

        if (i == 10)
        {
            if (gap != 0)
            {
                tempID[gap] = 0;
                gap--;
            }
        }
         else if (gap < 3)
        {
            gap++;
            tempID[gap] = i;
            

        }
        //Debug.Log("click");
        newnum();


    }
   
    public void newnum()
    {
        int tempnum = kimid;
        if (gap == 1) tempnum = tempID[1];
        if (gap == 2) tempnum = tempID[1] * 10 + tempID[2];
        if (gap == 3) tempnum = tempID[1] * 100 + tempID[2] * 10 + tempID[3];
        if (tempnum < 30 && tempnum > 0) { 
        if (kimid != tempnum) kimid = tempnum;
        }

    }

    public void falseclick()
    {
        tempID[1] = 0;
        tempID[2] = 0;
        tempID[3] = 0;
        kimsearch.gimssearch = true;
        kimsearcj_btn.SetActive(false);
        Debug.Log("whoy");
    }
    

}
