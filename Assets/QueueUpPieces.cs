using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class QueueUpPieces : MonoBehaviour
{
    public const int SQUARE_SIZE = 64;
    public const float SPAWN_INTERVAL = 3f;
    private float spawnTimer = SPAWN_INTERVAL;

    public const int QUEUE_SIZE = 8;
    private QueuePiece[] pieces = new QueuePiece[QUEUE_SIZE];

    public static GameObject queuePiecePrefab;
    public static GameObject piecePrefab;
    public static Camera mainCamera;

    void Start()
    {
        queuePiecePrefab = Resources.Load<GameObject>("Prefabs/QueuePiecePrefab");
        piecePrefab = Resources.Load<GameObject>("Prefabs/PiecePrefab");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        QueuePiece.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if ((spawnTimer -= Time.deltaTime) <= 0)
        {
            spawnTimer += SPAWN_INTERVAL;

            int slotIndex = -1;
            for (int i = 0; slotIndex < 0 && i < pieces.Length; i++)
            {
                if (pieces[i] == null)
                {
                    slotIndex = i;
                }
            }

            // todo: if slotIndex < 0, GAME OVER!
            if (slotIndex >= 0)
            {
                var pos = mainCamera.ScreenToWorldPoint(transform.GetChild(slotIndex).position);
                GameObject queuePieceObj = Instantiate(queuePiecePrefab, Vector3.zero, Quaternion.identity);
                QueuePiece queuePiece = queuePieceObj.GetComponent<QueuePiece>();
                queuePiece.Generate();
                queuePiece.transform.Translate(pos);

                Vector3[] corners = new Vector3[4];
                RectTransform rt = transform.GetChild(slotIndex)
                    .GetComponent<RectTransform>();
                rt.GetWorldCorners(corners);

                float slotWidth = Mathf.Abs(corners[2].x - corners[0].x);
                float slotHeight = Mathf.Abs(corners[2].y - corners[0].y);

                Vector3 screenDims = new Vector3(slotWidth, slotHeight, 0);
                Vector3 worldDims = mainCamera.ScreenToWorldPoint(screenDims);

                queuePiece.transform.localScale = worldDims / Max(queuePiece.width, queuePiece.height);

                pieces[slotIndex] = queuePiece;
            }
        }
    }
}
