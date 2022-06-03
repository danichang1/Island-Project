using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class CollisionDetector : MonoBehaviour
{

    public Camera playerCam;
    public Camera beastCam;

    public GameObject playerRender;

    public TextMeshProUGUI feedFish;
    public TextMeshProUGUI goodFish;

    private float beastTimer = 0;

    static public bool inBeastScene = false;

    Vector3 savePosition;


    // Start is called before the first frame update
    void Start()
    {
        feedFish.enabled = false;
        goodFish.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inBeastScene) {
            beastTimer += Time.deltaTime;
            if (beastTimer >= 3) {
                this.transform.position = savePosition;
                playerRender.GetComponent<MeshRenderer>().enabled = true;
                playerCam.GetComponent<Camera>().enabled = true;
                beastCam.GetComponent<Camera>().enabled = false;
                beastTimer = 0;
                inBeastScene = false;
                feedFish.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if(other.gameObject.CompareTag("BeastInteract")){
            savePosition = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);

            playerRender.GetComponent<MeshRenderer>().enabled = false;

            Debug.Log("Beast Collision");
            playerCam.GetComponent<Camera>().enabled = false;
            beastCam.GetComponent<Camera>().enabled = true;
            if (GemChecks.hasFish) {
                goodFish.enabled = true;
                GemChecks.fishGem = true;
            } else {
                feedFish.enabled = true;
            }
            
            inBeastScene = true;




        } else if (other.gameObject.CompareTag("ClimbingInteract")) {
            SceneManager.LoadScene("Platformer", LoadSceneMode.Single);
        } else if (other.gameObject.CompareTag("FishingInteract"))
            SceneManager.LoadScene("Rhythm", LoadSceneMode.Additive);
    }
}
