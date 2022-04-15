using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMatch : MonoBehaviour{

    public int width;
    public int height;
    public GameObject TilePrefab;
    private float[,] allTiles;

    void Start(){

        allTiles = new float[width,height];

        Setup();
        
    }

    private void Setup(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){

                Vector2 TempPos = new Vector2(i, j);

                Instantiate(TilePrefab, TempPos, Quaternion.identity);
            }
        }
        
    }
}
