using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Draw : MonoBehaviour
{
    public InputHelpers.Button drawInput;
    public Transform drawPositionSource;
    public float lineWidth = 0.03f;
    public Material lineMaterial;
    public float distanceThreshold = 0.05f;

    private List<Vector3> currentLinePositions = new List<Vector3>();
    //private XRController controller;
    public XRNode controllerNode;
    private bool isDrawing = false;
    private LineRenderer currentLine;
    private bool isDrawable = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "DrawingBoard")
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

    // Start is called before the first frame update
    void Start()
    {
        //controller = GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if input down
        //InputHelpers.IsPressed(controller.inputDevice, drawInput, out bool isPressed);
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controllerNode), drawInput, out bool isPressed);

        if (!isDrawing && isPressed && isDrawable)
        {
            StartDrawing();
        }
        else if ((isDrawing && !isPressed) || !isDrawable)
        {
            StopDrawing();
        }
        else if (isDrawing && isPressed && isDrawable)
        {
            UpdateDrawing();
        }
    }

    public void SetLineMaterial(Material newMat)
    {
        lineMaterial = newMat;
    }

    void StartDrawing()
    {
        isDrawing = true;
        //create line
        GameObject lineGameObject = new GameObject("Line");
        currentLine = lineGameObject.AddComponent<LineRenderer>();

        UpdateLine();
    }

    void UpdateLine()
    {
        //update line
        //update line position
        currentLinePositions.Add(drawPositionSource.position);
        currentLine.positionCount = currentLinePositions.Count;
        currentLine.SetPositions(currentLinePositions.ToArray());

        //update line visual
        currentLine.material = lineMaterial;
        currentLine.startWidth = lineWidth;
    }

    void StopDrawing()
    {
        isDrawing = false;
        currentLinePositions.Clear();
        currentLine = null;
    }

    void UpdateDrawing()
    {
        //check if we have a line
        if (!currentLine || currentLinePositions.Count == 0)
            return;

        Vector3 lastSetPosition = currentLinePositions[currentLinePositions.Count - 1];
        if (Vector3.Distance(lastSetPosition, drawPositionSource.position) > distanceThreshold)
        {
            UpdateLine();
        }
    }
}
