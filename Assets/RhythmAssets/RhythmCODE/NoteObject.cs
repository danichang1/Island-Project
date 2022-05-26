using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject perfectEffect, greatEffect, goodEffect, badEffect, missEffect;

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
                var distance = Mathf.Abs(1 - transform.position.y);

                var particlePos = new Vector3(transform.position.x, transform.position.y, -5);
                if (distance >= 0.8f){
                    GameManager.instance.BadHit();
                    Instantiate(badEffect, particlePos, badEffect.transform.rotation);
                } else if (distance >= 0.5f){
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, particlePos, goodEffect.transform.rotation);
                } else if (distance >= 0.15f){
                    GameManager.instance.GreatHit();
                    Instantiate(greatEffect, particlePos, greatEffect.transform.rotation);
                } else{
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, particlePos, perfectEffect.transform.rotation);
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
            Vector3 missPosition = new Vector3(transform.position.x, 1f, transform.position.z);
            Instantiate(missEffect, missPosition, missEffect.transform.rotation);
        }
    }
}
