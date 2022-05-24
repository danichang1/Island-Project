using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongEnd : MonoBehaviour
{
    private bool endShow;

    void Start(){
        endShow = false;
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Activator" && endShow == false){
            Debug.Log("END");
            GameManager.instance.endGame();
        }
    }
}
