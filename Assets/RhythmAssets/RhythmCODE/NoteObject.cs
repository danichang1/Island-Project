using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect;

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
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                var distance = Mathf.Abs(1 - transform.position.y);

                if (distance >= 0.5f){
                    GameManager.instance.BadHit();
                } else if (distance >= 0.3f){
                    GameManager.instance.GoodHit();
                } else if (distance >= 0.12f){
                    GameManager.instance.GreatHit();
                } else{
                    GameManager.instance.PerfectHit();
                }

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
