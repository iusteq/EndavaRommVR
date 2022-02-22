using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject ColorsCanvas;
    public MeshRenderer varf1;
    public MeshRenderer varf2;
    public MeshRenderer capac;
    public GameObject pen;

    public Material red;
    public Material blue;
    public Material black;
    public Material green;

    //public Material newMaterial;

    //private void OnTriggerEnter(Collider other)
    //{
    //    //change brush tip
    //    other.GetComponent<Renderer>().material = newMaterial;
    //    //change line material
    //    other.SendMessageUpwards("SetLineMaterial", newMaterial, SendMessageOptions.DontRequireReceiver);
    //}

    // Start is called before the first frame update
    void Start()
    {
        ColorsCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRED()
    {
        pen.GetComponent<Renderer>().material = red;
        pen.SendMessageUpwards("SetLineMaterial", red, SendMessageOptions.DontRequireReceiver);
        //varf1.material = red;
        //varf2.material = red;
        //capac.material = red;
        varf1.GetComponent<MeshRenderer>().material = red;
        varf2.GetComponent<MeshRenderer>().material = red;
        capac.GetComponent<MeshRenderer>().material = red;
    }

    public void OnBlue()
    {
        pen.GetComponent<Renderer>().material = blue;
        pen.SendMessageUpwards("SetLineMaterial", blue, SendMessageOptions.DontRequireReceiver);
        varf1.material = blue;
        varf2.material = blue;
        capac.material = blue;
    }

    public void OnBlack()
    {
        pen.GetComponent<Renderer>().material = black;
        pen.SendMessageUpwards("SetLineMaterial", black, SendMessageOptions.DontRequireReceiver);
        varf1.material = black;
        varf2.material = black;
        capac.material = black;
    }

    public void OnGreen()
    {
        pen.GetComponent<Renderer>().material = green;
        pen.SendMessageUpwards("SetLineMaterial", green, SendMessageOptions.DontRequireReceiver);
        varf1.material = green;
        varf2.material = green;
        capac.material = green;
    }
}


