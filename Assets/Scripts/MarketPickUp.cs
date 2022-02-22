using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MarketPickUp : MonoBehaviour
{
    public GameObject markerDown;
    public GameObject markerWrite;

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
        markerWrite.SetActive(false);
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

                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out inputValue) && inputValue) 
                {
                    if (count == 0)
                    {
                        count = 1;
                        markerWrite.SetActive(true);
                        markerDown.SetActive(false);
                    }
                    else
                    {
                        count = 0;
                        markerWrite.SetActive(false);
                        markerDown.SetActive(true);
                    }
                }

            }

        }
    }
}
