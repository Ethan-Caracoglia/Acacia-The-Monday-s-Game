using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Holds all of the necessary data on the mouse buttons and current position. This should be used
/// as the communicator between the mouse and any object
/// </summary>
public struct PlayerState
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
    #nullable enable
    private MoveableObject? heldObj; // The object (if any) that is being held
    #nullable disable
    #endregion

    #region Properties
    public int NumButtons
    {
        get
        {
            return NUM_OF_BUTTONS;
        }
    }
    #endregion

    #region Internal Methods
    public PlayerState(bool[] MBPressed, bool[] MBReleased, Vector2 mousePos, MoveableObject heldObj)
    {
        this.MBPressed = MBPressed;
        this.MBReleased = MBReleased;
        this.MBHeld = new bool[NUM_OF_BUTTONS];
        this.mousePos = mousePos;
        this.heldObj = heldObj;

        for (int i = 0; i < NUM_OF_BUTTONS; i++)
        {
            // Held values will alway be the opposite of released (unless we want to delay a "hold")
            this.MBHeld[i] = !this.MBReleased[i];
        }
    }
    #endregion

    #region External Methods
    public bool GetMBPressed(uint button)
    {
        return MBPressed[button];
    }

    public bool GetMBReleased(uint button)
    {
        return MBReleased[button];
    }

    public bool GetMBHeld(uint button)
    {
        return MBHeld[button];
    }

    public Vector2 GetMousePos()
    {
        Vector2 position = new Vector2(mousePos.x, mousePos.y);
        return position;
    }

    public MoveableObject GetHeldObj()
    {
        return this.heldObj;
    }
    #endregion
}