using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Holds all of the necessary data on the mouse buttons and current position
/// </summary>
public struct MState
{
    #region Constants
    // Number of buttons that are used from the mouse
    private const int NUM_OF_BUTTONS = 2;
    #endregion

    #region Fields
    /** "MB" stands for Mouse Button(s) */
    private bool[] MBPressed; // MBPressed[i] == true when the button was pressed this frame
    private bool[] MBReleased; // MBReleased[i] == true on and after the frame it is released
    private bool[] MBHeld; // MBHeld[i] == true until MBReleased[i] == true
    private Vector2 mousePos; // The world position of the mouse
    #endregion

    #region Internal Methods
    public MState(bool[] MBPressed, bool[] MBReleased, Vector2 mousePos)
    {
        this.MBPressed = MBPressed;
        this.MBReleased = MBReleased;
        this.MBHeld = new bool[NUM_OF_BUTTONS];
        this.mousePos = mousePos;

        for (int i = 0; i < NUM_OF_BUTTONS; i++)
        {
            // Held values will alway be the opposite of released (unless we want to delay a "hold")
            this.MBHeld[i] = !this.MBReleased[i];
        }
    }
    #endregion

    #region External Methods
    bool GetMBPressed(uint button)
    {
        return MBPressed[button];
    }

    bool GetMBReleased(uint button)
    {
        return MBReleased[button];
    }

    bool GetMBHeld(uint button)
    {
        return MBHeld[button];
    }

    Vector2 GetMousePos() 
    { 
        Vector2 position = new Vector2(mousePos.x, mousePos.y);
        return position;
    }
    #endregion
}