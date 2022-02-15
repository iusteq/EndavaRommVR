using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MovementRecognizer : MonoBehaviour
{
    List<InputDevice> devices;
    public XRNode controllerNode;

    private bool isMoving = false;
    public float inputThreshold = 0.1f;
    public Transform movementSource;
    public GameObject testingObject;

    private List<Vector3> positionsList = new List<Vector3>();

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

                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out inputValue) && inputValue) //butonul b
                {
                    if(!isMoving)
                    {
                        StartMovement();
                    }
                    else if (isMoving)
                    {
                        UpdateMovement();
                    }
                }
                else
                {
                    if(isMoving)
                    {
                        StopMovement();
                    }
                }

            }

        }
    }

    void StartMovement()
    {
        isMoving = true;
        positionsList.Clear();
        positionsList.Add(movementSource.position);

        if(testingObject)
            Instantiate(testingObject, movementSource.position, Quaternion.identity);
    }

    void StopMovement()
    {
        isMoving=false;
    }

    void UpdateMovement()
    {
        Vector3 lastPosition = positionsList[positionsList.Count - 1];

        if( Vector3.Distance(movementSource.position, lastPosition) > inputThreshold)
        {
            positionsList.Add(movementSource.position);

            if (testingObject)
                Instantiate(testingObject, movementSource.position, Quaternion.identity);
        }
            

    }
}
