using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SitDown : MonoBehaviour
{
    public GameObject RigSitDown;
    public GameObject RigNormal;
    //public GameObject chair;
    public GameObject SitDownQuestion;
    public GameObject SitDownCanvas;

    List<InputDevice> devices;
    public XRNode controllerNode;

    int count = 0;
    bool isDown = false;

    private void Awake()
    {
        devices = new List<InputDevice>();
    }

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
    }

    // Start is called before the first frame update
    void Start()
    {
        //RigNormal = GetComponent<GameObject>();
        RigSitDown.SetActive(false);
        SitDownCanvas.SetActive(false);
        SitDownQuestion.SetActive(false);
        GetDevice();
    }

    // Update is called once per frame
    void Update()
    {
        GetDevice();
        foreach (var device in devices)
        {
            Debug.Log(device.name + " " + device.characteristics);

            if (device.isValid)
            {
                bool inputValue;

                if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out inputValue) && inputValue) //butonul b
                {
                    if (count == 0)
                    {
                        count = 1;
                        SitDownCanvas.SetActive(true);
                        SitDownQuestion.SetActive(true);
                    }
                    else
                    {
                        count = 0;
                        SitDownCanvas.SetActive(false);
                        SitDownQuestion.SetActive(false);
                    }
                    if(isDown)
                    {
                        RigNormal.SetActive(true);
                        RigSitDown.SetActive(false);
                        SitDownCanvas.SetActive(false);
                        SitDownQuestion.SetActive(false);
                    }
                }

            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            count = 1;
            SitDownCanvas.SetActive(true);
            SitDownQuestion.SetActive(true);
        }
        else
        {
            count = 0;
            SitDownCanvas.SetActive(false);
            SitDownQuestion.SetActive(false);
        }
    }

    public void OnYesButton()
    {
        RigNormal.SetActive(false);
        RigSitDown.SetActive(true);
        isDown = true;
    }

    public void OnNoButton()
    {
        SitDownCanvas.SetActive(false);
        SitDownQuestion.SetActive(false);
        isDown = false;
    }
}
