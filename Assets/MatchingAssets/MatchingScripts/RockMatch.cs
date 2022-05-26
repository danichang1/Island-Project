using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//comment

public class RockMatch : MonoBehaviour
{

    [Header("Board Variables")]
    private Vector2 FirstPos;
    private Vector2 FinalPos;

    public float SwipeAngle = 0;

    public int Column;
    public int Row;

    private GameObject OtherRock;

    private FindMatch FindMatches;

    private BoardMatch board;

    public int TargetX;
    public int TargetY;

    private Vector2 TempPos;

    public bool Matched = false;

    public bool Counted = false;

    public int PreviousColumn;
    public int PreviousRow;

    public float SwipeResist = 1f;



    void Start(){
        board = FindObjectOfType<BoardMatch>();

        FindMatches = FindObjectOfType<FindMatch>();

        //TargetX = (int)transform.position.x;
        //TargetY = (int)transform.position.y;

        //Row = TargetY;
        //Column = TargetX;

        //PreviousColumn = Column;
        //PreviousRow = Row;
    }

    void Update(){

        if(Matched && !Counted){
            board.TotalMatch++;

            Counted = true;
        }
        TargetX = Column;
        TargetY = Row;

        //Match

        //FindMatch();

        if(Matched){
            //SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            //mySprite.color = new Color(0f, 0f, 0f, .2f);

        }

        //Swipe

        if(Mathf.Abs(TargetX - transform.position.x) > .1){
            //Move to target

            TempPos = new Vector2(TargetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, TempPos, .6f);

            if(board.AllRocks[Column, Row] != this.gameObject){
                board.AllRocks[Column, Row] = this.gameObject;
            }

            FindMatches.FindAllMatch();

        } else {
            //Directly set pos

            TempPos = new Vector2(TargetX, transform.position.y);
            transform.position = TempPos;

        }

        if(Mathf.Abs(TargetY - transform.position.y) > .1){
            //Move to target

            TempPos = new Vector2(transform.position.x, TargetY);
            transform.position = Vector2.Lerp(transform.position, TempPos, .6f);

            if(board.AllRocks[Column, Row] != this.gameObject){
                board.AllRocks[Column, Row] = this.gameObject;
            }

            FindMatches.FindAllMatch();

        } else {
            //Directly set pos

            TempPos = new Vector2(transform.position.x, TargetY);
            transform.position = TempPos;
        }
    }

    public IEnumerator CheckMoveCo(){
        yield return new WaitForSeconds(.25f);
        if(OtherRock != null){
            if(!Matched && !OtherRock.GetComponent<RockMatch>().Matched){
                OtherRock.GetComponent<RockMatch>().Row = Row;
                OtherRock.GetComponent<RockMatch>().Column = Column;

                Row = PreviousRow;
                Column = PreviousColumn;

                yield return new WaitForSeconds(.25f);

                board.CurSta = GameState.move;

            }else{

            board.DestroyMatch();
        }
            OtherRock = null;
        }
    }



    void OnMouseDown(){

        if(board.CurSta == GameState.move){
            FirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
    }

    void OnMouseUp(){
        FinalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CalcAngle();
    }

    void CalcAngle(){

        if(Mathf.Abs(FinalPos.y - FirstPos.y) > SwipeResist || Mathf.Abs(FinalPos.x - FirstPos.x) > SwipeResist){
             SwipeAngle = Mathf.Atan2(FinalPos.y - FirstPos.y, FinalPos.x - FirstPos.x) * 180/Mathf.PI;

            MovePieces();

            board.CurSta = GameState.wait;
        }else {
            board.CurSta = GameState.move;
        }


    }

    void MovePieces(){
        if(SwipeAngle > -45 && SwipeAngle <= 45 && Column < board.width - 1){
            //Right
            OtherRock = board.AllRocks[Column + 1, Row];

            PreviousColumn = Column;
            PreviousRow = Row;

            OtherRock.GetComponent<RockMatch>().Column -= 1;
            Column += 1;
        } else if(SwipeAngle > 45 && SwipeAngle <= 135 && Row < board.height - 1){
            //Up
            OtherRock = board.AllRocks[Column, Row + 1];

            PreviousColumn = Column;
            PreviousRow = Row;

            OtherRock.GetComponent<RockMatch>().Row -= 1;
            Row += 1;
        } else if((SwipeAngle > 135 || SwipeAngle <= -135) && Column > 0){
            //Left
            OtherRock = board.AllRocks[Column - 1, Row];

            PreviousColumn = Column;
            PreviousRow = Row;

            OtherRock.GetComponent<RockMatch>().Column += 1;
            Column -= 1;
        } else if(SwipeAngle < -45 && SwipeAngle >= -135 && Row > 0){
            //Down
            OtherRock = board.AllRocks[Column, Row - 1];

            PreviousColumn = Column;
            PreviousRow = Row;

            OtherRock.GetComponent<RockMatch>().Row += 1;
            Row -= 1;
        }

        StartCoroutine(CheckMoveCo());
    }

    void FindMatch (){
        if(Column > 0 && Column < board.width - 1){
            GameObject LeftRockOne = board.AllRocks[Column - 1, Row];
            GameObject RightRockOne = board.AllRocks[Column + 1, Row];

            if(LeftRockOne != null && RightRockOne != null){

                 if(LeftRockOne.tag == this.gameObject.tag && RightRockOne.tag == this.gameObject.tag){
                      LeftRockOne.GetComponent<RockMatch>().Matched = true;
                     RightRockOne.GetComponent<RockMatch>().Matched = true;
                     Matched = true;
                }
            }

        }

        if(Row > 0 && Row < board.height - 1){
            GameObject UpRockOne = board.AllRocks[Column, Row + 1];
            GameObject DownRockOne = board.AllRocks[Column, Row - 1];

            if(UpRockOne != null && DownRockOne != null){

                if(UpRockOne.tag == this.gameObject.tag && DownRockOne.tag == this.gameObject.tag){
                    UpRockOne.GetComponent<RockMatch>().Matched = true;
                    DownRockOne.GetComponent<RockMatch>().Matched = true;
                    Matched = true;
                }
            }

        }
    }

    
}
