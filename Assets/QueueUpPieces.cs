using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueUpPieces : MonoBehaviour
{
    public const int SQUARE_SIZE = 64;
    public const float SPAWN_INTERVAL = 3f;
    private float spawnTimer = SPAWN_INTERVAL;

    public const int QUEUE_SIZE = 8;
    private QueuePiece[] pieces = new QueuePiece[QUEUE_SIZE];

    public static GameObject queuePiecePrefab;
    public static GameObject piecePrefab;

    void Start()
    {
        queuePiecePrefab = Resources.Load<GameObject>("Prefabs/QueuePiecePrefab");
        piecePrefab = Resources.Load<GameObject>("Prefabs/PiecePrefab");
        QueuePiece.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if ((spawnTimer -= Time.deltaTime) <= 0)
        {
            spawnTimer += SPAWN_INTERVAL;
            QueuePiece newPiece = QueuePiece.Generate();
            bool spotFound = false;
            for (int i = 0; !spotFound && i < pieces.Length; i++)
            {
                if (pieces[i] == null)
                {
                    pieces[i] = newPiece;
                    pieces[i].transform.parent = transform.GetChild(i);
                    pieces[i].transform.localPosition = Vector3.zero;
                    spotFound = true;
                }
            }
            // todo: if (!spotFound) {GAME OVER}
        }
    }

    /*
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

                    Texture2D texture = new Texture2D(SQUARE_SIZE, SQUARE_SIZE);
                    Color randomColor = new Color(Random.value, Random.value, Random.value);
                    Color[] pixels = new Color[SQUARE_SIZE * SQUARE_SIZE];
                    for (int h = 0; h < pixels.Length; h++)
                    {
                        pixels[h] = randomColor;
                    }
                    texture.SetPixels(pixels);
                    texture.Apply();
                    Sprite squareSprite = Sprite.Create(texture, new Rect(0, 0, SQUARE_SIZE, SQUARE_SIZE), new Vector2(0.5f, 0.5f));

                }
            }
        }
        return parent;
    }
    */
}
