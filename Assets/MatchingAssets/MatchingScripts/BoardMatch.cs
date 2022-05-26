using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState{
    wait,
    move
}

public class BoardMatch : MonoBehaviour{

    public TextMeshProUGUI ScoreTxt;
    
    public int TotalMatch;

    public GameState CurSta = GameState.move;

    public int width;
    public int height;

    public GameObject TilePrefab;

    private float[,] allTiles;

    public GameObject[,] AllRocks;

    public GameObject[] Rocks;

    public int Offset;

    private FindMatch FindMatches;

    void Update(){
        ScoreTxt.text = "You have mined " + TotalMatch + "/100 crystals";

        if(TotalMatch >= 100){
            Win();
        }
    }


    void Start(){

        FindMatches = FindObjectOfType<FindMatch>();

        allTiles = new float[width,height];

        AllRocks = new GameObject[width,height];

        Setup();
        
    }

    private void Win(){

    }

    private void Setup(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){

                Vector2 TempPos = new Vector2(i, j);

                GameObject BGTile = Instantiate(TilePrefab, TempPos, Quaternion.identity) as GameObject;

                BGTile.transform.parent = this.transform;
                BGTile.name = "(" + i + "," + j + ")";

                int RockUse = Random.Range(0, Rocks.Length);

                int MaxIt = 0;

                while(MatchesAt(i, j, Rocks[RockUse]) && MaxIt < 100){
                    RockUse = Random.Range(0, Rocks.Length);

                    MaxIt++;
                }

                MaxIt = 0;

                GameObject Rock = Instantiate(Rocks[RockUse], TempPos, Quaternion.identity);

                Rock.GetComponent<RockMatch>().Row = j;
                Rock.GetComponent<RockMatch>().Column = i;

                Rock.transform.parent = this.transform;
                Rock.name = "(" + i + "," + j + ")";
                AllRocks[i,j] = Rock;
            }
        }
        
    }

    private bool MatchesAt(int Column, int Row, GameObject Piece){
        if(Column > 1 && Row > 1){
            if(AllRocks[Column - 1, Row].tag == Piece.tag && AllRocks[Column - 2, Row].tag == Piece.tag){
                return true;
            }

            if(AllRocks[Column, Row - 1].tag == Piece.tag && AllRocks[Column, Row - 2].tag == Piece.tag){
                return true;
            }
        } else if( Column <= 1 || Row <= 1){
                if(Row > 1){
                    if(AllRocks[Column, Row - 1].tag == Piece.tag && AllRocks[Column, Row - 2].tag == Piece.tag){
                        return true;
                    }
                }

                if(Column > 1){
                    if(AllRocks[Column - 1, Row].tag == Piece.tag && AllRocks[Column - 2, Row].tag == Piece.tag){
                        return true;
                    }
                }
        }
        return false;
    }

    private void DestroyMatchAt(int Column, int Row){
        if(AllRocks[Column, Row].GetComponent<RockMatch>().Matched){
            FindMatches.CurMatches.Remove(AllRocks[Column, Row]);
            Destroy(AllRocks[Column, Row]);
            AllRocks[Column, Row] = null;
        }
    }

    public void DestroyMatch(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(AllRocks[i,j] != null){
                    DestroyMatchAt(i,j);
                }
            }
        }

        StartCoroutine(DecreaseRowCO());
    }

    private IEnumerator DecreaseRowCO(){

        int NullCount = 0;

        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                if(AllRocks[i, j] == null){
                    NullCount++;
                }else if (NullCount > 0){
                    AllRocks[i, j].GetComponent<RockMatch>().Row -= NullCount;
                    AllRocks[i, j] = null;
                }
            }

            NullCount = 0;
        }

        yield return new WaitForSeconds(.4f);

        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard(){

        for(int i = 0; i < width; i++){
            for(int j = 0; j < height ; j++){
                if(AllRocks[i, j] == null){
                    Vector2 TempPos = new Vector2(i, j + Offset);
                    int RockUse = Random.Range(0, Rocks.Length);
                    GameObject Piece = Instantiate(Rocks[RockUse], TempPos, Quaternion.identity);
                    AllRocks[i,j] = Piece;

                    Piece.GetComponent<RockMatch>().Row = j;
                    Piece.GetComponent<RockMatch>().Column = i;
                }
            }
        }
    }

bool MatchesOnBoard(){

            for(int i = 0; i < width; i++){
                for(int j = 0; j < height; j++){
                    if(AllRocks[i, j] != null){
                        if(AllRocks[i, j].GetComponent<RockMatch>().Matched){
                            return true;
                        }
                    }
                }
        }
            return false;
        }


    private IEnumerator FillBoardCo(){
        RefillBoard();

        yield return new WaitForSeconds(.2f);

        while(MatchesOnBoard()){
            yield return new WaitForSeconds(.2f);
            DestroyMatch();
        }

        yield return new WaitForSeconds(.2f);

        CurSta = GameState.move;
    }
}
