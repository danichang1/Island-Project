using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;



    void Start()
    {
        if (transform.position.x == -7.8f){
            keyToPress = KeyCode.D;
        } else if (transform.position.x == -5.8f){
            keyToPress = KeyCode.F;
        } else if (transform.position.x == -3.8f){
            keyToPress = KeyCode.J;
        } else {
            keyToPress = KeyCode.K;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(keyToPress)){
            if(canBePressed){
                gameObject.SetActive(false);
                GameManager.instance.NoteHit();

            }
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Activator"){
            canBePressed = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.tag == "Activator"){
            canBePressed = false;
            GameManager.instance.NoteMiss();
        }
    }
}
