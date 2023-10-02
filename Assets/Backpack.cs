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

                    var o = Instantiate(GameObject.Find("Piece"));
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
    }

    public void MoveLeft()
    {
        if (leftColumn > 0)
        {
            --leftColumn;
            transform.Translate(new Vector2(-Board.PIECE_SIZE, 0));
        }
    }

    public void MoveRight()
    {
        if (leftColumn + width < Board.GRID_WIDTH)
        {
            ++leftColumn;
            transform.Translate(new Vector2(Board.PIECE_SIZE, 0));
        }
    }

    public void MoveDownUnchecked()
    {
        --bottomRow;
        transform.Translate(new Vector2(0, -Board.PIECE_SIZE));
    }

    public void MoveUp()
    {
        if (bottomRow + height < Board.GRID_HEIGHT)
        {
            ++bottomRow;
            transform.Translate(new Vector2(0, Board.PIECE_SIZE));
        }
    }

    public void Rotate()
    {
        // translate all pieces to 0, 0
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (pieces[i, j] != null)
                    pieces[i, j].transform.Translate(
                        new Vector2(-Board.PIECE_SIZE * j, -Board.PIECE_SIZE * i));
            }
        }

        // convert columns to rows and vice-versa
        Piece[,] newPieces = new Piece[width, height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                newPieces[width - (j + 1), i] = pieces[i, j];
            }
        }

        (width, height) = (height, width);
        pieces = newPieces;

        // translate all pieces to their new spot
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (pieces[i, j] != null)
                    pieces[i, j].transform.Translate(
                        new Vector2(Board.PIECE_SIZE * j, Board.PIECE_SIZE * i));
            }
        }
    }
}