using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** An abstract class for every tool  */
public abstract class abTool : MoveableObj
{
    #region Methods
    #region public
    public override void GetInput(PlayerState player)
    {
        if (player.GetMBPressed(1))
        {
            HeldUse();
        }
    }

    public abstract void HeldUse();

    public abstract void StartBaseUse();

    public abstract void EndBaseUse();
    #endregion
    #endregion
}
