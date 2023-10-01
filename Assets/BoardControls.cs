using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControls
{
    public static bool getMoveLeft()
    {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public static bool getMoveRight()
    {
        return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }

    public static bool getRotate()
    {
        return Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
    }

    public static bool getFastDrop()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}
