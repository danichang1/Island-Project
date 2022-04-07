using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ArrayLayout {

public static int RowSize = 7;

    [System.Serializable]
    public struct RowData{
        public bool[] Row;
    }

    public RowData[] rows = new RowData[RowSize];
}
