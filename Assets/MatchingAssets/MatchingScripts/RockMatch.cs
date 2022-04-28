using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMatch : MonoBehaviour
{
    private Vector2 FirstPos;
    private Vector2 FinalPos;

    public float SwipeAngle = 0;

    public int Column;
    public int Row;

    private GameObject OtherRock;

    private BoardMatch board;

    public int TargetX;
    public int TargetY;

    private Vector2 TempPos;

    public bool Matched = false;



    void Start(){
        board = FindObjectOfType<BoardMatch>();

        TargetX = (int)transform.position.x;
        TargetY = (int)transform.position.y;

        Row = TargetY;
        Column = TargetX;
    }

    void Update(){
        TargetX = Column;
        TargetY = Row;

        //Match

        FindMatch();

        if(Matched){
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0f, 0f, 0f, .2f);

        }

        //Swipe

        if(Mathf.Abs(TargetX - transform.position.x) > .1){
            //Move to target

            TempPos = new Vector2(TargetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, TempPos, .4f);

        } else {
            //Directly set pos

            TempPos = new Vector2(TargetX, transform.position.y);
            transform.position = TempPos;
            board.AllRocks[Column, Row] = this.gameObject;
        }

        if(Mathf.Abs(TargetY - transform.position.y) > .1){
            //Move to target

            TempPos = new Vector2(transform.position.x, TargetY);
            transform.position = Vector2.Lerp(transform.position, TempPos, .4f);

        } else {
            //Directly set pos

            TempPos = new Vector2(transform.position.x, TargetY);
            transform.position = TempPos;
            board.AllRocks[Column, Row] = this.gameObject;
        }
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

        MovePieces();

    }

    void MovePieces(){
        if(SwipeAngle > -45 && SwipeAngle <= 45 && Column < board.width){
            //Right
            OtherRock = board.AllRocks[Column + 1, Row];

            OtherRock.GetComponent<RockMatch>().Column -= 1;
            Column += 1;
        } else if(SwipeAngle > 45 && SwipeAngle <= 135 && Row < board.height){
            //Up
            OtherRock = board.AllRocks[Column, Row + 1];

            OtherRock.GetComponent<RockMatch>().Row -= 1;
            Row += 1;
        } else if((SwipeAngle > 135 || SwipeAngle <= -135) && Column > 0){
            //Left
            OtherRock = board.AllRocks[Column - 1, Row];

            OtherRock.GetComponent<RockMatch>().Column += 1;
            Column -= 1;
        } else if(SwipeAngle < -45 && SwipeAngle >= -135 && Row > 0){
            //Down
            OtherRock = board.AllRocks[Column, Row - 1];

            OtherRock.GetComponent<RockMatch>().Row += 1;
            Row -= 1;
        }
    }

    void FindMatch (){
        if(Column > 0 && Column < board.width - 1){
            GameObject LeftRockOne = board.AllRocks[Column - 1, Row];
            GameObject RightRockOne = board.AllRocks[Column + 1, Row];

            if(LeftRockOne.tag == this.gameObject.tag && RightRockOne.tag == this.gameObject.tag){
                LeftRockOne.GetComponent<RockMatch>().Matched = true;
                RightRockOne.GetComponent<RockMatch>().Matched = true;
                Matched = true;
            }

        }

        if(Row > 0 && Row < board.height - 1){
            GameObject UpDotOne = board.AllRocks[Column, Row + 1];
            GameObject DownDotOne = board.AllRocks[Column, Row - 1];

            if(UpDotOne.tag == this.gameObject.tag && DownDotOne.tag == this.gameObject.tag){
                UpDotOne.GetComponent<RockMatch>().Matched = true;
                DownDotOne.GetComponent<RockMatch>().Matched = true;
                Matched = true;
            }

        }
    }

    
}
