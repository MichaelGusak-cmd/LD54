using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePiece : MonoBehaviour
{
    public static List<bool[,]> pieces = new List<bool[,]>();
    public static List<Texture2D> textures = new List<Texture2D>();

    Piece[,] tiles;
    int height, width;

    public static bool[,] Ipiece = new bool[,] {
        {true, true, true, true}
    };

    public static bool[,] Jpiece = new bool[,] {
        {true, false, false}, 
        {true, true, true}
    };
        
    public static bool[,] Lpiece = new bool[,] {
        {false, false, true},
        {true, true, true}
    };

    public static bool[,] Opiece = new bool[,] {
        {true, true},
        {true, true}
    };

    public static bool[,] Spiece = new bool[,] {
        {false, true, true}, 
        {true, true, false}
    };

    public static bool[,] Tpiece = new bool[,] {
        {true, true, true},
        {false, true, false}
    };

    public static bool[,] Zpiece = new bool[,] {
        {true, true, false},
        {false, true, true}
    };

    public static bool[,] SmallIpiece = new bool[,] {
        {true},
        {true}
    };

    public static bool[,] SmallLpiece = new bool[,] {
        {true, false},
        {true, true}
    };

    public static bool[,] MediumIpiece = new bool[,] {
        {true, true, true}
    };

    public static void Init()
    {
        const string pref = "textures/items/";
        pieces.Add(Ipiece);
        textures.Add(Resources.Load<Texture2D>(pref + "4x1_wrench"));

        pieces.Add(Jpiece);
        textures.Add(Resources.Load<Texture2D>(pref + "3x2_boot_flipped"));

        pieces.Add(Lpiece);
        textures.Add(Resources.Load<Texture2D>(pref + "3x2_boot"));

        pieces.Add(Opiece);
        textures.Add(Resources.Load<Texture2D>(pref + "2x2_box"));

        pieces.Add(Spiece);
        textures.Add(Resources.Load<Texture2D>(pref + "3x2_box"));

        pieces.Add(Tpiece);
        textures.Add(Resources.Load<Texture2D>(pref + "3x2_drill"));

        pieces.Add(Zpiece);
        textures.Add(Resources.Load<Texture2D>(pref + "3x2_box_flipped"));

        pieces.Add(SmallIpiece);
        textures.Add(Resources.Load<Texture2D>(pref + "1x2_gameboy"));

        pieces.Add(SmallLpiece);
        textures.Add(Resources.Load<Texture2D>(pref + "2x2_boot"));

        pieces.Add(MediumIpiece);
        textures.Add(Resources.Load<Texture2D>(pref + "1x3_bottle"));
    }

    public static QueuePiece Generate()
    {
        int index = Random.Range(0, pieces.Count);
        bool[,] filled = pieces[index];
        Texture2D tex = textures[index];

        GameObject pieceObj = Instantiate(QueueUpPieces.queuePiecePrefab);
        QueuePiece qp = pieceObj.GetComponent<QueuePiece>();
        qp.height = filled.GetLength(0);
        qp.width = filled.GetLength(1);
        qp.tiles = new Piece[qp.height, qp.width];

        for (int i = 0; i < qp.height; i++)
        {
            for (int j = 0; j < qp.width; j++)
            {
                if (filled[i, j])
                {
                    var o = Instantiate(GameObject.Find("Piece"), qp.transform);
                    o.transform.localScale = Vector2.one;
                    qp.tiles[i, j] = o.GetComponent<Piece>();
                    qp.tiles[i, j].sprite = Sprite.Create(tex, new Rect(j, i, 1, 1), new Vector2(0.5f, 0.5f), 16);
                    qp.tiles[i, j].transform.Translate(qp.tiles[i, j].transform.localToWorldMatrix.MultiplyVector(new Vector3(j, i, 0)));
                }
            }
        }

        return qp;
    }
}


