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

    void Start()
    {
        instance = this;
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
        comboText.text = "Combo: " + currentCombo;

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
        currentCombo = 0;
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
