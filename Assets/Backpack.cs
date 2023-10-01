using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public Piece[,] pieces;
    public int width, height;
    public int leftColumn, bottomRow;

    public void Start()
    {
        
    }

    public void Generate(bool[,] filled)
    {
        height = filled.GetLength(0);
        width = filled.GetLength(1);
        leftColumn = 0;
        bottomRow = 0;
        pieces = new Piece[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (filled[i, j])
                {
                    // todo: use a texture
                    // fornow: use a color gradient
                    Texture2D tex = new Texture2D(1, 1);
                    Color c = new(1, i / (float)(height - 1), j / (float)(width - 1), 1);
                    tex.SetPixel(0, 0, c);
                    tex.Apply();

                    var o = GameObject.Instantiate(GameObject.Find("Piece"));
                    o.transform.parent = transform;
                    o.transform.localScale = Vector2.one;
                    pieces[i, j] = o.GetComponent<Piece>();
                    pieces[i, j].sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0, 0), 1.0f);
                    pieces[i, j].transform.Translate(pieces[i, j].transform.localToWorldMatrix.MultiplyVector(new Vector3(j, i, 0)));
                }
            }
        }
    }

    public void Update()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (pieces[i, j] != null)
                {
                    pieces[i, j].transform
                        .Translate(new Vector2(0, -0.1f) * Time.deltaTime);
                }
            }
        }
    }
}