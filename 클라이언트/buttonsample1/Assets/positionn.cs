using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class positionn : MonoBehaviour {
    private bool gyroEnabled;
    //Gyro
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rot;//회전표현, 
    public bool a = true;
    public bool goyes = false;

    //Cam
    private WebCamTexture cam;
    public RawImage background;
    public AspectRatioFitter fit;

    private bool arReady = false;
	// Use this for initialization
	void Start () {


       // gyroEnabled = EnableGyro();

        if(!SystemInfo.supportsGyroscope)
        {
            return;
        }

        //backcamera
        for(int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            if(!WebCamTexture.devices[i].isFrontFacing)
            {
                cam = new WebCamTexture(WebCamTexture.devices[i].name, Screen.width, Screen.height);
                break;
            }
        }
        if(cam == null)
        {
            return;
        }

        cameraContainer = new GameObject("Camera Container");
        gameObject.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);//부모설정

        gyro = Input.gyro;
        gyro.enabled = true;
        cameraContainer.transform.rotation = Quaternion.Euler(90f, 0, 0);
        rot = new Quaternion(0, 0, 1, 0);
        cam.Play();
        background.texture = cam;

        arReady = true;

        Input.location.Start();
        Input.compass.enabled = true;
        
    }
    /*
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 90f);//부모 오일러회전
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }

        return false;
    }
    */

    // Update is called once per frame
    void Update () {
        print(Input.compass.rawVector.ToString());
        if(arReady)
        {
            float ratio = (float)cam.width / (float)cam.height;
            fit.aspectRatio = ratio;

            float scaleY = cam.videoVerticallyMirrored ? -1.0f : 1.0f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -cam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

            transform.localRotation = gyro.attitude * rot;

        }

        if (goyes == true) this.transform.Translate(Vector3.forward * 3f * Time.deltaTime);

        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;//부모 회전에 상대적인 회전, 오일러각으로 회전
        }

    }
    public void redMove()
    {
        if (a == true)
        {
          gameObject.transform.position = new Vector3(160, 0, 60);
            a = false;
        }
        else
        {
           gameObject.transform.position = new Vector3(60, 1, 20);
            a = true;
        }
    }
    public void gogo()
    {
        if (goyes == true) goyes = false;
        else goyes = true;
    }
}
