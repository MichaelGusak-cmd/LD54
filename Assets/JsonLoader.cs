using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PieceData
{
    public string name;
    public bool[,] shape;
}

public class JsonLoader : MonoBehaviour
{
    public TextAsset jsonFile; // Reference to the JSON file in Unity's inspector
    public List<PieceData> pieceDataList; // A list to store the deserialized JSON data

    private void Start()
    {
        if (jsonFile != null)
        {
            // Deserialize the JSON data into the pieceDataList
            pieceDataList = new List<PieceData>(JsonUtility.FromJson<PieceData[]>(jsonFile.text));

            if (pieceDataList.Count > 0)
            {
                // The JSON data has been loaded into pieceDataList
            }
            else
            {
                Debug.LogError("JSON data contains no pieces.");
            }
        }
        else
        {
            Debug.LogError("No JSON file assigned.");
        }
    }
}