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

    const float FALL_INTERVAL = 0.5f;
    const float MOVE_INTERVAL = 0.25f;

    Piece[,] grid = new Piece[GRID_HEIGHT, GRID_WIDTH];

    Backpack droppingBag;

    float fallTimer = FALL_INTERVAL;
    float moveTimer = 0;

    // Can a new bag be dropped? False if there is already one being dropped
    public bool CanDropBag()
    {
        // todo
        return true;
    }

    // Drop a new backpack
    public void DropBag(Backpack backpack)
    {
        droppingBag = backpack;
        fallTimer = FALL_INTERVAL;
    }

    // Start is called before the first frame update
    void Start()
    {
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
                droppingBag.moveLeft();
                // todo: collisions. Maybe move it, and if it now overlaps
                // something, move it back?
            }
        }
        else if (BoardControls.getMoveRight())
        {
            if ((moveTimer -= Time.deltaTime) <= 0 && droppingBag.leftColumn < GRID_WIDTH - 1)
            {
                moveTimer += MOVE_INTERVAL;
                droppingBag.moveRight();
            }
        }

        // handle rotating: todo

        // handle falling: double speed if quickdrop is held
        fallTimer -= (BoardControls.getFastDrop() ?  Time.deltaTime * 2 : Time.deltaTime);
        if (fallTimer <= 0)
        {
            droppingBag.moveDown();
            // todo: if collision occurred or we're at the bottom of the board,
            // move back (for collisions only) and convert bag to normal pieces
        }
    }
}
