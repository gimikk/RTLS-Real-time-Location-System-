using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IDd
{
    public bool real;
    public int X;
    public int Y;
}

public class objectpooling : MonoBehaviour {
   public IDd[] id = new IDd[30];
    char[,] data_exist = new char[660, 1180];
    double timer;
    public GameObject human;
    public GameObject kims;
    public GameObject newhuman;
    public GameObject play_ob;
    public List<GameObject> poolobj;
    float sec = 0f;
    float ss = 0;
    
    
    void Start () {
        kims = (GameObject)Instantiate(newhuman);
        kims.transform.parent = play_ob.transform;
        kims.SetActive(false);
        poolobj = new List<GameObject>();

		for(int i=0; i<30; i++)
        {
            if (i % 3 != 0)
            {
                id[i].real = true;
                id[i].X = i * 20;
                id[i].Y = i * 20;
            }
            else id[i].real = false;
        }
        for(int i=0; i<30; i++)
        {
            GameObject obj = (GameObject)Instantiate(human);
           
            obj.transform.parent = play_ob.transform;

            obj.SetActive(false);
            poolobj.Add(obj);
            
        }
	}
	
	// Update is called once per frame
	void Update () {
        sec += Time.deltaTime;
        if(sec>1)
        {
            sec = 0;
            ss++;
            for (int i = 0; i < 30; i++)
            {
                if (i % 3 != 0)
                {
                    id[i].X += 1;
                }
            }
          
        }
        if (ss > 5)
        {
            for (int i = 0; i < 30; i++)
            {
                if (i % 3 == 0)
                {
                    id[i].real = true;
                    id[i].X = i * 8;
                    id[i].Y = i * 3;
                }
                else id[i].real = false;
            }
        }
        poolob();
    }

    void poolob()
    {
        print("hee");
        for(int i=0; i<30; i++)
        {
            if (id[i].real == true)
            {
                Vector3 temp = new Vector3(id[i].X, 0, id[i].Y);
                poolobj[i].transform.position = temp;
                poolobj[i].SetActive(true);
            }
            else poolobj[i].SetActive(false);
        }
        if (kimsearch.gimssearch == true)
        {
            if (id[inputthekey.kimid].real == true)
            {
                poolobj[inputthekey.kimid].SetActive(false);
                Vector3 temp = new Vector3(id[inputthekey.kimid].X, 0, id[inputthekey.kimid].Y);
                kims.transform.position = temp;
                kims.SetActive(true);
            }
            else kims.SetActive(false);

        }
        else kims.SetActive(false);
    }

}
