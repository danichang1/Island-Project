using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.localScale = new Vector3(GameManager.currentScore/100, 1, 1);
    }
}
