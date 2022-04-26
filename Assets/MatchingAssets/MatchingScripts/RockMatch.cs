using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMatch : MonoBehaviour
{
    private Vector2 FirstPos;
    private Vector2 FinalPos;

    public float SwipeAngle = 0;

    public int column;
    public int row;

    public int TargetX;
    public int TargetY;

    private GameObject OtherRock;

    private BoardMatch Board;



    void Start(){
        Board = FindObjectOfType<BoardMatch>();

        TargetX = (int)transform.position.x;
        TargetY = (int)transform.position.y;

    }

    void Update(){
        TargetX = column;
        TargetY = row;
    }

    void OnMouseDown(){
        FirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(FirstPos);
    }

    void OnMouseUp(){
        FinalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CalcAngle();
    }

    void CalcAngle(){
        SwipeAngle = Mathf.Atan2(FinalPos.y - FirstPos.y, FinalPos.x - FirstPos.x) * 180/Mathf.PI;
        Debug.Log(SwipeAngle);
    }

    void MoveRocks(){
        if(SwipeAngle > -45 && SwipeAngle <= 45 && column < Board.width){
            //Right swipe
            OtherRock = Board.AllRocks[column + 1, row];
            OtherRock.GetComponent<Rock>().column -= 1;
            column += 1;

        } else if(SwipeAngle > 45 && SwipeAngle <= 135 && row < Board.height){
            //Up swipe
            OtherRock = Board.AllRocks[column, row + 1];
            OtherRock.GetComponent<Rock>().row -= 1;
            row += 1;

        } else if((SwipeAngle > -135 || SwipeAngle <= -135) && column > 0){
            //Left swipe
            OtherRock = Board.AllRocks[column - 1, row];
            OtherRock.GetComponent<Rock>().column += 1;
            column -= 1;

        } else if(SwipeAngle < -45 && SwipeAngle >= -135 && row > 0){
            //Down swipe
            OtherRock = Board.AllRocks[column, row - 1];
            OtherRock.GetComponent<Rock>().row += 1;
            row -= 1;

        }
    }
}
