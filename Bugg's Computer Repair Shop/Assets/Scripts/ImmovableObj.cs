using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableObj : ObjInterface
{
    #region Methods
    #region public
    public override void ParentPositionChange(Vector3 newPos)
    {
        Move(newPos);
    }

    public override void GetInput(PlayerState player)
    {
        return;
    }
    #endregion
    #endregion
}
