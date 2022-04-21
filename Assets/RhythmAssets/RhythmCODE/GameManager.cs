using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public AudioSource music;
    public bool startMusic;
    public BeatScroller theBS;
    public static GameManager instance;

    public int currentCombo;
    public static float currentScore;
    public int scorePerfect;
    public int scoreGreat;
    public int scoreGood;
    public int scoreBad;
    public int scoreMiss;

    public TextMeshProUGUI comboText;

    public TextAsset beatmapFile;
    public GameObject note;
    public GameObject parent;

    void Start()
    {
        instance = this;

        //makes beatmap
        string text = beatmapFile.text;
        var lines = text.Split('\n');
        for(int i = 0; i < lines.Length; i++){
            var currentLine = lines[lines.Length - (i + 1)];
            for(int u = 0; u < 4; u++){
                var why = "1";
                if (currentLine[u] == why[0]){
                    float xValue = 0f;
                    if (u == 0){
                        xValue = -7.8f;
                    } else if (u == 1){
                        xValue = -5.8f;
                    } else if (u == 2){
                        xValue = -3.8f;
                    } else{
                        xValue = -1.8f;
                    }
                    var currentNote = Instantiate(note, new Vector3(xValue, i + 9, -0.1f), note.transform.rotation);
                    currentNote.transform.parent = parent.transform;
                }
            }
        }
    }

    void Update()
    {
        if(!startMusic){
            if(Input.anyKeyDown){
                startMusic = true;
                theBS.hasStarted = true;

                music.Play();
            }
        }
    }

    public void ScoreSet(){
        currentScore = Mathf.Max(0f, currentScore);
        currentScore = Mathf.Min(currentScore, 700);
        comboText.text = "" + currentCombo;

    }

    public void PerfectHit(){
        Debug.Log("P");
        currentScore += scorePerfect;
        currentCombo++;
        ScoreSet();
    }

    public void GreatHit(){
        Debug.Log("gr");
        currentScore += scoreGreat;
        currentCombo++;
        ScoreSet();
    }

    public void GoodHit(){
        Debug.Log("go");
        currentScore += scoreGood;
        currentCombo++;
        ScoreSet();
    }

    public void BadHit(){
        Debug.Log("b");
        currentScore -= scoreBad;
        currentCombo = 0;
        ScoreSet();
    }

    public void NoteMiss(){
        Debug.Log("m");
        currentScore -= scoreMiss;
        currentCombo = 0;
        ScoreSet();
    }
}
