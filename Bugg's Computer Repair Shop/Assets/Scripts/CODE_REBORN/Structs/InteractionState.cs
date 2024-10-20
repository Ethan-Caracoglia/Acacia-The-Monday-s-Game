using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// A struct that stores the mouse interaction data
public struct InteractionState
{
    public string ObjId;
    public MouseState MouseAction;
    public MouseButton Button;
    public Vector3 MousePos;
    public MoveMouse Sender;
    public bool[] mouseButtons;

    public InteractionState(string objId, MouseButton button, MouseState mouseAction, MoveMouse sender)
    {
        ObjId = objId;
        MouseAction = mouseAction;
        Button = button;
        MousePos = sender.transform.position;
        Sender = sender;
        mouseButtons = new bool[3];
    }
}

public enum MouseButton
{
    MouseLeft,
    MouseRight,
    MouseMiddle,
    MouseMovement
}

// Test both L & R at the same time

public enum MouseState
{
    MouseDown,
    MouseUp,
    Held,
    None
}