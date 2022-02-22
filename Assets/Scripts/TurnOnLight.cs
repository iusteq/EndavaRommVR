using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    public GameObject LightCanvas;
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    bool isOn=false;

    // Start is called before the first frame update
    void Start()
    {
        LightCanvas.SetActive(true);
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLightButton()
    {
        if(!isOn)
        {
            light1.SetActive(true);
            light2.SetActive(true);
            light3.SetActive(true);
            isOn = true;
        }
        else if(isOn)
        {
            light1.SetActive(false);
            light2.SetActive(false);
            light3.SetActive(false);
            isOn = false;
        }
    }


}
