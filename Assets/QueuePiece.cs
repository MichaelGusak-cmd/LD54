using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePiece : MonoBehaviour
{
    //public GameObject jsonObj;
    //public JsonLoader json;
    public bool[,] fill;

    public List<bool[,]> pieces = new List<bool[,]>();
    
    public bool[,] Ipiece = new bool[,] {{true, true, true, true}};

    public bool[,] Jpiece = new bool[,] {
        {true, false, false}, 
        {true, true, true}
    };
        
    public bool[,] Lpiece = new bool[,] {
        {false, false, true},
        {true, true, true}
    };

    public bool[,] Opiece = new bool[,] {
        {true, true},
        {true, true}
    };

    public bool[,] Spiece = new bool[,] {
        {false, true, true}, 
        {true, true, false}
    };

    public bool[,] Tpiece = new bool[,] {
        {false, true, false}, 
        {true, true, true}
    };

    public bool[,] Zpiece = new bool[,] {
        {true, true, false},
        {false, true, true}
    };

    public void Load() {
        pieces.Add(Ipiece);
        pieces.Add(Jpiece);
        pieces.Add(Lpiece);
        pieces.Add(Opiece);
        pieces.Add(Spiece);
        pieces.Add(Tpiece);
        pieces.Add(Zpiece);
        
        //json = GameObject.Find("JsonLoader").GetComponent<JsonLoader>();
        fill = pieces[Random.Range(0, pieces.Count)];//json.tetrisPiecesData.tetrisPieces[Random.Range(0, json.tetrisPiecesData.tetrisPieces.Count)].shape;
    }
}


