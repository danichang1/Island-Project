using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeatmapMaker : MonoBehaviour
{
    public FileInfo beatFile;
    public StreamReader reader;
    private string text = "";

    void Start()
    {
        beatFile = new FileInfo("beatmap.txt");
        reader = beatFile.OpenText();
    }

    void Update()
    {
    }
}
