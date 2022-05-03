using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject pauseScreen;

    void Start(){
        pauseScreen.SetActive(false);
    }

    void OnMouseDown(){
        if(Time.timeScale > 0){
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            GameManager.instance.music.Pause();
        } else{
            Time.timeScale = 1;
            GameManager.instance.music.Play();
        }
    }
}
