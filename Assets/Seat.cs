using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Seat : MonoBehaviour
{
    List<InputDevice> devices;
    public XRNode controllerNode;
    public CharacterController characterController;

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
        characterController = GetComponent<CharacterController>();
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
                        characterController.height = 1.7f;
                        characterController.center = new Vector3(0, 0, 3.6f);
                    }
                    else
                    {
                        count = 0;
                        characterController.height = 2.5f;
                        characterController.center = new Vector3(-0.3f, 2f, 3f);
                    }
                }

            }

        }
    }
}
