using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** An abstract class for every tool  */
public abstract class abTool : MoveableObj
{
    public override void TryMouseInput(InteractionState state)
    {
        if (state.Button == MouseButton.MouseLeft && state.MouseAction == MouseState.MouseDown)
        {
            if (PickUpObject(state.MousePos, state.Sender))
            {
                objCollider.enabled = false;
            }
        }


        if (state.Button == MouseButton.MouseRight && state.MouseAction == MouseState.MouseUp)
        {
            // Check this, make sure it doesn't re-enable
            objCollider.enabled = true;
            SetDownObject();
        }
    }

    public override void HeldUse(MouseButton button, bool down)
    {
        if(button == MouseButton.MouseLeft && down)
        {
            SetDownObject();
        }

        if(button == MouseButton.MouseRight)
        {
            if (down)
                StartBaseUse();
            else
                EndBaseUse();
        }
    }

    public abstract void StartBaseUse();

    public abstract void EndBaseUse();
}
