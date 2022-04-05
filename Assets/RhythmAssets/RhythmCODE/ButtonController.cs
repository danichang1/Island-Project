using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Material myMaterial;
    public Material notPressed;
    public Material pressed;

    public KeyCode keyToPress;

    void Start(){

    }

    void Update()
    {
        
        if(Input.GetKeyDown(keyToPress)){
            GetComponent<Renderer>().material = pressed;
        }

        if (Input.GetKeyUp(keyToPress)){
            GetComponent<Renderer>().material = notPressed;
        }

    }
}
