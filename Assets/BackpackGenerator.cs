using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // todo: this function will accept data representing the backpack the player
    // filled
    // fornow: generates a 4x4 backpack w/ square sprites
    public Backpack Generate()
    {
        Backpack result = new(4, 4);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                result.pieces[i, j] = new Piece();
                
                var tex = new Texture2D(1, 1);
                tex.SetPixel(0, 0, new Color(1, i / 4.0f, j / 4.0f, 1));
                tex.Apply();

                result.pieces[i, j].sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
                result.pieces[i, j].transform.Translate(new Vector2(i, j));
            }
        }
        return result;
    }
}
