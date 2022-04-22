using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMatch : MonoBehaviour{

    public int width;
    public int height;

    public GameObject TilePrefab;

    private float[,] allTiles;

    public GameObject[,] AllRocks;

    public GameObject[] Rocks;

    void Start(){

        allTiles = new float[width,height];

        AllRocks = new GameObject[width,height];

        Setup();
        
    }

    private void Setup(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){

                Vector2 TempPos = new Vector2(i, j);

                GameObject BGTile = Instantiate(TilePrefab, TempPos, Quaternion.identity) as GameObject;

                BGTile.transform.parent = this.transform;
                BGTile.name = "(" + i + "," + j + ")";

                int RockUse = Random.Range(0, Rocks.Length);

                GameObject Rock = Instantiate(Rocks[RockUse], TempPos, Quaternion.identity);

                Rock.transform.parent = this.transform;
                Rock.name = "(" + i + "," + j + ")";
                AllRocks[i,j] = Rock;
            }
        }
        
    }
}
