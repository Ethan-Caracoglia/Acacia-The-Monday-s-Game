using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MousePosState
{
    #region Fields
    public Vector2 pos;
    #endregion

    #region Methods
    public MousePosState(Vector2 pos)
    {
        this.pos = pos; 
    }
    #endregion
}
