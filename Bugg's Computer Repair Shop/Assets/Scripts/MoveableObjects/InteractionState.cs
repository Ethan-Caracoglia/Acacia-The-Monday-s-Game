using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public struct InteractionState
{
    public string ObjId;
    public InputAction.CallbackContext Ctx;
    public MouseButton Button;

    public InteractionState(string objId, InputAction.CallbackContext ctx, MouseButton button)
    {
        ObjId = objId;
        Ctx = ctx;
        Button = button;
    }
}

public enum MouseButton{
    MouseLeft,
    MouseRight,
    MouseMiddle,
    MouseMovement
}