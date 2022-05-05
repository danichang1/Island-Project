using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject perfectEffect, greatEffect, goodEffect, badEffect, missEffect;

    public GameObject tail;

    public bool beingHeld;

    private Vector3 stay;
    private bool staySet;

    public float tailLength;


    void Start()
    {
        tailLength = 2;
        tail.transform.localScale = new Vector3(tail.transform.localScale.x, tailLength, tail.transform.localScale.z);
        beingHeld = false;
        staySet = false;
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
        if (beingHeld == true){
            transform.position = stay;
            var newY = tail.transform.localScale.y - GameManager.instance.beatTempo/30 * Time.deltaTime;
            newY = Mathf.Max(0f, newY);
            tail.transform.localScale = new Vector3(tail.transform.localScale.x, newY, tail.transform.localScale.z);
        }
        if(Input.GetKeyDown(keyToPress)){
            if(canBePressed){
                beingHeld = true;
                if (staySet == false){
                    stay = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    staySet = true;
                }
                
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

        if(Input.GetKeyUp(keyToPress)){
            gameObject.SetActive(false);
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