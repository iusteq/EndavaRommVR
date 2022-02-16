using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SitUp : MonoBehaviour
{
    public GameObject RigSitDown;
    public GameObject RigNormal;
    //public GameObject chair;
    public GameObject SitUpQuestion;
    public GameObject SitUpCanvas;

    List<InputDevice> devices;
    public XRNode controllerNode;

    int count = 0;

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
        RigNormal.SetActive(false);
        SitUpCanvas.SetActive(false);
        SitUpQuestion.SetActive(false);
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
                        SitUpCanvas.SetActive(true);
                        SitUpQuestion.SetActive(true);
                    }
                    else
                    {
                        count = 0;
                        SitUpCanvas.SetActive(false);
                        SitUpQuestion.SetActive(false);
                    }
                }

            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            count = 1;
            SitUpCanvas.SetActive(true);
            SitUpQuestion.SetActive(true);
        }
        else
        {
            count = 0;
            SitUpCanvas.SetActive(false);
            SitUpQuestion.SetActive(false);
        }
    }

    public void OnYesButton()
    {
        RigNormal.SetActive(true);
        RigSitDown.SetActive(false);
    }

    public void OnNoButton()
    {
        SitUpCanvas.SetActive(false);
        SitUpQuestion.SetActive(false);
    }
}
