using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int
        GRID_WIDTH = 16,
        GRID_HEIGHT = 32;

    public static float PIECE_SIZE;

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
    public void DropBag(bool [,] filled)
    {
        fallTimer = FALL_INTERVAL;

        var backpackObj = GameObject.Instantiate(GameObject.Find("Backpack"));
        droppingBag = backpackObj.GetComponent<Backpack>();

        droppingBag.transform.parent = transform.parent;
        droppingBag.transform.localScale = Vector2.one;
        
        droppingBag.Generate(filled);
        droppingBag.leftColumn = 0;
        droppingBag.bottomRow = GRID_HEIGHT - droppingBag.height;
        
        // move to center
        droppingBag.transform.Translate(transform.parent.localPosition);
        // move to top left
        droppingBag.transform.Translate(new Vector2(-PIECE_SIZE * GRID_WIDTH / 2, PIECE_SIZE * (GRID_HEIGHT / 2 - droppingBag.height)));
    }

    // Start is called before the first frame update
    void Start()
    {
        PIECE_SIZE = transform.parent.localScale.x;
        transform.localScale = new Vector2(GRID_WIDTH, GRID_HEIGHT);

        // generate an example backpack
        bool[,] filled = new bool[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                filled[i, j] = true;
            }
        }
        DropBag(filled);
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
        if (BoardControls.MoveLeft())
        {
            moveTimer = MOVE_INTERVAL;
            if (droppingBag.leftColumn > 0)
            {
                droppingBag.MoveLeft();
                if (Overlap())
                    droppingBag.MoveRight();
            }
        }
        else if (BoardControls.MoveRight())
        {
            moveTimer = MOVE_INTERVAL;
            if (droppingBag.leftColumn + droppingBag.width < GRID_WIDTH)
            {
                droppingBag.MoveRight();
                if (Overlap())
                    droppingBag.MoveLeft();
            }
        }
        else if (BoardControls.MoveLeftHeld())
        {
            if ((moveTimer -= Time.deltaTime) <= 0 && droppingBag.leftColumn > 0)
            {
                moveTimer += MOVE_INTERVAL;
                droppingBag.MoveLeft();
                if (Overlap())
                    droppingBag.MoveRight();
            }
        }
        else if (BoardControls.MoveRightHeld())
        {
            if ((moveTimer -= Time.deltaTime) <= 0 && droppingBag.leftColumn + droppingBag.width < GRID_WIDTH)
            {
                moveTimer += MOVE_INTERVAL;
                droppingBag.MoveRight();
                if (Overlap())
                    droppingBag.MoveLeft();
            }
        }

        // handle rotating
        if (BoardControls.Rotate())
        {
            droppingBag.Rotate();
            // if an overlap occurred, rotate 3 more times to unrotate it
            if (Overlap())
            {
                droppingBag.Rotate();
                droppingBag.Rotate();
                droppingBag.Rotate();
            }
        }

        // handle falling: double speed if quickdrop is held
        fallTimer -= (BoardControls.FastDropHeld() ?  Time.deltaTime * 3 : Time.deltaTime);
        if (fallTimer <= 0)
        {
            droppingBag.MoveDownUnchecked();
            fallTimer += FALL_INTERVAL;
            // if we collide or reach the bottom, add it to the board
            if (droppingBag.bottomRow < 0 || Overlap())
            {
                droppingBag.MoveUp();
                AddToBoard();
            }
        }
    }

    // Return true if droppingBag is overlapping part of the board
    bool Overlap()
    {
        for (int i = 0; i < droppingBag.height; i++)
        {
            for (int j = 0; j < droppingBag.width; j++)
            {
                if (grid[i + droppingBag.bottomRow, j + droppingBag.leftColumn] != null)
                    return true;
            }
        }
        return false;
    }

    // Add the droppingBag's pieces to the board
    void AddToBoard()
    {
        for (int i = 0; i < droppingBag.height; i++)
        {
            for (int j = 0; j < droppingBag.width; j++)
            {
                if (droppingBag.pieces[i, j] != null)
                {
                    grid[i + droppingBag.bottomRow, j + droppingBag.leftColumn] = droppingBag.pieces[i, j];
                }
            }
        }
        droppingBag = null;

        // todo: remove when not needed for testing
        bool[,] filled = new bool[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                filled[i, j] = true;
            }
        }
        DropBag(filled);
    }
}
