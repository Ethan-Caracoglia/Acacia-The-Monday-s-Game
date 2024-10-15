using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abTool : MoveableObject
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TryMouseInput(InteractionState state)
    {
        if (state.Button == MouseButton.MouseLeft && state.Ctx.performed)
        {
            if (PickUpObject(state.MousePos, state.Sender))
            {
                objCollider.enabled = false;
            }
        }


        if (state.Button == MouseButton.MouseRight && state.Ctx.canceled)
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
