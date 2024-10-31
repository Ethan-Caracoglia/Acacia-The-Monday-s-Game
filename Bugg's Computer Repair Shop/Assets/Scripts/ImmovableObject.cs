using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableObj : ObjInterface
{
    public override void ParentPositionChange(Vector3 newPos)
    {
        UpdatePosition(newPos);
    }

    public override void TryMouseInput(InteractionState state)
    {
        return;
    }

    public override void UpdateMousePosition(Vector3 MousePos)
    {
        return;
    }
}
