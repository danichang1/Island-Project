using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject perfectEffect, greatEffect, goodEffect, badEffect, missEffect;

    public GameObject tail;

    public GameObject end;

    public bool beingHeld;

    private Vector3 stay;
    private bool staySet;

    public float tailLength;

    public GameObject holdEffect;
    public GameObject holdDestroy;


    void Start()
    {
        var endPos = transform.position.y + tailLength;
        end.transform.position = new Vector3(end.transform.position.x, endPos, end.transform.position.z);
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
                    gameObject.SetActive(false);
                    GameManager.instance.NoteMiss();
                    var badMiss = new Vector3(transform.position.x, transform.position.y + 0.5f, -5);
                    Instantiate(missEffect, badMiss, missEffect.transform.rotation);
                    end.SetActive(false);
                } else if (distance >= 0.5f){
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, particlePos, goodEffect.transform.rotation);
                    holdDestroy = Instantiate(holdEffect, particlePos, holdEffect.transform.rotation);
                } else if (distance >= 0.15f){
                    GameManager.instance.GreatHit();
                    Instantiate(greatEffect, particlePos, greatEffect.transform.rotation);
                    holdDestroy = Instantiate(holdEffect, particlePos, holdEffect.transform.rotation);
                } else{
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, particlePos, perfectEffect.transform.rotation);
                    holdDestroy = Instantiate(holdEffect, particlePos, holdEffect.transform.rotation);
                }

            }
        }

        var howfar = Mathf.Abs(end.transform.position.y - 1);

        if (howfar <= 0.2f){
            end.GetComponent<MeshRenderer>().enabled = false;
        }

        if (end.transform.position.y - transform.position.y <= -2.30){
            var releaseMiss = new Vector3(transform.position.x, transform.position.y, -5);
            GameManager.instance.NoteMiss();
            Instantiate(missEffect, releaseMiss, missEffect.transform.rotation);
            if (holdDestroy != null){
                Destroy(holdDestroy);
            }
            gameObject.SetActive(false);
            end.SetActive(false);
        }

        if(Input.GetKeyUp(keyToPress) && beingHeld == true){
            if (canBePressed){
                if (holdDestroy != null){
                    Destroy(holdDestroy);
                }
                var releaseParticle = new Vector3(transform.position.x, transform.position.y, -5);
                if (howfar >= 1f){
                    GameManager.instance.NoteMiss();
                    Instantiate(missEffect, releaseParticle, missEffect.transform.rotation);
                } else if (howfar >= 0.8f){
                    GameManager.instance.BadHit();
                    Instantiate(badEffect, releaseParticle, badEffect.transform.rotation);
                } else if (howfar >= 0.5f){
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, releaseParticle, goodEffect.transform.rotation);
                } else if (howfar >= 0.15f){
                    GameManager.instance.GreatHit();
                    Instantiate(greatEffect, releaseParticle, greatEffect.transform.rotation);
                } else {
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, releaseParticle, perfectEffect.transform.rotation);
                }
                gameObject.SetActive(false);
                end.SetActive(false);
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
            gameObject.SetActive(false);
            GameManager.instance.NoteMiss();
            var doubleMiss = new Vector3(transform.position.x, 1.5f, transform.position.z);
            Instantiate(missEffect, doubleMiss, missEffect.transform.rotation);
            end.SetActive(false);
        }
    }
}
