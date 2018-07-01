using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimeControl : MonoBehaviour {
    public InputField Hour;
    public InputField Minute;
    public Button hourPlus;
    public Button hourMinus;
    public Button minutePlus;
    public Button minuteMinus;
    public Button Confirm;
    public string hourTime;
    public string minTime;
    public Text setTime;
    string timeStr;
    public static string s;
    float sec = 0f;
    float ss = 0f;
    int hT;
    int mT;

    // Use this for initialization
    void Start () {
        hourTime = "13";
        Hour.text = hourTime;
        minTime = "21";
        Minute.text = minTime;
        s = hourTime + minTime + "0" + ss.ToString();
        timeStr = hourTime + ":" + minTime + ":" + "00";
        setTime.text = "Time : " + timeStr;
    }
	
	// Update is called once per frame
	void Update () {
        hourPlus.onClick.AddListener(HourPlus);
        hourMinus.onClick.AddListener(HourMinus);
        minutePlus.onClick.AddListener(MinPlus);
        minuteMinus.onClick.AddListener(MinMinus);

        hourTime = Hour.text;
        minTime = Minute.text;

        Confirm.onClick.AddListener(confirm);
        sec += Time.deltaTime;
        if(sec>1)
        {
            sec = 0;
            ss++;
        }
        if (ss > 59)
        {
            mT = System.Convert.ToInt32(minTime);
            mT++;
            minTime = mT.ToString();
            if (mT < 10)
                Minute.text = "0" + System.Convert.ToString(mT);
            else
                Minute.text = System.Convert.ToString(mT);

            ss = 0;
        }
        if(ss<10) timeStr = hourTime + ":" + minTime + ":" +"0"+ ss;
        else timeStr = hourTime + ":" + minTime + ":" + ss;
        setTime.text = "Time : " + timeStr;
        setTime.text = "Time : " + timeStr;
        if (ss < 10 && System.Convert.ToInt32(minTime) < 10) s = hourTime + "0" + minTime + "0" + ss.ToString();
        else if(ss < 10) s = hourTime + minTime + "0" + ss.ToString();
        else if(System.Convert.ToInt32(minTime) < 10) s = hourTime + "0" + minTime + ss.ToString();
        else s = hourTime + minTime + ss.ToString();

    }

    void HourPlus()
    {
        hT = System.Convert.ToInt32(hourTime);
        hT++;
       
        if (hT > 24)
            hT = 1;

        if (hT < 10)
            Hour.text = "0" + System.Convert.ToString(hT);
        else
            Hour.text = System.Convert.ToString(hT);

    }

    void HourMinus()
    {
        hT = System.Convert.ToInt32(hourTime);
        hT--;
      
        if (hT < 1)
            hT = 24;

        if (hT < 10)
            Hour.text = "0" + System.Convert.ToString(hT);
        else
            Hour.text = System.Convert.ToString(hT);
   
    }

    void MinPlus()
    {
        mT = System.Convert.ToInt32(minTime);
        mT++;

        if (mT > 59)
            mT = 0;

        if (mT < 10)
            Minute.text = "0" + System.Convert.ToString(mT);
        else
            Minute.text = System.Convert.ToString(mT);
    }

    void MinMinus()
    {
        mT = System.Convert.ToInt32(minTime);
        mT--;
    
        if (mT < 0)
            mT = 59;

        if (mT < 10)
            Minute.text = "0" + System.Convert.ToString(mT);
        else
            Minute.text = System.Convert.ToString(mT);
    }

    void confirm()
    {
        hourTime = hT.ToString();
        minTime = mT.ToString();
        s = hourTime + minTime + ss.ToString();
        ss = 0;

    }
}
