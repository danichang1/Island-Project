using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject respawnPoint;

    Vector3 checkpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        checkpoint = respawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -10) {
            this.transform.position = checkpoint;
        }
    }
}
