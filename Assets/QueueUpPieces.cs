using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueUpPieces : MonoBehaviour
{
    
    public int squareSize = 64;
    public float spawnTimer = 3f;
    private float spawnTime = 0;
    public GameObject pieceSourcePrefab;
    public GameObject piecePrefab;

    public int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (spawnTime + spawnTimer < Time.time) {
            spawnTime = Time.time;
            GameObject p = SpawnPiece();
            count+=3;
        }
    }

    public GameObject SpawnPiece() {
        GameObject parent = Instantiate(pieceSourcePrefab, new Vector3(count, 0, 0), Quaternion.identity);
        QueuePiece p = parent.GetComponent<QueuePiece>();
        p.Load();
        
        for (int i = 0; i < p.fill.GetLength(0); i++) {
            for (int j = 0; j < p.fill.GetLength(1); j++) {
                if (p.fill[i,j]) {
                    // Instantiate the prefab as a child of the parent
                    GameObject piece = Instantiate(piecePrefab, parent.transform);

                    piece.transform.localPosition = new Vector3(i,j);
                    piece.transform.localRotation = Quaternion.identity;
                    piece.transform.localScale = Vector3.one;

                    Texture2D texture = new Texture2D(squareSize, squareSize);
                    Color randomColor = new Color(Random.value, Random.value, Random.value);
                    Color[] pixels = new Color[squareSize * squareSize];
                    for (int h = 0; h < pixels.Length; h++)
                    {
                        pixels[h] = randomColor;
                    }
                    texture.SetPixels(pixels);
                    texture.Apply();
                    Sprite squareSprite = Sprite.Create(texture, new Rect(0, 0, squareSize, squareSize), new Vector2(0.5f, 0.5f));

                }
            }
        }
        return parent;
    }
}
