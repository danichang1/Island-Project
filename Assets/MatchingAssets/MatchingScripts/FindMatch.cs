using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatch : MonoBehaviour
{
  private BoardMatch Board;

  public List<GameObject> CurMatches = new List<GameObject>();

  void Start(){
      Board = FindObjectOfType<BoardMatch>();
  }

  public void FindAllMatch(){
      StartCoroutine(FindAllMatchCo());

  }

  private IEnumerator FindAllMatchCo(){
        yield return new WaitForSeconds(.1f);

      for(int i = 0; i < Board.width; i++){

          for(int j = 0; j < Board.height; j++){

              GameObject CurRock = Board.AllRocks[i, j];

              if(CurRock != null){

                  if(i > 0 && i < Board.width - 1){

                      GameObject LeftRock = Board.AllRocks[i - 1, j];
                      GameObject RightRock = Board.AllRocks[i + 1, j];

                      if(LeftRock != null && RightRock != null){

                          if(LeftRock.tag == CurRock.tag && RightRock.tag == CurRock.tag){

                              if(!CurMatches.Contains(LeftRock)){
                                  CurMatches.Add(LeftRock);
                              }

                              LeftRock.GetComponent<RockMatch>().Matched = true;

                              if(!CurMatches.Contains(RightRock)){
                                  CurMatches.Add(RightRock);
                              }

                              RightRock.GetComponent<RockMatch>().Matched = true;

                              if(!CurMatches.Contains(CurRock)){
                                  CurMatches.Add(CurRock);
                              }

                              CurRock.GetComponent<RockMatch>().Matched = true;
                          }
                      }
                  }

                  if(j > 0 && j < Board.height - 1){

                      GameObject UpRock = Board.AllRocks[i, j + 1];
                      GameObject DownRock = Board.AllRocks[i, j - 1];

                      if(UpRock != null && DownRock != null){
                          
                          if(UpRock.tag == CurRock.tag && DownRock.tag == CurRock.tag){

                              if(!CurMatches.Contains(UpRock)){
                                  CurMatches.Add(UpRock);
                              }

                              UpRock.GetComponent<RockMatch>().Matched = true;

                              if(!CurMatches.Contains(DownRock)){
                                  CurMatches.Add(DownRock);
                              }

                              DownRock.GetComponent<RockMatch>().Matched = true;

                              if(!CurMatches.Contains(CurRock)){
                                  CurMatches.Add(CurRock);
                              }

                              CurRock.GetComponent<RockMatch>().Matched = true;
                          }
                      }
                  }
              }
          }
      }
  }
}
