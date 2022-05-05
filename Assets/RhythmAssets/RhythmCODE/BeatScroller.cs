using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    private float beatTempo;
    public bool hasStarted;
    
    void Start()
    {
        beatTempo = GameManager.instance.beatTempo/30f;
    }

    void Update()
    {
        if (!hasStarted){
            
        } else{
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
