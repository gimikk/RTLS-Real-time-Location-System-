using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Timers;
using System.Collections;

public struct ID{

    public bool real;
    public GameObject obj;
    public int X;
    public int Y;

    public ID(bool real_t, GameObject obj_t, int X_t, int Y_t)
    {
        real = real_t;
        obj = obj_t;
        X = X_t;
        Y = Y_t; 
    }
}

//사용자 객체 정보
public struct Info
{
    public GameObject obj;
    //0이면 남자 1이면 여자
    public int sex;
    public int age;
    public int X;
    public int Y;

    public Info(GameObject obj_t, int sex_t, int age_t,int X_t, int Y_t)
    {
        obj = obj_t;
        sex = sex_t;
        age = age_t;
        X = X_t;
        Y = Y_t;

    }
}

public class txtReader2 : MonoBehaviour
{
    //ID 가 없으면 0, ID가 있으면 1~30

    char[] delimiterChars = { ' ' };
    public GameObject human;
    public GameObject info_state;
    //ID 1~30까지 사용
    public static List<ID> poolList = new List<ID>();
    public static List<Info> poolinfo = new List<Info>();

    public String host = "10.70.17.143";
    public Int32 port = 40000;
    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;
    StreamWriter the_writer;
    StreamReader the_reader;
    public GameObject newhuman;
    GameObject kims;
    GameObject info;
    public static int kim_position=1;

    //객체를 미리 생성해줌
    public void CreateObj()
    {
        //human 생성
        GameObject newInstance = Instantiate(human);
        
        Vector4 col = new Vector4(UnityEngine.Random.Range(0.1f, 1.0f), UnityEngine.Random.Range(0.1f, 1.0f),UnityEngine.Random.Range(0.1f, 1.0f), UnityEngine.Random.Range(0.1f, 1.0f));
        newInstance.GetComponent<MeshRenderer>().material.color = col;
        ID t = new ID(false, newInstance, 0, 0);
        poolList.Add(t);

        GameObject newInfo = Instantiate(info_state);
        float n_age = UnityEngine.Random.Range(10, 50);
        float n_sex = UnityEngine.Random.Range(0.1f, 1.9f);
        Info information = new Info(newInfo,(int)n_sex ,(int)n_age,0,0);
        poolinfo.Add(information);

        
    }

    void Render(int id,int real,float x, float y)
    {
        //1으로 오브젝트 활성화
        if (real == 1 && inforonoff.info_on == true)
        {
            //객체 풀링
            poolList[id].obj.SetActive(true);
            poolList[id].obj.transform.position = new Vector3(x*4, 0, y*4);
            //객체 정보 풀링
            poolinfo[id].obj.SetActive(true);
            Info temp = poolinfo[id];
            temp.X = (int)x;
            temp.Y = (int)y;
            poolinfo[id] = temp;
            poolinfo[id].obj.transform.position = new Vector3(x * 4, 10, y * 4);
            //정보 넣기
            if (temp.sex == 0)
            {
                poolinfo[id].obj.GetComponent<TextMesh>().text = "ID : " + Convert.ToString(id) + "\n" + "나이 : " + Convert.ToString(temp.age)
                    + "\n" + "성별 : 남자 \n" + Convert.ToString(temp.X) + "\n" + Convert.ToString(temp.Y);
            }
            else
            {
                poolinfo[id].obj.GetComponent<TextMesh>().text = "ID : " + Convert.ToString(id) + "\n" + "나이 : " + Convert.ToString(temp.age)
                    + "\n" + "성별 : 여자 \n" + Convert.ToString(temp.X) + "\n" + Convert.ToString(temp.Y);
            }
        }

        else if (real == 1 && inforonoff.info_on == false)
        {
            poolList[id].obj.SetActive(true);
            poolList[id].obj.transform.position = new Vector3(x * 4, 0, y * 4);

            poolinfo[id].obj.SetActive(false);
        }
        //0으로 오브젝트 죽임
        else if(real == 0)
        {
            poolList[id].obj.SetActive(false);
            poolList[id].obj.transform.position = new Vector3(x*4, 0, y*4);

            poolinfo[id].obj.SetActive(false);
        }
    }

    void Awake()
    {
        setupSocket();

        GameObject obj = Instantiate(human);
        ID t = new ID(false,obj , 0, 0);
        poolList.Add(t);
        //첫번째는 가짜 데이터
        for (int i = 1; i < 31; i++)
        {
            //객체 생성과 동시에 다 꺼버린다.
            CreateObj();
            poolList[i].obj.SetActive(false);
        }
        kims = (GameObject)Instantiate(newhuman);
        kims.SetActive(false);
    }

    public void setupSocket()
    {
        try
        {
            tcp_socket = new TcpClient(host, port);
            net_stream = tcp_socket.GetStream();
            the_writer = new StreamWriter(net_stream);
            the_reader = new StreamReader(net_stream);
            socket_ready = true;
        }

        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    //0.1초 마다 호출
    void FixedUpdate()
    {
 
        //네트워크 연결, 데이터 스트림을 받아옴
        StartClient();
    }

    public void StartClient()
    {
        string key_stroke = Input.inputString;
        string theta = Input.compass.magneticHeading.ToString();
       
        //ID 저장
        string kim = "";
        if (kimsearch.gimssearch == true)
        {
            if(inputthekey.kimid!=0)
            kim = inputthekey.kimid.ToString();
        }
        string message = TimeControl.s+ "0" + " " + theta + " " + kim;
        //string message = Convert.ToString(send_timer);
        writeSocket(message); //현재 시간을 보내주면
        readSocket();
        //kim 프리팹 on/off
        if (kimsearch.gimssearch == true)
        {
            kims.SetActive(true);
            kims.transform.position = new Vector3(poolList[inputthekey.kimid].obj.transform.position.x, 10, poolList[inputthekey.kimid].obj.transform.position.y);
            poolList[inputthekey.kimid].obj.SetActive(false);
        }
        else
            kims.SetActive(false);

    }

    public void readSocket()
    {
        if (!socket_ready)
        {
            Debug.Log("소켓이 열리지 않았습니다");
            return;
        }
        if (!net_stream.DataAvailable)
        {
            Debug.Log("받은 데이터가 없습니다");
            return;
        }

        try
        {
            int BUFFERSIZE = tcp_socket.ReceiveBufferSize;
            byte[] buffer = new byte[BUFFERSIZE];
            int bytes = net_stream.Read(buffer, 0, buffer.Length);

            string message = Encoding.UTF8.GetString(buffer, 0, bytes);
            //150 * 5 = 600개의 인덱스   

            string[] IDs_temp = message.Split(delimiterChars);
            for (int j = 0; j < 5; j++)
            {
                StartCoroutine(StartRendering(j, IDs_temp));
            }
            kim_position = int.Parse(IDs_temp[600]);
        }
        catch (Exception e)
        {
            Debug.Log("Read error : " + e);
            return;
        }
            return;
    }

    IEnumerator<WaitForSeconds> StartRendering(int j,string[] IDs_temp)
    {
        for (int i = 0; i < 30; i++)
        {
            int ID_temp = int.Parse(IDs_temp[4 * i + j * 120]);
            int Real_temp = int.Parse(IDs_temp[4 * i + j * 120 + 1]);
            float X_temp = float.Parse(IDs_temp[4 * i + j * 120 + 2]);
            X_temp /= 10;
            float Y_temp = float.Parse(IDs_temp[4 * i + j * 120 + 3]);
            Y_temp /= 10;
            //오브젝트 풀링을 해줌
            Render(ID_temp, Real_temp, X_temp, Y_temp);
        }
        yield return new WaitForSeconds(0.1f);
    }
   
    public void writeSocket(string line)
    {
        if (!socket_ready)
            return;
        the_writer.Write(line);
        the_writer.Flush();
    }

   // void OnApplicationQuit()
   //{
   //     closeSocket();
   // }
   //
   // public void closeSocket()
   // {
   //     if (!socket_ready)
   //         return;
   //     socket_writer.Close();
   //     socket_reader.Close();
   //     tcp_socket.Close();
   //    socket_ready = false;
   // }
   
    //시간대 조정

}