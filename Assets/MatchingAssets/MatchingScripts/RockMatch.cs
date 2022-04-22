using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMatch : MonoBehaviour
{
    private Vector2 FirstPos;
    private Vector2 FinalPos;

    public float SwipeAngle = 0;

    void Update(){
        CalcAngle();
    }

    void OnMouseDown(){
        FirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp(){
        FinalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void CalcAngle(){
        SwipeAngle = Mathf.Atan2(FinalPos.y - FirstPos.y, FinalPos.x - FirstPos.x) * 180/Mathf.PI;
        Debug.Log(SwipeAngle);
    }
}
