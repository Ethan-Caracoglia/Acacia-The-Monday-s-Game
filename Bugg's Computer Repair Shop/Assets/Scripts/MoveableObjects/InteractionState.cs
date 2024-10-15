using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public struct InteractionState
{
    public string ObjId;
    public InputAction.CallbackContext Ctx;
    public MouseButton Button;
    public Vector3 MousePos;
    public MoveMouse Sender;

    public InteractionState(string objId, InputAction.CallbackContext ctx, MouseButton button, MoveMouse sender)
    {
        ObjId = objId;
        Ctx = ctx;
        Button = button;
        MousePos = sender.transform.position;
        Sender = sender;
    }
}

public enum MouseButton{
    MouseLeft,
    MouseRight,
    MouseMiddle,
    MouseMovement
}