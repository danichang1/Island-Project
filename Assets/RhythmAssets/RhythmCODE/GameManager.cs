using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public AudioSource music;
    public bool startMusic;
    public BeatScroller theBS;
    public static GameManager instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(!startMusic){
            if(Input.anyKeyDown){
                startMusic = true;
                theBS.hasStarted = true;

                music.Play();
            }
        }
    }

    public void NoteHit(){
        Debug.Log("HIT");
    }

    public void NoteMiss(){
        Debug.Log("MISS");
    }
}
