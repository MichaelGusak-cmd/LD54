using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Sprite sprite;
    public void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
