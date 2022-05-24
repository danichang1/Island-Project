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

    public TextMesh comboText;

    public TextAsset beatmapFile;
    public GameObject note;
    public GameObject hold;
    public GameObject parent;
    public GameObject end;

    public int perfectCount;
    public int greatCount;
    public int goodCount;
    public int badCount;
    public int missCount;

    public GameObject startScreen;

    public float beatTempo;

    public GameObject endScreen, fullCombo, endClear, endFail;
    public TextMesh perfectText, greatText, goodText, badText, missText;

    

    void Start()
    {
        endScreen.SetActive(false);
        fullCombo.SetActive(false);
        endClear.SetActive(false);
        endFail.SetActive(false);
        instance = this;
        startScreen.SetActive(true);

        //makes beatmap
        string text = beatmapFile.text;
        var lines = text.Split('\n');
        for(int i = 0; i < lines.Length; i++){
            var currentLine = lines[i];
            for(int u = 0; u < 4; u++){
                var why = "h123458e7lx";
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
                if (currentLine[u] == why[0]){  
                    var currentNote = Instantiate(note, new Vector3(xValue, i + 9, -0.1f), note.transform.rotation);
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[1]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 2;
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[2]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 4;
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[3]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 3;
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[4]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 8;
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[5]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 10;
                    currentNote.transform.parent = parent.transform;
                }else if (currentLine[u] == why[6]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 16;
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[7]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 1;
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[8]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 5;
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[9]){
                    var currentNote = Instantiate(hold, new Vector3(xValue, i + 9, -0.1f), hold.transform.rotation);
                    var head = currentNote.transform.GetChild(0).gameObject;
                    head.GetComponent<HoldNote>().tailLength = 32;
                    currentNote.transform.parent = parent.transform;
                } else if (currentLine[u] == why[10]){
                    end.transform.position = new Vector3(xValue, i + 0, -0.1f);
                } 
            }
        }
    }

    void Update()
    {
        if(!startMusic){
            if(Input.anyKeyDown){
                startScreen.SetActive(false);
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
        perfectCount++;
        currentScore += scorePerfect;
        currentCombo++;
        ScoreSet();
    }

    public void GreatHit(){
        greatCount++;
        currentScore += scoreGreat;
        currentCombo++;
        ScoreSet();
    }

    public void GoodHit(){
        goodCount++;
        currentScore += scoreGood;
        currentCombo++;
        ScoreSet();
    }

    public void BadHit(){
        badCount++;
        currentScore -= scoreBad;
        currentCombo = 0;
        ScoreSet();
    }

    public void NoteMiss(){
        missCount++;
        currentScore -= scoreMiss;
        currentCombo = 0;
        ScoreSet();
    }

    public void endGame(){
        perfectText.text = "" + perfectCount;
        greatText.text = "" + greatCount;
        goodText.text = "" + goodCount;
        badText.text = "" + badCount;
        missText.text = "" + missCount;
        endScreen.SetActive(true);
        if (currentCombo == 317){
            fullCombo.SetActive(true);
        } else if (currentScore >= 450){
            endClear.SetActive(true);
        } else{
            endFail.SetActive(true);
        }
    }
}
