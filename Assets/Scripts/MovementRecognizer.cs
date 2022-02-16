using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementRecognizer : MonoBehaviour
{
    public XRNode controllerNode;
    public Transform movementSource;
    public GameObject testingObject;
    public InputHelpers.Button inputButton;
    public RaycastHit hit;

    private bool isMoving = false;
    public float inputThreshold = 0.01f;
    public float newThreshold = 0.05f;

    private List<Vector3> positionsList = new List<Vector3>();
    private bool isDrawable=false;

    void Start()
    {
       
    }

    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controllerNode), inputButton, out bool isPressed, inputThreshold);

        if (!isMoving && isPressed && isDrawable)
        {
            StartMovement();
        }

        else if ((isMoving && !isPressed) || !isDrawable)
        {
            StopMovement();
        }

        else if (isMoving && isPressed && isDrawable)
        {
            UpdateMovement();
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name== "DrawingBoard")
        {
            isDrawable = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "DrawingBoard")
        {
            isDrawable = false;
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

        if( Vector3.Distance(movementSource.position, lastPosition) > newThreshold)
        {
            positionsList.Add(movementSource.position);

            if (testingObject)
                Instantiate(testingObject, movementSource.position, Quaternion.identity);
        }
            

    }
}
