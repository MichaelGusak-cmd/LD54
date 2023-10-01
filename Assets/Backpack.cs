using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack
{
    // pieces are initialized externally by BackpackGenerator
    public Piece[,] pieces;
    // set by Board when Backpack is sent to it
    public int bottomRow, leftColumn, width, height;

    public Backpack(int width_, int height_)
    {
        pieces = new Piece[width_, height_];
        width = width_;
        height = height_;
        bottomRow = 0;
        leftColumn = 0;
    }

    public void moveLeft()
    {
        if (leftColumn > 0)
        {
            leftColumn--;
            foreach (var piece in pieces)
            {
                piece.transform.Translate(new Vector2(-1, 0));
            }
        }
    }

    public void moveRight()
    {
        if (leftColumn < Board.GRID_WIDTH - 1)
        {
            leftColumn++;
            foreach (var piece in pieces)
            {
                piece.transform.Translate(new Vector2(1, 0));
            }
        }
    }

    public void moveDown()
    {
        if (bottomRow > 0)
        {
            bottomRow--;
            foreach (var piece in pieces)
            {
                piece.transform.Translate(new Vector2(0, -1));
            }
        }
    }
}
