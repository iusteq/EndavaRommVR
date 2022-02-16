using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Seat : MonoBehaviour
{
    List<InputDevice> devices;
    public XRNode controllerNode;
    public CharacterController characterController;
    public GameObject rig;
    
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
        rig = GetComponent<GameObject>();
        GetDevice();
    }

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
                    characterController.height = 0.5f;
                    characterController.transform.position = new Vector3(0, 0, 3.6f);
                }

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
            transform.position = new Vector3(0, 0, 3.6f);
    }
}
