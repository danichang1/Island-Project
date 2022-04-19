using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    void OnMouseDown(){
        if(Time.timeScale > 0){
            Time.timeScale = 0;
            GameManager.instance.music.Pause();
        } else{
            Time.timeScale = 1;
            GameManager.instance.music.Play();
        }
    }
}
