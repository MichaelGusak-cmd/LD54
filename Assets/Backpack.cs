using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public bool[,] Grid { get; set; }
    public List<GameObject> Pieces { get; set; }
    public List<GameObject> InGrid { get; set; }
    public bool Ready { get; set; }

    public void Start() {
        Ready = false;
        Generate(3,4);
    }

    public void Generate(int x, int y) {
        Grid = new bool[x,y];
        SetPieceAt(1,1);
        Ready = true;
    }

    public void SetPieceAt(int x, int y) {
        GameObject p = Pieces[0];
        Piece piece = p.GetComponent<Piece>()
        piece.SetPos(x,y);
        piece.SetRot(Vector2.right);
        bool[,] locations = piece.GetPlacements();


        bool valid = true;
        for (int i = 0; i < locations.GetLength(0) && valid; i++) {
            for (int j = 0; j < locations.GetLength(1) && valid; j++) {
                if (i+x > Grid.GetLength(0) || y+j > Grid.GetLength(1)) {
                    valid = false;
                }
                if (Grid[i+x,j+y] && locations[i,j] && valid) 
                    valid = false;
            }
        }

        if (valid) {
            for (int i = 0; i < locations.GetLength(0); i++) {
                for (int j = 0; j < locations.GetLength(1); j++) {
                    if (locations[i,j]) 
                        Grid[x+i, y+j] = true;
                }
            }
            InGrid.Add(p);
        }
    }
}