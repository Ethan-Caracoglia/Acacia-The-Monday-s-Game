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
    #nullable enable
    #region Constants
    // Number of buttons that are used from the mouse
    private const int NUM_OF_BUTTONS = 2;
    #endregion

    #region Fields
    #region private
    /** "MB" stands for Mouse Button(s) */
    private bool[] MBPressed; // MBPressed[i] == true when the button was pressed this frame
    private bool[] MBReleased; // MBReleased[i] == true on and after the frame it is released
    private bool[] MBHeld; // MBHeld[i] == true until MBReleased[i] == true
    private Vector2 MousePos; // The world position of the mouse
    private MoveableObj? HeldObj; // The object (if any) that is being held
    #endregion
    #endregion

    #region Methods
    #region public
    public PlayerState(bool[] MBPressed, bool[] MBReleased, Vector2 MousePos, MoveableObj? HeldObj)
    {
        this.MBPressed = MBPressed;
        this.MBReleased = MBReleased;
        this.MBHeld = new bool[NUM_OF_BUTTONS];
        this.MousePos = MousePos;
        this.HeldObj = HeldObj;

        for (int i = 0; i < NUM_OF_BUTTONS; i++)
        {
            // Held values will alway be the opposite of released (unless we want to delay a "hold")
            this.MBHeld[i] = !this.MBReleased[i];
        }
    }

    public int GetNumButtons()
    {
        return NUM_OF_BUTTONS;
    }

    public bool GetMBPressed(int button)
    {
        return MBPressed[button];
    }

    public bool GetMBReleased(int button)
    {
        return MBReleased[button];
    }

    public bool GetMBHeld(int button)
    {
        return MBHeld[button];
    }

    public Vector2 GetMousePos()
    {
        Vector2 position = new Vector2(MousePos.x, MousePos.y);
        return position;
    }

    public MoveableObj? GetHeldObj()
    {
        return HeldObj;
    }

    public override string ToString()
    {
        return $"LeftMousePressed: {MBPressed[0]}\n" +
            $"LeftMouseReleased: {MBReleased[0]}\n" +
            $"LeftMouseHeld: {MBHeld[0]}\n" +
            $"RightMousePressed: {MBPressed[1]}\n" +
            $"RightMouseReleased: {MBReleased[1]}\n" +
            $"RightMouseHeld: {MBHeld[1]}\n" +
            $"MousePos: {MousePos}\n" +
            $"HeldObj: {HeldObj}";
    }
    #endregion
    #endregion
}