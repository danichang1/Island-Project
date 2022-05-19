using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongEnd : MonoBehaviour
{

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Activator"){
            Debug.Log("END");
        }
    }
}
