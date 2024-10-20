using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public struct MouseButtonsState
{
    #region Fields
    // "MB" stands for Mouse Button(s)
    public bool[] MBPressed;
    public bool[] MBReleased;
    public bool[] MBHeld;
    public int numButtons;
    #endregion

    #region Methods
    public MouseButtonsState(int numButtons, bool[] MBPressed, bool[] MBReleased)
    {
        this.numButtons = numButtons;
        this.MBPressed = new bool[numButtons];
        this.MBReleased = new bool[numButtons];
        this.MBHeld = new bool[numButtons];

        for (int i = 0; i < numButtons; i++)
        {
            this.MBPressed[i] = MBPressed[i];
        }

        for (int i = 0; i < numButtons; i++)
        {
            this.MBReleased[i] = MBReleased[i];
        }

        for (int i = 0; i < numButtons; i++)
        {
            // Holding values will alway be the opposite of released (unless we want to delay a "hold")
            this.MBHeld[i] = !this.MBReleased[i];
        }
    }
    #endregion
}