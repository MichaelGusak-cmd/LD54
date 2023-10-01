using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int
        GRID_WIDTH = 16,
        GRID_HEIGHT = 32;
    public const float
        LAYOUT_MARGIN = 0.95f;

    public const float
        LAYOUT_W = (LAYOUT_MARGIN / GRID_HEIGHT) * GRID_WIDTH,
        LAYOUT_H = LAYOUT_MARGIN,
        PIECE_SIZE = LAYOUT_W / GRID_WIDTH;

    public static float pieceUnits;

    const float FALL_INTERVAL = 0.5f;
    const float MOVE_INTERVAL = 0.25f;

    Piece[,] grid = new Piece[GRID_HEIGHT, GRID_WIDTH];

    Backpack droppingBag;

    float fallTimer = FALL_INTERVAL;
    float moveTimer = 0;

    // Can a new bag be dropped? False if there is already one being dropped
    public bool CanDropBag()
    {
        return droppingBag == null;
    }

    // Drop a new backpack
    public void DropBag(Backpack backpack)
    {
        droppingBag = backpack;
        fallTimer = FALL_INTERVAL;
        droppingBag.leftColumn = 0;
        droppingBag.bottomRow = GRID_HEIGHT - droppingBag.height;
        Vector3 parentPos;
        Quaternion rotation;
        transform.parent.GetLocalPositionAndRotation(out parentPos, out rotation);
        droppingBag.transform.Translate(parentPos, Space.World);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        // if we go by height, this is the width we'll get
        const float maxWidth = (LAYOUT_MARGIN / GRID_HEIGHT) * GRID_WIDTH;
        // if width is small enough, use it
        if (maxWidth <= LAYOUT_MARGIN)
        {
            transform.localScale = new Vector2(maxWidth, LAYOUT_MARGIN);
        }
        else
        {
            transform.localScale = new Vector2(LAYOUT_MARGIN, (LAYOUT_MARGIN / GRID_WIDTH) * GRID_HEIGHT);
        }
        */
        transform.localScale = new Vector2(LAYOUT_W, LAYOUT_H);
        float heightUnits = transform.parent.localScale.y * LAYOUT_MARGIN;
        pieceUnits = heightUnits / GRID_HEIGHT;

        // generate an example backpack
        bool[,] filled = new bool[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                filled[i, j] = true;
            }
        }

        var backpack = GameObject.Instantiate(GameObject.Find("Backpack"));
        backpack.transform.parent = transform;
        backpack.transform.localScale = new Vector2(1.0f / GRID_WIDTH, 1.0f / GRID_HEIGHT);
        var b = backpack.GetComponent<Backpack>();
        b.Generate(filled);
        DropBag(b);

        // GameObject backpackObject = new();
        // backpackObject.AddComponent<Backpack>();
        // var b = backpackObject.GetComponent<Backpack>();
        // b.Generate(filled);
        // DropBag(b);
    }

    /*
     * Blocks already in board need to move downwards, be cleared if moved a
     * whole unit
     * 
     * Newly dropped blocks need to move down 1 row periodically, player can
     * rotate them and move side to side, player can also make them fall faster
     */

    // Update is called once per frame
    void Update()
    {
        if (droppingBag != null)
        {
            updateDroppingBag();
        }
        
        // todo move blocks sprites down

        // todo if blocks are low enough, delete bottom row, move all others down
    }

    void updateDroppingBag()
    {
        // handle moving left/right
        if (BoardControls.getMoveLeft())
        {
            if ((moveTimer -= Time.deltaTime) <= 0 && droppingBag.leftColumn > 0)
            {
                moveTimer += MOVE_INTERVAL;
                // droppingBag.moveLeft();
                // todo: collisions. Maybe move it, and if it now overlaps
                // something, move it back?
            }
        }
        else if (BoardControls.getMoveRight())
        {
            if ((moveTimer -= Time.deltaTime) <= 0 && droppingBag.leftColumn < GRID_WIDTH - 1)
            {
                moveTimer += MOVE_INTERVAL;
                // droppingBag.moveRight();
            }
        }

        // handle rotating: todo

        // handle falling: double speed if quickdrop is held
        fallTimer -= (BoardControls.getFastDrop() ?  Time.deltaTime * 2 : Time.deltaTime);
        if (fallTimer <= 0)
        {
            // droppingBag.moveDown();
            // todo: if collision occurred or we're at the bottom of the board,
            // move back (for collisions only) and convert bag to normal pieces
        }
    }
}
