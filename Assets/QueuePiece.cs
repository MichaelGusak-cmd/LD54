using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class QueuePiece : MonoBehaviour
{
    public static List<bool[,]> pieces = new List<bool[,]>();
    public static List<Texture2D> textures = new List<Texture2D>();

    GameObject[,] tiles;
    public int height, width;

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
        {true},
        {true},
        {true}
    };

    public static void Init()
    {
        const string pref = "Textures/items/";

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

    public void Generate()
    {
        int index = Random.Range(0, pieces.Count);
        bool[,] filled = pieces[index];
        Texture2D tex = textures[index];

        height = filled.GetLength(0);
        width = filled.GetLength(1);
        tiles = new GameObject[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (filled[i, j])
                {
                    var o = Instantiate(QueueUpPieces.piecePrefab);
                    o.transform.parent = transform;
                    o.transform.localScale = Vector2.one;
                    tiles[i, j] = o;

                    o.GetComponent<SpriteRenderer>().sprite = Sprite.Create(
                        tex, new Rect(j * 16, (height - i - 1) * 16, 16, 16),
                        new Vector2(0.5f, 0.5f), 16);
                    o.GetComponent<SpriteRenderer>().sortingOrder = 15;
                    tiles[i, j].transform.Translate(tiles[i, j].transform.localToWorldMatrix.MultiplyVector(new Vector3(j, height - i - 1, 0)));
                }
            }
        }
    }
}
