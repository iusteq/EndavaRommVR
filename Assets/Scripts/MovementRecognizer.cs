using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementRecognizer : MonoBehaviour
{
    //public XRNode controllerNode;
    private XRController controller;
    public Transform movementSource;
    //public GameObject testingObject;
    public InputHelpers.Button inputButton;
    //public RaycastHit hit;
    public Material lineMaterial;

    private bool isMoving = false;
    public float lineWidth = 0.03f;
    public float newThreshold = 0.05f;

    private List<Vector3> positionsList = new List<Vector3>();
    private bool isDrawable=false;
    private LineRenderer currentLine; 


    void Start()
    {
        controller = GetComponent<XRController>();
    }

    void Update()
    {
        //InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controllerNode), inputButton, out bool isPressed);
        InputHelpers.IsPressed(controller.inputDevice, inputButton, out bool isPressed);

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

    public void SetLineMaterial(Material newMat)
    {
        lineMaterial = newMat;
    }

    void StartMovement()
    {
        isMoving = true;
        //positionsList.Clear();
        //positionsList.Add(movementSource.position);

        //if(testingObject)
        //    Instantiate(testingObject, new Vector3(Xpos,Ypos,9.10f), Quaternion.identity);

        GameObject line = new GameObject("Line");
        currentLine = line.AddComponent<LineRenderer>();

        UpdateLine();
    }

    void UpdateLine()
    {
        positionsList.Add(movementSource.position);
        currentLine.positionCount = positionsList.Count;
        currentLine.SetPositions(positionsList.ToArray());

        currentLine.material = lineMaterial;
        currentLine.startWidth = lineWidth;
    }

    void StopMovement()
    {
        isMoving=false;
        positionsList.Clear();
        currentLine = null;
    }

    void UpdateMovement()
    {
        if (!currentLine || positionsList.Count == 0)
            return;

        Vector3 lastPosition = positionsList[positionsList.Count - 1];

        if( Vector3.Distance(movementSource.position, lastPosition) > newThreshold)
        {
            //positionsList.Add(movementSource.position);

            //if (testingObject)
            //    Instantiate(testingObject, movementSource.position, Quaternion.identity);
           
            UpdateLine();
        }
            

    }
}
