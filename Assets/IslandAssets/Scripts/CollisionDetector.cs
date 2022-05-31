using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    public Camera playerCam;
    public Camera beastCam;

    private float beastTimer = 0;

    static public bool inBeastScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inBeastScene) {
            beastTimer += Time.deltaTime;
            if (beastTimer >= 2) {
                playerCam.GetComponent<Camera>().enabled = true;
                beastCam.GetComponent<Camera>().enabled = false;

                inBeastScene = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if(other.gameObject.CompareTag("BeastInteract"))
        {
            playerCam.GetComponent<Camera>().enabled = false;
            beastCam.GetComponent<Camera>().enabled = true;

            inBeastScene = true;




        }
    }
}
