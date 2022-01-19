using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LoginUIManager : MonoBehaviour
{
    public GameObject ConnectPanel;
    public GameObject ConnectingOptions;
    public GameObject LogInCanvas;
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
        LogInCanvas.SetActive(false);
        ConnectingOptions.SetActive(false);
        ConnectPanel.SetActive(false);
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
                        LogInCanvas.SetActive(true);
                        ConnectingOptions.SetActive(true);
                    }
                    else
                    {
                        count = 0;
                        LogInCanvas.SetActive(false);
                        ConnectingOptions.SetActive(false);
                    }
                }

            }

        }
    }
}
