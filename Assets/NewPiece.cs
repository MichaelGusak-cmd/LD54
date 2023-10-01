using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePiece : MonoBehaviour
{
    public JsonLoader json;
    public bool[,] fill;

    public void Start() {
        
        json = GameObject.Find("Canvas").GetComponent<JsonLoader>();
        fill = json.pieceDataList[Random.Range(0, json.pieceDataList.Count)].shape;
    }
}


