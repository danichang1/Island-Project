using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{

    public GameObject pauseScreen;

    void OnMouseDown(){
        Time.timeScale = 1;
        GameManager.instance.music.Play();
        pauseScreen.SetActive(false);
    }
}
