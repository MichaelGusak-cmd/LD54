using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TetrisPiecesData
{
    public List<PieceData> tetrisPieces;
}

[System.Serializable]
public class PieceData
{
    public string name;
    public bool[,] shape;
}

public class JsonLoader : MonoBehaviour
{
    public TextAsset jsonFile; // Reference to the JSON file in Unity's inspector
    public TetrisPiecesData tetrisPiecesData; // A class to store the deserialized JSON data

    private void Start()
    {
        if (jsonFile != null)
        {
            // Deserialize the JSON data into the tetrisPiecesData object
            tetrisPiecesData = JsonUtility.FromJson<TetrisPiecesData>(jsonFile.text);

            if (tetrisPiecesData != null && tetrisPiecesData.tetrisPieces.Count > 0)
            {
                // The JSON data has been loaded into tetrisPiecesData
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
