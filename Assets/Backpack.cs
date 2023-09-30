using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public Piece[,] filled;

    public void Start() {
        filled = new Piece[4,4];
    }
}
